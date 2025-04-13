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
    Character[] allController;
    Character[] ally;

    // Start is called before the first frame update
    void Awake()
    {
        enemy = GetComponent<Character>();
        allController = FindObjectsOfType<Character>();
        foreach (Character c in allController)
        {
            int nextFree = 0;
            if (c.ControllerType == Character.controller.player)
            {
                ally[nextFree] = c;
                nextFree++;
            }
        }
        TriggerCombo();
    }

    public override void TriggerCombo()
    {
        enemy.DamageCharacter(Damage, Ability.AbilityType.None);
        for (int i = 0; i < ally.Length; i++)
        {
            ally[i].DamageCharacter(-Damage / 4, Ability.AbilityType.None);
        }

        turnCount++;

        if (turnCount == turnDuration)
        {
            Destroy(this);
        }
    }
}

