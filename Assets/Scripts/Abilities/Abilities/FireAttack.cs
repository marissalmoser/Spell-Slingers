using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FireAttack", menuName = "ScriptableObjects/FireAttack")]
public class FireAttack : Ability
{
    public override void TriggerAbility(Tile input)
    {
        Debug.Log("FIRE ATTACK");
        base.TriggerAbility(input);

        if(input.TryGetComponent(out Character ch))
        {
            ch.DamageCharacter(10, AbilityType.FireAttack);
        }
    }
}
