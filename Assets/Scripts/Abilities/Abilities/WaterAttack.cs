using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaterAttack", menuName = "ScriptableObjects/WaterAttack")]
public class WaterAttack : Ability
{
    public override void TriggerAbility(Tile input)
    {
        SoundManager.instance.PlayUniversalOneShotSound("waterselect");

        base.TriggerAbility(input);

        if (input.GetOccupyingCharacter().gameObject.TryGetComponent(out Character ch))
        {
            ch.DamageCharacter(-5, AbilityType.WaterAttack);
        }
    }

    public override void TriggerAbility(Tile input, int damageMultiplier)
    {
        SoundManager.instance.PlayUniversalOneShotSound("waterselect");

        base.TriggerAbility(input);

        if (input.GetOccupyingCharacter().gameObject.TryGetComponent(out Character ch))
        {
            ch.DamageCharacter(-5 * damageMultiplier, AbilityType.WaterAttack);
        }
    }
}
