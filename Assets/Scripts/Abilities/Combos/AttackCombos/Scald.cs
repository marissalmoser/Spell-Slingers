using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scald : Combo
{
    public int Damage = -50;
    
    Character enemy;

    void Awake()
    {
        enemy = GetComponent<Character>();
        SoundManager.instance.PlayUniversalOneShotSound("scald");
        TriggerCombo();
    }

    public override void TriggerCombo()
    {
        enemy.DamageCharacter(Damage, Ability.AbilityType.None);

        EndCombo();
    }
}

