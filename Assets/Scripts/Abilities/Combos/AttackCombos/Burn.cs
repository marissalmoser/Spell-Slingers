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
        GameManager.OnEnemyTurnEnd += IncrementCounter;
        TriggerCombo();
    }

    private void OnDestroy()
    {
        GameManager.OnEnemyTurnEnd -= IncrementCounter;
    }

    public override void TriggerCombo()
    {
        enemy.DamageCharacter(Damage, Ability.AbilityType.None);
    }

    public void IncrementCounter()
    {
        //advance turn
        turnCount++;

        //check duration
        if (turnCount >= turnDuration)
        {
            EndCombo();
        }
    }
}
