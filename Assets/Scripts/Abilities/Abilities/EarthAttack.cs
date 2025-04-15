using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EarthAttack", menuName = "ScriptableObjects/EarthAttack")]
public class EarthAttack : Ability
{
    public override void TriggerAbility(Tile input)
    {
        base.TriggerAbility(input);

        if (input.GetOccupyingCharacter().gameObject.TryGetComponent(out Character ch))
        {
            ch.DamageCharacter(-10, AbilityType.EarthAttack);
        }
    }

    public override void TriggerAbility(Tile input, int damageMultiplier)
    {
        base.TriggerAbility(input);

        if (input.GetOccupyingCharacter().gameObject.TryGetComponent(out Character ch))
        {
            ch.DamageCharacter(-10 * damageMultiplier, AbilityType.EarthAttack);
        }
    }
}
