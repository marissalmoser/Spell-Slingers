/******************************************************************
*    Author: Marissa Moser
*    Contributors: 
*    Date Created: April 9, 2025
*    Description: Doubles damage for all friendly elementals on that tile
        (Lasts 3 rounds, doubles all damage)

*******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : Combo
{
    int turnDuration = 3;
    int turnCount = 0;

    Tile tile;

    void Awake()
    {
        tile = GetComponent<Tile>();
        TriggerCombo();
    }

    public override void TriggerCombo()
    {
        //gets character on tile that turn 
        Character character = null;
        if (tile != null)
        {
            character = tile.GetOccupyingCharacter();
        }
        //sets damage multiplier
        if (character != null && character.ControllerType == Character.controller.ai)
        {
            character.DamageMultiplier = 2;
        }

        //advance turn
        turnCount++;

        //check duration
        if (turnCount >= turnDuration)
        {
            Destroy(this);
        }
    }
}

//make sure charcter damage multiplier is reset at the end of every character's turn
