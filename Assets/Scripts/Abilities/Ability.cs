using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "ScriptableObjects/Ability")]
public class Ability : ScriptableObject
{
    public string abilityName;

    public enum AbilityType{
        None,
        FireAttack,
        WaterAttack,
        EarthAttack,
        FireTile,
        WaterTile,
        EarthTile
    }

    public AbilityType ThisAbility;

    public virtual void TriggerAbility(Tile input)
    {
        //TODO: a little bit of damage
    }
}
