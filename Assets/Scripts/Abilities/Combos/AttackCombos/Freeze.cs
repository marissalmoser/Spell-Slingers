using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : Combo
{
    Character enemy;
    void Awake()
    {
        enemy = GetComponent<Character>();
        
        TriggerCombo();
    }

    public override void TriggerCombo()
    {
        enemy.skipTurn = true;

        EndCombo();
    }
}
