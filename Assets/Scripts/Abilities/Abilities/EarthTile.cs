using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EarthTile", menuName = "ScriptableObjects/EarthTile")]

public class EarthTile : Ability
{
    public override void TriggerAbility(Tile input)
    {
        input.AddEffect(ThisAbility);
    }
}
