/******************************************************************
*    Author: Marissa Moser
*    Contributors: 
*    Date Created: April 9, 2025
*    Description: On enemies' turn they have to run out of the steam cloud in a
*       random direction and end their turn.
        (Lasts one turn, Scares enemy)

*******************************************************************/

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Steam : Combo
{
    Tile tile;

    public override void TriggerCombo()
    {
        Debug.Log("Steam Triggered");
        //if there is not a charcter on the tile, return
        tile = GetComponent<Tile>();
        if(tile == null || tile.GetOccupyingCharacter() == null)
            return; 

        //if the character is not an ai, return
        Character ch = tile.GetOccupyingCharacter();
        if(ch.ControllerType != Character.controller.ai)
            return;

        //get neighboring tiles to move to
        List<Tile> tiles = tile.GetTilesInRadius(1);

        //loop thru tiles and check if occupied
        for(int j = 0; j < tiles.Count; j++)
        {
            int index = 0;

            //if tile is occupied, remove from list of avaliable tiles
            //can check coordinates to make sure its in the immediate range
            if (tiles[j].GetIsOccupied())
            {
                tiles.Remove(tiles[j]);
            }
            else
            {
                index++;
            }
        }

        //move ai to unoccupied tile
        int i = Random.Range(0, tiles.Count);
        ch.MoveCharacter(tiles[i]);

        Destroy(this);
    }
}
