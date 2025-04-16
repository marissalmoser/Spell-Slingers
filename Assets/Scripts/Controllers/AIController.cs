using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Controller
{
    public static AIController instance;

    private Character selectedCharacter;

    #region Instance Var Setup

    private void Awake()
    {
        if (instance != null)
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

    #endregion

    /// <summary>
    /// Activate characters and start turns.
    /// </summary>
    public override void StartTurn()
    {
        ClearSkippedCharacters();

        foreach (Character c in GetControlledCharacters())
        {
            if (c.skipTurn == true)
            {
                c.skipTurn = false;
                AddSkippedCharacter(c);
                continue;
            }

            c.ActivateCharacter();
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
