using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormCloud : Combo
{
    int turnDuration = 3;
    int turnCount = 0;

    int damageAmount = 10;

    void Awake()
    {
        TriggerCombo();
    }

    public override void TriggerCombo()
    {
        //TODO: Get Character this sctipt is on and call damage function.

        turnCount++;

        if (turnCount == turnDuration)
        {
            Destroy(this);
        }
    }
}
