using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaterAttack", menuName = "ScriptableObjects/WaterAttack")]
public class WaterAttack : Ability
{
    public override void TriggerAbility(Tile input)
    {
        base.TriggerAbility(input);

        if (input.GetOccupyingCharacter().gameObject.TryGetComponent(out Character ch))
        {
            ch.DamageCharacter(-10, AbilityType.WaterAttack);
        }
    }

    public override void TriggerAbility(Tile input, int damageMultiplier)
    {
        base.TriggerAbility(input);

        if (input.GetOccupyingCharacter().gameObject.TryGetComponent(out Character ch))
        {
            ch.DamageCharacter(-10 * damageMultiplier, AbilityType.WaterAttack);
        }
    }
}
