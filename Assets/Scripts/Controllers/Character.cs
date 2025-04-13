using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Character : MonoBehaviour
{
    private bool isSelected = false;
    private bool attacking = false;
    public bool skipTurn;

    [Header("Gameplay Values")]
    [SerializeField] private int moveRange;
    [SerializeField] private int attackRange;
    public float DamageMultiplier = 1;
    public float RangeMultiplier = 1;

    [Header("Programming Values")]
    public bool canAct = false;
    public Tile curTile;
    [SerializeField] private bool aiControlled;

    [SerializeField] private Ability[] attacks;
    private Ability curAbility;

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

    #region OnEnableOnDisable

    private void OnEnable()
    {
        //OnPlayerSelected += SelectCharacter;

        //player should not be listening to when a tile is selected but alas
        Tile.TileSelected += MoveOrAttack;

        curTile.SetIsOccupied(true);
        curTile.SetOccupyingCharacter(this);
    }

    private void OnDisable()
    {
        OnPlayerSelected -= SelectCharacter;
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

    public void SetCurrentAttack(int attackIndex)
    {
        curAbility = attacks[attackIndex];
    }

    #region Activate and Deactivate Character

    /// <summary>
    /// Activates a character.
    /// </summary>
    public void ActivateCharacter()
    {
        canAct = true;
    }

    /// <summary>
    /// Deactivates a character.
    /// </summary>
    public void DeactivateCharacter()
    {
        canAct = false;
        isSelected = false;

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
        OnShouldUpdateTiles?.Invoke(moveRange, curTile.GetCoordinates());
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

        if (input.GetTileState() == Tile.TileState.moveable)
        {
            //move actor to tile
            MoveCharacter(input);
        }
        else if (input.GetTileState() == Tile.TileState.attackable)
        {
            Attack(input);
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

        OnShouldUpdateTiles?.Invoke(moveRange, curTile.GetCoordinates());
        PlayerController.instance.DestroyUI();
    }

    /// <summary>
    /// Performs attack action.
    /// </summary>
    private void Attack(Tile input)
    {
        if(input.GetTileState() == Tile.TileState.attackable && curAbility != null)
        {
            Debug.Log("ATTACKING");
            currentlyAttacking = input.GetOccupyingCharacter().gameObject;
            curAbility.TriggerAbility();
            DeactivateCharacter();
            PlayerController.instance.DestroyUI();
        }
        else
        {
            Debug.Log("NOT AN ATTACKABLE CHARACTER");
        }
    }

    /// <summary>
    /// Tells a character to wait without performing any actions.
    /// </summary>
    public void Wait()
    {
        DeactivateCharacter();
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
        if (canAct == true)
            SelectCharacter();
    }

    public void DamageCharacter(int damage)
    {
        var text = Instantiate(damageTextPrefab, transform.position, Quaternion.identity);
        text.GetComponent<TextRise>().StartRise(damage);
    }

    private void OnDestroy()
    {
        
    }

    public int GetMovementRange()
    {
        return moveRange;
    }

    public void SetMovementRange(int range)
    {
        moveRange = range;
    }

}

