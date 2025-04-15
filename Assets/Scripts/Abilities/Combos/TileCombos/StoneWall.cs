/******************************************************************
*    Author: Marissa Moser
*    Contributors: 
*    Date Created: April 9, 2025
*    Description: Removes tile, and the one above and below from view to block
*       enemies Attack and movement
        (Lasts 3 turns, Blocks enemy)

*******************************************************************/

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StoneWall : Combo
{
    int turnDuration = 3;
    int turnCount = 0;

    Tile tile;
    List<Tile> affectedTiles = new();

    private void Awake()
    {
        tile = GetComponent<Tile>();

        //find all tiles in range
        List<Tile> tiles = tile.GetTilesInRadius(1);

        GameManager.OnEnemyTurnEnd += IncrementCounter;

        //loop thru tiles in radius
        foreach (Tile tile in tiles)
        {
            //if tile is not occupied and is in line with this tile, add to affected list
            if (tile.GetOccupyingCharacter() == null
                && this.tile.GetCoordinates().x == tile.GetCoordinates().x)
            {
                affectedTiles.Add(tile);
            }
        }

        //disable affected tiles
        foreach(Tile tile in affectedTiles)
        {
            tile.gameObject.SetActive(false);
        }

        //Advance turn
        TriggerCombo();
    }

    private void OnDestroy()
    {
        GameManager.OnEnemyTurnEnd -= IncrementCounter;
    }

    public override void TriggerCombo()
    {
        GetComponent<ParticleSystem>().Play();
    }

    public void IncrementCounter()
    {
        //advance turn
        turnCount++;

        //check duration
        if (turnCount >= turnDuration)
        {
            //enable affected tiles
            foreach (Tile tile in affectedTiles)
            {
                tile.gameObject.SetActive(true);
            }

            EndCombo();
        }
    }
}