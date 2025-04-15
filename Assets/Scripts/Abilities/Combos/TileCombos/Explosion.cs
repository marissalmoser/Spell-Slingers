/******************************************************************
*    Author: Marissa Moser
*    Contributors: 
*    Date Created: April 9, 2025
*    Description: An AoE attack that deals damage to enemies in a square area.
        (One and done, Deals a lot of damage)

*******************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : Combo
{
    int aoeRange = 3;
    int damageAmount;

    Tile tile;

    private void Awake()
    {
        tile = GetComponent<Tile>();
        GetComponent<ParticleSystem>().Play();

        //find all tiles in range
        List<Tile> tiles = tile.GetTilesInRadius(aoeRange);

        //loop thru tiles and if has an enemy occupying character on it, damage
        foreach(Tile tile in tiles)
        {
            Character ch = tile.GetOccupyingCharacter();

            if(ch != null && ch.ControllerType == Character.controller.ai)
            {
                ch.DamageCharacter(damageAmount, Ability.AbilityType.None);
            }
        }
    }
}
