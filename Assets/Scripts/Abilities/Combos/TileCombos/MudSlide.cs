/******************************************************************
*    Author: Marissa Moser
*    Contributors: 
*    Date Created: April 9, 2025
*    Description: An AoE attack that slows enemies in a square area
        (Lasts 3 rounds, Slows Enemies)

*******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudSlide : Combo
{
    int aoeRange = 3;

    int turnDuration = 3;
    int turnCount = 0;

    Tile tile;
    List<Character> affectedCharacters = new();

    private void Awake()
    {
        tile = GetComponent<Tile>();

        //find all tiles in range
        List<Tile> tiles = tile.GetTilesInRadius(aoeRange);

        //loop thru tiles and if has an enemy character occupying it, affect it
        foreach (Tile tile in tiles)
        {
            Character ch = tile.GetOccupyingCharacter();

            if (ch != null && ch.ControllerType == Character.controller.ai)
            {
                ch.RangeMultiplier = 0.5f;
                affectedCharacters.Add(ch);
            }
        }

        //Advance turn
        TriggerCombo();
    }

    public override void TriggerCombo()
    {
        turnCount++;

        if (turnCount >= turnDuration)
        {
            //reset character speeds
            foreach (Character ch in affectedCharacters)
            {
                ch.RangeMultiplier = 1;
            }

            Destroy(this);
        }
    }
}
