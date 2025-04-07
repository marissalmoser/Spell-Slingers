using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class VampireSeed : Combo
{
    public int Damage;

    private int turnDuration = 3;
    private int turnCount = 0;

    Character enemy;
    Character ally1, ally2, ally3;

    // Start is called before the first frame update
    void Awake()
    {
        enemy = GetComponent<Character>();
        //set ally Characters

        TriggerCombo();
    }

    public override void TriggerCombo()
    {
        enemy.DamageCharacter(Damage);
        //ally1.DamageCharacter(-.3 * Damage);
        //ally2.DamageCharacter(-.3 * Damage);
        //ally3.DamageCharacter(-.3 * Damage);

        turnCount++;

        if (turnCount == turnDuration)
        {
            Destroy(this);
        }
    }
}

