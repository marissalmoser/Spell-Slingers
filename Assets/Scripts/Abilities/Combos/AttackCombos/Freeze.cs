using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : Combo
{
    Character enemy;
    void Awake()
    {
        enemy = GetComponent<Character>();
        SoundManager.instance.PlayUniversalOneShotSound("freeze");
        enemy.skipTurn = true;
    }

    public override void TriggerCombo()
    {

        EndCombo();
    }
}
