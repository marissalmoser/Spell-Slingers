using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : Controller
{
    public static PlayerController instance;

    private Character selectedCharacter;

    [SerializeField] private GameObject actionUI;
    [SerializeField] private Button attackBtn;
    [SerializeField] private Button waitBtn;

    #region Instance Var Setup

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void OnDestroy()
    {
        instance = null;
    }

    #endregion

    #region Getters and Setters

    public Character GetSelectedCharacter()
    {
        return selectedCharacter;
    }

    public void SetSelectedCharacter(Character character)
    {
        selectedCharacter = character;
    }

    public GameObject GetActionUI()
    {
        return actionUI;
    }

    public Button GetAttackButton()
    {
        return attackBtn;
    }

    public Button GetWaitButton()
    {
        return waitBtn;
    }

    #endregion

    /// <summary>
    /// Activate characters and start turns.
    /// </summary>
    public override void StartTurn()
    {
        List<Character> skippedCharacters = new List<Character>();

        foreach(Character c in GetControlledCharacters())
        {
            if (c.skipTurn == true)
            {
                c.skipTurn = false;
                skippedCharacters.Add(c);
                continue;
            }

            c.ActivateCharacter();
        }

        foreach(Character c in skippedCharacters)
        {
            GetControlledCharacters().Remove(c);
        }
    }

    public override void EndTurn()
    {
        foreach (Character c in GetControlledCharacters())
        {
            c.DeactivateCharacter();
        }
    }
}
