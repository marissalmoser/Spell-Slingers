using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Character : MonoBehaviour
{
    private bool isSelected = false;

    [Header("Gameplay Values")]
    [SerializeField] private int moveRange;

    [Header("Programming Values")]
    public bool canAct = false;
    public Tile curTile;

    //Ability[] attacks;

    [SerializeField] private controller controllerType;
    //Set an enum for elemental afflictions to check against combos

    public enum controller { none, player, ai}

    public static Action OnPlayerSelected;
    public static Action<int, Vector2Int> OnShouldUpdateTiles;

    public controller ControllerType { get => controllerType; }

    #region OnEnableOnDisable

    private void OnEnable()
    {
        //OnPlayerSelected += SelectCharacter;

        //player should not be listening to when a tile is selected but alas
        Tile.TileSelected += MoveOrAttack;

        curTile.SetIsOccupied(true);
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        else
            throw new Exception("No controller assigned to this character.");
    }

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
    }

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
            throw new NotImplementedException();
        else
            throw new Exception("No controller assigned to this character.");

        Tile.ResetTiles?.Invoke();
        OnShouldUpdateTiles?.Invoke(moveRange, curTile.GetCoordinates());
        isSelected = true;
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

    private void OnDestroy()
    {
        if(PlayerController.instance.GetControlledCharacters().Contains(this))
            PlayerController.instance.RemoveControlledCharacter(this);
    }

    #region Temp Functions Until UI is In

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
            MoveActor(input);
        }
        else if (input.GetTileState() == Tile.TileState.attackable)
        {
            //call attack here
            print("Attack!");
        }

        Tile.ResetTiles?.Invoke();
        DeactivateCharacter();
    }

    /// <summary>
    /// Moves the actor to a new tile and updates the fields on old and new tile
    /// </summary>
    /// <param name="input"></param>
    private void MoveActor(Tile input)
    {
        curTile.SetIsOccupied(false);
        transform.position = input.GetTileActorAnchor().position;
        input.SetIsOccupied(true);
        curTile = input;
    }

    #endregion
}
