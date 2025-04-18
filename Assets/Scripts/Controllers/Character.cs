using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.EventSystems;

public class Character : MonoBehaviour
{
    private bool isSelected = false;
    private bool attacking = false;
    [SerializeField] private bool isTileAttack = false;
    public bool skipTurn;

    [Header("Gameplay Values")]
    [SerializeField] private int moveRange;
    [SerializeField] private int attackRange;
    public int DamageMultiplier = 1;
    public float RangeMultiplier = 1;

    [Header("Programming Values")]
    public bool canAct = false;
    public Tile curTile;
    [SerializeField] private bool aiControlled;

    [SerializeField] private Ability[] attacks;
    private Ability curAbility;

    private Vector2Int startCoordinates;

    [SerializeField] private controller controllerType;
    //Set an enum for elemental afflictions to check against combos

    public enum controller { none, player, ai}

    public static Action OnPlayerSelected;
    public static Action<int, Vector2Int> OnShouldUpdateTiles;
    public static Action OnCantAct;
    public static Action<int, Vector2Int> OnAttackPressed;

    public controller ControllerType { get => controllerType; }

    [SerializeField] private GameObject damageTextPrefab;
    public GameObject currentlyAttacking;

    [SerializeField] private Ability.AbilityType affectedAbility;

    #region OnEnableOnDisable

    private void OnEnable()
    {
        //OnPlayerSelected += SelectCharacter;

        //player should not be listening to when a tile is selected but alas
        Tile.TileSelected += MoveOrAttack;

        GameManager.OnTurnStart += TryTriggerCombo;
        GameManager.OnTurnStart += SetStartCoordinates;

        curTile.SetIsOccupied(true);
        curTile.SetOccupyingCharacter(this);
    }

    private void OnDisable()
    {
        OnPlayerSelected -= SelectCharacter;

        GameManager.OnTurnStart -= TryTriggerCombo;
        GameManager.OnTurnStart -= SetStartCoordinates;
    }

    #endregion

    #region Start

    private void Start()
    {
        if(controllerType == controller.player)
        {
            PlayerController.instance.AddControlledCharacter(this);
        }
        else if(controllerType == controller.ai)
        {
            AIController.instance.AddControlledCharacter(this);
        }
        else
        {
            Debug.LogWarning("This character has no assigned controller type.");
        }
    }

    #endregion

    #region Constructors

    public Character()
    {

    }

    public Character(controller controllerType)
    {
        this.controllerType = controllerType;
    }

    #endregion

    #region Getters and Setters

    /// <summary>
    /// Returns the controller controlling this character.
    /// </summary>
    /// <returns></returns>
    public Controller GetController()
    {
        if (controllerType == controller.player)
            return PlayerController.instance;
        else if (controllerType == controller.ai)
            return AIController.instance;
        else
            throw new Exception("No controller assigned to this character.");
    }

    public Ability GetCurrentAttack()
    {
        return curAbility;
    }

    public Ability.AbilityType GetCurrentAttackType()
    {
        return curAbility.ThisAbility;
    }

    public void SetCurrentAttack(int attackIndex)
    {
        curAbility = attacks[attackIndex];
    }

    public void SetIsTileAttack(bool value)
    {
        isTileAttack = value;
    }

    public int GetMovementRange()
    {
        return moveRange;
    }

    public void SetMovementRange(int range)
    {
        moveRange = range;
    }

    #endregion

    #region Activate and Deactivate Character

    /// <summary>
    /// Activates a character.
    /// </summary>
    public void ActivateCharacter()
    {
        if(skipTurn == false)
        {
            canAct = true;
        }

        skipTurn = false;
    }

    /// <summary>
    /// Deactivates a character.
    /// </summary>
    public void DeactivateCharacter()
    {
        canAct = false;
        isSelected = false;
        isTileAttack = false;

        PlayerController.instance.GetActionUI().SetActive(false);
        OnCantAct?.Invoke();
    }

    #endregion

    /// <summary>
    /// Selects a character updates map + UI state.
    /// </summary>
    public void SelectCharacter()
    {
        if (controllerType == controller.player)
        {
            if(PlayerController.instance.GetSelectedCharacter() != null)
                PlayerController.instance.GetSelectedCharacter().isSelected = false;

            PlayerController.instance.SetSelectedCharacter(this);
        }
        else if (controllerType == controller.ai)
        {
            if (AIController.instance.GetSelectedCharacter() != null)
                AIController.instance.GetSelectedCharacter().isSelected = false;

            AIController.instance.SetSelectedCharacter(this);
        }    
        else
            throw new Exception("No controller assigned to this character.");

        Tile.ResetTiles?.Invoke();
        OnShouldUpdateTiles?.Invoke((int)(moveRange * RangeMultiplier), startCoordinates);
        isSelected = true;

        UISetup();
    }

    /// <summary>
    /// Sets up player action UI;
    /// </summary>
    private void UISetup()
    {
        if (aiControlled == true)
            return;

        PlayerController.instance.GetAttackButton().onClick.RemoveAllListeners();
        PlayerController.instance.GetWaitButton().onClick.RemoveAllListeners();

        PlayerController.instance.GetActionUI().SetActive(true);
        PlayerController.instance.GetAttackButton().onClick.AddListener(OpenAttackSelection);
        PlayerController.instance.GetWaitButton().onClick.AddListener(Wait);
    }

