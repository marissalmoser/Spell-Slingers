using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaterTile", menuName = "ScriptableObjects/WaterTile")]

public class WaterTile : Ability
{
    public override void TriggerAbility(Tile input)
    {
        input.AddEffect(ThisAbility);
    }
}
