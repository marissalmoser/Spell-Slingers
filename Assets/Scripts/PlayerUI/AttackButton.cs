using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackButton : MonoBehaviour
{
    public int attackIndex = 0;

    public void SetAttackOnCharacter()
    {
        PlayerController.instance.GetSelectedCharacter().SetCurrentAttack(attackIndex);
        PlayerController.instance.GetSelectedCharacter().SelectAttack();
    }
}
