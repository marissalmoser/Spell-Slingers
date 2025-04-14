using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FireTile", menuName = "ScriptableObjects/FireTile")]

public class FireTile : Ability
{
    public override void TriggerAbility(Tile input)
    {
        input.AddEffect(ThisAbility);
    }
}
