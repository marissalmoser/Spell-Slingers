using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FireAttack", menuName = "ScriptableObjects/FireAttack")]
public class FireAttack : Ability
{
    public override void TriggerAbility(Tile input)
    {
        SoundManager.instance.PlayUniversalOneShotSound("fireselect");

        base.TriggerAbility(input);

        if(input.GetOccupyingCharacter().gameObject.TryGetComponent(out Character ch))
        {
            ch.DamageCharacter(-5, AbilityType.FireAttack);
            Debug.Log("FIRE ATTACK");
        }
    }

    public override void TriggerAbility(Tile input, int damageMultiplier)
    {
        SoundManager.instance.PlayUniversalOneShotSound("fireselect");

        base.TriggerAbility(input);

        if (input.GetOccupyingCharacter().gameObject.TryGetComponent(out Character ch))
        {
            ch.DamageCharacter(-5 * damageMultiplier, AbilityType.FireAttack);
        }
    }
}
