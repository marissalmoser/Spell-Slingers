/******************************************************************
*    Author: Marissa Moser
*    Contributors: 
*    Date Created: April 7, 2025
*    Description: At the beginning of enemies turn they get damaged by lightning
        (Lasts 3 rounds, Deals small damage).

*******************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormCloud : Combo
{
    int turnDuration = 3;
    int turnCount = 0;

    int damageAmount = 10;

    Tile tile;

    void Awake()
    {
        tile = GetComponent<Tile>();
        TriggerCombo();
    }

    public override void TriggerCombo()
    {
        //gets current character on tile
        Character character = null;
        if(tile != null)
        {
            character = tile.GetOccupyingCharacter();
        }
        //damages character
        if(character != null && character.ControllerType == Character.controller.ai)
        {
            character.DamageCharacter(damageAmount);
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
