using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scald : Combo
{
    public int Damage;
    
    Character enemy;

    void Awake()
    {
        enemy = GetComponent<Character>();

        TriggerCombo();
    }

    public override void TriggerCombo()
    {
        enemy.DamageCharacter(Damage, Ability.AbilityType.None);

        EndCombo();
    }
}

