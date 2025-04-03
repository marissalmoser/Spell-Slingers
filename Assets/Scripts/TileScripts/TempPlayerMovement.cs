/******************************************************************
*    Author: Marissa Moser
*    Contributors: 
*    Date Created: April 2, 2025
*    Description: A temprary script to model how the actor movement should work. There
*       is a lot of functionality in here that should not end up in the actor script,
*       but I dodn;t want to make an additonal temp script to is all together here.
*       Ask Marissa with any questions!
*******************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlayerMovement : MonoBehaviour
{
    [SerializeField] Tile startTile;
    [SerializeField] int moveRange = 4;

    //kinda jank solution to knowing which player is selected
    bool isSelected = false;

    Tile currentTile;
    
    //should be changed to the character class when we have one.
    public static Action<int, Vector2Int> PlayerSelected;

    private void OnMouseUpAsButton()
    {
        //clear current setup when player selects a new actor
        Tile.ResetTiles?.Invoke();

        //display new character's range
        PlayerSelected?.Invoke(moveRange, currentTile.GetCoordinates());
        isSelected = true;
    }

    private void OnEnable()
    {
        //player should not be listening to when a tile is selected but alas
        Tile.TileSelected += MoveOrAttack;
        Tile.ResetTiles += ClearSelectedActor;

        startTile.SetIsOccupied(true);
        currentTile = startTile;
    }
    private void OnDisable()
    {
        Tile.TileSelected -= MoveOrAttack;
        Tile.ResetTiles -= ClearSelectedActor;
    }

    /// <summary>
    /// Listen to the tile selected action to know when to move. This function should
    /// only run when the game is in the state to let it. Based on UI imput and turns
    /// and such thats not implemented yet.
    /// </summary>
    /// <param name="input"></param>
    private void MoveOrAttack(Tile input)
    {
        //only the selected player should be doing anything
        if (!isSelected)
        {
            return;
        }

        if (input.GetTileState() == Tile.TileState.moveable)
        {
            //move actor to tile
            MoveActor(input);
        }
        else if(input.GetTileState() == Tile.TileState.attackable)
        {
            //call attack here
            print("Attack!");
        }

        Tile.ResetTiles?.Invoke();
        isSelected = false;
    }

    /// <summary>
    /// Moves the actor to a new tile and updates the fields on old and new tile
    /// </summary>
    /// <param name="input"></param>
    private void MoveActor(Tile input)
    {
        currentTile.SetIsOccupied(false);
        transform.position = input.GetTileActorAnchor().position;
        input.SetIsOccupied(true);
        currentTile = input;
    }

    //again a jank solution to unselecting the current actor
    private void ClearSelectedActor()
    {
        isSelected = false;
    }
}
