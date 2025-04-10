using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn : Combo
{
    public int Damage;

    private int turnDuration = 3;
    private int turnCount = 0;

    Character enemy;

    void Awake()
    {
        enemy = GetComponent<Character>();

        TriggerCombo();
    }

    public override void TriggerCombo()
    {
        enemy.DamageCharacter(Damage);

        turnCount++;

        if (turnCount == turnDuration)
        {
            Destroy(this);
        }
    }
}
