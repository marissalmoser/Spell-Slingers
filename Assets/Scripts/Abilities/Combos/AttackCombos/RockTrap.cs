using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockTrap : Combo
{
    private int turnDuration = 3;
    private int turnCount = 0;

    Character enemy;

    private void Awake()
    {
        enemy = GetComponent<Character>();

        TriggerCombo();
    }

    public override void TriggerCombo()
    {
        //set enemy movement to 0 for three turns

        turnCount++;

        if (turnCount == turnDuration)
        {
            //Set movement back to saved int
            Destroy(this);
        }
    }
}
