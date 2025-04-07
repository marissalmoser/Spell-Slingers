using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderThrow : Combo
{
    Character enemy;

    private void Awake()
    {
        enemy = GetComponent<Character>();

        TriggerCombo();
    }

    public override void TriggerCombo()
    {
        //call knockback function, knock enemy back in general direction 3 tiles

        Destroy(this);
    }
}
