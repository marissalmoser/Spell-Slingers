using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject tutorialObj;
    [SerializeField] private TextMeshProUGUI textBox;

    [SerializeField] private string[] tutorialText;

    /// <summary>
    /// Runs initial setup for the tutorial.
    /// </summary>
    public void Init()
    {
        //Set listeners for actions
        Character.OnPlayerSelected += CharacterClicked;
        Character.OnAttacksOpened += AttackClicked;
        Character.OnAttackUsed += AttackUsed;
        GameManager.OnPlayerTurnEnd += EnemyTurn;
        GameManager.OnEnemyTurnEnd += EndTutorial;

        //Fill text box with first tutorial info
        textBox.text = tutorialText[0];
    }

    private void CharacterClicked()
    {
        textBox.text = tutorialText[1];
        Character.OnPlayerSelected -= CharacterClicked;
    }    

    private void AttackClicked()
    {
        textBox.text = tutorialText[2];
        Character.OnAttacksOpened -= AttackClicked;
    }

    private void AttackUsed()
    {
        textBox.text = tutorialText[3];
        Character.OnAttackUsed -= AttackUsed;
    }

    private void EnemyTurn()
    {
        textBox.text = tutorialText[4];
        Character.OnPlayerSelected += EnemyClicked;
        GameManager.OnPlayerTurnEnd -= EnemyTurn;
    }

    private void EnemyClicked()
    {
        textBox.text = tutorialText[5];
        Character.OnPlayerSelected -= EnemyClicked;
        Character.OnCharacterMoved += FinishEnemyTurn;
    }

    private void FinishEnemyTurn()
    {
        textBox.text = tutorialText[6];
        Character.OnCharacterMoved -= FinishEnemyTurn;
    }

    private void EndTutorial()
    {
        tutorialObj.SetActive(false);
        GameManager.OnEnemyTurnEnd -= EndTutorial;
        Character.OnPlayerSelected -= CharacterClicked;
        Character.OnAttacksOpened -= AttackClicked;
        Character.OnAttackUsed -= AttackUsed;
        GameManager.OnPlayerTurnEnd -= EnemyTurn;
    }
}
