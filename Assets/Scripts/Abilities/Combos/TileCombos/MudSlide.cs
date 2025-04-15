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
using System.Threading.Tasks;

public class MudSlide : Combo
{
    int aoeRange = 3;

    int turnDuration = 3;
    int turnCount = 0;

    Tile tile;
    List<Character> affectedCharacters = new();

    List<Tile> tiles;

    private void Awake()
    {
        tile = GetComponent<Tile>();

        //find all tiles in range
        tiles = tile.GetTilesInRadius(aoeRange);

        GameManager.OnEnemyTurnEnd += IncrementCounter;
        GameManager.OnTurnEnd += ResetSpeeds;
        GameManager.OnTurnStart += ApplyDebuffs;

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

    private void OnDestroy()
    {
        GameManager.OnEnemyTurnEnd -= IncrementCounter;
        GameManager.OnTurnEnd -= ResetSpeeds;
        GameManager.OnTurnStart -= ApplyDebuffs;
    }

    public override void TriggerCombo()
    {
        GetComponent<ParticleSystem>().Play();
    }

    private void ApplyDebuffs()
    {
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
    }

    private void ResetSpeeds()
    {
        //reset character speeds
        foreach (Character ch in affectedCharacters)
        {
            ch.RangeMultiplier = 1;
        }

        affectedCharacters.Clear();
    }

    public void IncrementCounter()
    {
        //advance turn
        turnCount++;

        //check duration
        if (turnCount >= turnDuration)
        {
            GameManager.OnTurnStart -= ApplyDebuffs;

            //reset character speeds
            foreach (Character ch in affectedCharacters)
            {
                ch.RangeMultiplier = 1;
            }

            affectedCharacters.Clear();

            EndCombo();
        }
    }
}