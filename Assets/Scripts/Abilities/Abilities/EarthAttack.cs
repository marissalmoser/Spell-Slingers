using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EarthAttack", menuName = "ScriptableObjects/EarthAttack")]
public class EarthAttack : Ability
{
    public override void TriggerAbility(Tile input)
    {
        SoundManager.instance.PlayUniversalOneShotSound("earthselect");

        base.TriggerAbility(input);

        if (input.GetOccupyingCharacter().gameObject.TryGetComponent(out Character ch))
        {
            ch.DamageCharacter(-5, AbilityType.EarthAttack);
        }
    }

    public override void TriggerAbility(Tile input, int damageMultiplier)
    {
        SoundManager.instance.PlayUniversalOneShotSound("earthselect");

        base.TriggerAbility(input);

        if (input.GetOccupyingCharacter().gameObject.TryGetComponent(out Character ch))
        {
            ch.DamageCharacter(-5 * damageMultiplier, AbilityType.EarthAttack);
        }
    }
}
