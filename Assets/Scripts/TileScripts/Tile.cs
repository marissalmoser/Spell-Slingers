/******************************************************************
*    Author: Marissa Moser
*    Contributors: 
*    Date Created: April 2, 2025
*    Description: The functionality for each tile. This script includes
*       functionality concerning the tile's state and effect.
*******************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[SelectionBase]
public class Tile : MonoBehaviour
{
    /// utility
    [SerializeField] Vector2Int coordinates;
    [SerializeField] Transform actorAnchor;
    [SerializeField] GameObject moveableStatus;
    [SerializeField] GameObject attackableStatus;
    [SerializeField] bool isOccupied;

    private Character occupyingCharacter;

    ///tile state
    public enum TileState { idle, moveable, attackable };
    private TileState currentState = TileState.idle;
    //TODO: add tile effect field

    ///actions
    public static Action<Tile> TileSelected;
    public static Action ResetTiles;

    private Ability.AbilityType affectedAbility;

    #region Getters and Setters

    /// <summary>
    /// Sets the corrdinates of the tile. Should only be used by the tile manager.
    /// </summary>
    public void SetCoordinates(int x, int y)
    {
        coordinates = new Vector2Int(x, y);
        print(coordinates);
    }

    /// <summary>
    /// Sets the tile to the idle state
    /// </summary>
    public void SetToIdle()
    {
        SetState(TileState.idle);
    }

    /// <summary>
    /// Gets the current tile state.
    /// </summary>
    public TileState GetTileState()
    {
        return currentState;
    }

    public Transform GetTileActorAnchor()
    {
        return actorAnchor;
    }

    public Vector2Int GetCoordinates()
    {
        return coordinates;
    }

    public void SetIsOccupied(bool input)
    {
        isOccupied = input;
    }

    public bool GetIsOccupied()
    {
        return isOccupied;
    }

    public Character GetOccupyingCharacter()
    {
        return occupyingCharacter;
    }

    public void SetOccupyingCharacter(Character character)
    {
        occupyingCharacter = character;
    }

    #endregion

    #region OnEnable and Disable

    private void OnEnable()
    {
        ResetTiles += SetToIdle;

        //temporary, this call should come from jacob's stuff
        TempPlayerMovement.PlayerSelected += UpdateTile;

        Character.OnShouldUpdateTiles += UpdateTile;
        Character.OnAttackPressed += SetAttackableState;

        GameManager.OnTurnStart += TryTriggerCombo;
    }


    private void OnDisable()
    {
        ResetTiles -= SetToIdle;

        //temporary, this call should come from jacob's stuff
        TempPlayerMovement.PlayerSelected -= UpdateTile;

        Character.OnShouldUpdateTiles -= UpdateTile;
        Character.OnAttackPressed -= SetAttackableState;

        GameManager.OnTurnStart -= TryTriggerCombo;
    }

    #endregion

    private void OnMouseUpAsButton()
    {
        if(currentState != TileState.idle)
        {
            TileSelected?.Invoke(this);
        }
    }
    
    /// <summary>
    /// Call this function to add an effect to this tile. This function will evaluate 
    /// if it should call the ability attack or combo and set the visuals accordingly.
    /// </summary>
    public void AddEffect(Ability.AbilityType type)
    {
        if (affectedAbility != Ability.AbilityType.None && type != Ability.AbilityType.None)
        {
            ComboCodex.Instance.AddCombo(affectedAbility, type, gameObject);
            affectedAbility = Ability.AbilityType.None;
        }
        else if(type != Ability.AbilityType.None)
        {
            affectedAbility = type;
        }
    }

    /// <summary>
    /// This function will update the tile's current state and visuals
    /// </summary>
    /// <param name="newState">State to change tile to</param>
    private void SetState(TileState newState)
    {
        //remove all visuals
        moveableStatus.SetActive(false);
        attackableStatus.SetActive(false);

        //set state and needed visuals
        switch (newState)
        {
            case TileState.idle:
                currentState = TileState.idle;
                break;

            case TileState.moveable:
                moveableStatus.SetActive(true);
                currentState = TileState.moveable;
                break;

            /*case TileState.attackable:
                attackableStatus.SetActive(true);
                currentState = TileState.attackable;
                break;*/
        }
    }

    /// <summary>
    /// Sets a tile to the attackable state.
    /// </summary>
    /// <param name="range"></param>
    /// <param name="originTile"></param>
    public void SetAttackableState(int range, Vector2Int originTile)
    {
        if (originTile == coordinates)
            return;

        moveableStatus.SetActive(false);
        attackableStatus.SetActive(false);

        Vector2 distance = coordinates - originTile;
        float magnitude = distance.magnitude;

        if(magnitude <= range + 1 && isOccupied && occupyingCharacter.ControllerType == Character.controller.ai)
        {
            attackableStatus.SetActive(true);
            currentState = TileState.attackable;
        }
    }

    /// <summary>
    /// Determines if the input tile is in range, and if it should get a state change. 
    ///     
    ///     TEMP: Listens to action in TempPlayerMovement for when it is selected.
    ///         This will likely be changed to somewhere in Jacob's structure.
    ///     
    /// </summary>
    /// <param name="range"></param>
    /// <param name="oroginTile"></param>
    public void UpdateTile(int range, Vector2Int originTile)
    {
        //prevent's actor's tile from changing state
        if (originTile == coordinates)
        {
            return;
        }

        Vector2 distance = coordinates - originTile;
        float magnitude = distance.magnitude;

        //sets tiles in range to moveable
        if(magnitude <= range && !isOccupied)
        {
            SetState(TileState.moveable);
        }
        //sets valid tiles in range to attackable
        if (magnitude <= range + 1 && isOccupied)
        {
            SetState(TileState.attackable);
        }
    }

    /// <summary>
    /// Returns tiles in range of this tile in a square area at the input range.
    /// </summary>
    public List<Tile> GetTilesInRadius(int range)
    {
        Vector3 rangeVec = new Vector3(range, range, range);
        Collider[] colliders =  Physics.OverlapBox(transform.position, rangeVec, Quaternion.identity, 1 << 3);
        List<Tile> tiles = new();
        
        foreach(Collider col in colliders)
        {
            tiles.Add(col.gameObject.GetComponent<Tile>());
        }

        return tiles;
    }

    /// <summary>
    /// Checks if tile has a combo component, and triggers the combo.
    /// </summary>
    private void TryTriggerCombo()
    {
        if(TryGetComponent(out Combo combo))
        {
            combo.TriggerCombo();
            Debug.Log("Combo Triggered");
        }
    }

}


