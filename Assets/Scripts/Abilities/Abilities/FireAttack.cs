using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FireAttack", menuName = "ScriptableObjects/FireAttack")]
public class FireAttack : Ability
{
    public override void TriggerAbility(Tile input)
    {
        base.TriggerAbility(input);

        if(input.GetOccupyingCharacter().gameObject.TryGetComponent(out Character ch))
        {
            ch.DamageCharacter(-10, AbilityType.FireAttack);
            Debug.Log("FIRE ATTACK");
        }
    }

    public override void TriggerAbility(Tile input, int damageMultiplier)
    {
        base.TriggerAbility(input);

        if (input.GetOccupyingCharacter().gameObject.TryGetComponent(out Character ch))
        {
            ch.DamageCharacter(-10 * damageMultiplier, AbilityType.FireAttack);
        }
    }
}
