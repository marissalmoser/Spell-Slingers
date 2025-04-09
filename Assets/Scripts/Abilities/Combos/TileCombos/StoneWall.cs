/******************************************************************
*    Author: Marissa Moser
*    Contributors: 
*    Date Created: April 9, 2025
*    Description: Removes tile, and the one above and below from view to block
*       enemies Attack and movement
        (Lasts 3 turns, Blocks enemy)

        if a tile is occupied, nothing happens to the tile

*******************************************************************/

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StoneWall : Combo
{

    Tile tile;
    //List<Tile> = new Tile

    private void Awake()
    {
        tile = GetComponent<Tile>();

        //find all tiles in range
        Tile[] tiles = tile.GetTilesInRadius(1);

        //loop thru tiles and if has an enemy occupying character on it, damage
        foreach (Tile tile in tiles)
        {
            //if tile is not occupied and is in line with this tile
            if (tile.GetOccupyingCharacter() != null
                && this.tile.GetCoordinates().x == tile.GetCoordinates().x)
            {
                
            }
        }
    }
}
//if x corrdinate is the same