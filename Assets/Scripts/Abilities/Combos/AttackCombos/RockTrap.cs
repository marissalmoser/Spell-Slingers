using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockTrap : Combo
{
    private int turnDuration = 3;
    private int turnCount = 0;

    private int savedMovementRange;

    Character enemy;

    private void Awake()
    {
        enemy = gameObject.GetComponent<Character>();

        savedMovementRange = enemy.GetMovementRange();

        GameManager.OnEnemyTurnEnd += IncrementCounter;

        TriggerCombo();
    }

    private void OnDestroy()
    {
        enemy.SetMovementRange(savedMovementRange);
        GameManager.OnEnemyTurnEnd -= IncrementCounter;
    }

    public override void TriggerCombo()
    {
        enemy.SetMovementRange(0);
    }

    public void IncrementCounter()
    {
        //advance turn
        turnCount++;

        //check duration
        if (turnCount >= turnDuration)
        {
            //Fix this order.

            EndCombo();
        }
    }
}
