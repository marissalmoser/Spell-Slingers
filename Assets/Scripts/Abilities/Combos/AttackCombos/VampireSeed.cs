using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireSeed : Combo
{
    public int Damage;

    private int turnDuration = 3;
    private int turnCount = 0;

    Character enemy;
    Character[] allController;

    List<Character> ally = new();

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
                ally.Add(c);
                nextFree++;
            }
        }

        GameManager.OnEnemyTurnEnd += IncrementCounter;
        TriggerCombo();
    }

    private void OnDestroy()
    {
        GameManager.OnEnemyTurnEnd -= IncrementCounter;
    }

    public override void TriggerCombo()
    {
        enemy.DamageCharacter(-Damage, Ability.AbilityType.None);
        for (int i = 0; i < ally.Count; i++)
        {
            ally[i].DamageCharacter(Damage / 4, Ability.AbilityType.None);
        }
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