    /// <summary>
    /// Listen to the tile selected action to know when to move. This function should
    /// only run when the game is in the state to let it. Based on UI imput and turns
    /// and such thats not implemented yet.
    /// </summary>
    /// <param name="input"></param>
    private void MoveOrAttack(Tile input)
    {
        if (isSelected == false)
            return;

        if (input.GetTileState() == Tile.TileState.moveable && isTileAttack == false)
        {
            //move actor to tile
            MoveCharacter(input);
        }
        else if (input.GetTileState() == Tile.TileState.attackable)
        {
            Attack(input);
        }
        else
        {
            AttackTile(input);
        }

        Tile.ResetTiles?.Invoke();
    }

    /// <summary>
    /// Moves the actor to a new tile and updates the fields on old and new tile
    /// </summary>
    /// <param name="input"></param>
    public void MoveCharacter(Tile input)
    {
        curTile.SetIsOccupied(false);
        curTile.SetOccupyingCharacter(null);
        transform.position = input.GetTileActorAnchor().position;
        input.SetIsOccupied(true);
        input.SetOccupyingCharacter(this);
        curTile = input;

        if (controllerType == controller.ai)
            DeactivateCharacter();
    }

    /// <summary>
    /// Activates attack highlights and sets tile states to be attackable.
    /// </summary>
    /// <param name="range"></param>
    /// <param name="originTile"></param>
    public void OpenAttackSelection()
    {
        attacking = true;
        PlayerController.instance.ConstructUI(attacks);
    }

    /// <summary>
    /// Selects an active attack.
    /// </summary>
    public void SelectAttack()
    {
        OnAttackPressed?.Invoke(attackRange, curTile.GetCoordinates());
    }

    /// <summary>
    /// Exits attacking state.
    /// </summary>
    public void CloseAttackSelection()
    {
        attacking = false;
        curAbility = null;
        isTileAttack = false;

        OnShouldUpdateTiles?.Invoke((int)(moveRange * RangeMultiplier), curTile.GetCoordinates());
        PlayerController.instance.DestroyUI();
    }

    /// <summary>
    /// Performs attack action.
    /// </summary>
    private void Attack(Tile input)
    {
        if(input.GetTileState() == Tile.TileState.attackable && curAbility != null)
        {
            //Debug.Log("ATTACKING");
            curAbility.TriggerAbility(input, DamageMultiplier);
            DeactivateCharacter();
            PlayerController.instance.DestroyUI();
        }
        else if(curAbility != null)
        {
            Debug.Log("NOT AN ATTACKABLE CHARACTER");
        }
    }

    private void AttackTile(Tile input)
    {
        if (isTileAttack == true && curAbility != null)
        {
            Debug.Log("ATTACKING");
            //CALL TILE ATTACK FUNCTIONALITY
            curAbility.TriggerAbility(input);
            isTileAttack = false;
            DeactivateCharacter();
            PlayerController.instance.DestroyUI();
        }
        else
        {
            Debug.Log("NOT AN ATTACKABLE CHARACTER");
            attacking = false;
        }
    }

    /// <summary>
    /// Tells a character to wait without performing any actions.
    /// </summary>
    public void Wait()
    {
        DeactivateCharacter();
        Tile.ResetTiles?.Invoke();
    }

    /// <summary>
    /// Checks whether a combo should be executed or if an affliction should be applied.
    /// </summary>
    public void CheckCombo()
    {
        throw new NotImplementedException();
    }

    private void OnMouseUpAsButton()
    {
        if (canAct == true && !EventSystem.current.IsPointerOverGameObject())
            SelectCharacter();
    }

    public void DamageCharacter(int damage, Ability.AbilityType type)
    {
        var text = Instantiate(damageTextPrefab, transform.position, Quaternion.identity);
        text.GetComponent<TextRise>().StartRise(damage);
        AddEffect(type);
    }

    /// <summary>
    /// Adds an effect to this character.
    /// </summary>
    /// <param name="type"></param>
    public void AddEffect(Ability.AbilityType type)
    {
        if (affectedAbility != Ability.AbilityType.None && type != Ability.AbilityType.None)
        {
            ComboCodex.Instance.AddCombo(affectedAbility, type, gameObject);
            affectedAbility = Ability.AbilityType.None;
        }
        else
        {
            affectedAbility = type;
            if (type != Ability.AbilityType.None)
                Instantiate(ComboCodex.Instance.GetAbilityVFX(type), transform);
        }
    }

    /// <summary>
    /// Checks if tile has a combo component, and triggers the combo.
    /// </summary>
    private void TryTriggerCombo()
    {
        if (TryGetComponent(out Combo combo))
        {
            combo.TriggerCombo();
            //Debug.Log("Combo Triggered");
        }
    }

    private void SetStartCoordinates()
    {
        startCoordinates = curTile.GetCoordinates();
    }

    private void OnDestroy()
    {
        
    }
}

