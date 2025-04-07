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
        enemy = GetComponent<Character>();

        savedMovementRange = enemy.GetMovementRange();

        TriggerCombo();
    }

    public override void TriggerCombo()
    {
        enemy.SetMovementRange(0);

        turnCount++;

        if (turnCount == turnDuration)
        {
            enemy.SetMovementRange(savedMovementRange);
            Destroy(this);
        }
    }
}
