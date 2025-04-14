using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackButton : MonoBehaviour
{
    public int attackIndex = 0;
    public bool isTileAttack = false;

    public void SetAttackOnCharacter()
    {
        Character cur = PlayerController.instance.GetSelectedCharacter();

        cur.SetCurrentAttack(attackIndex);
        cur.SelectAttack();
        cur.SetIsTileAttack(isTileAttack);
    }
}
