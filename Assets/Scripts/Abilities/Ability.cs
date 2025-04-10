using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "ScriptableObjects/Ability")]
public class Ability : ScriptableObject
{
    public string abilityName;

    public enum AbilityType{
        FireAttack,
        WaterAttack,
        EarthAttack,
        FireTile,
        WaterTile,
        EarthTile
    }

    public AbilityType ThisAbility;

    public virtual void TriggerAbility()
    {
        //TODO: a little bit of damage
    }
}
