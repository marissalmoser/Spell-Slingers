using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller
{
    public static PlayerController instance;

    private Character selectedCharacter;

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

    #endregion

    /// <summary>
    /// Activate characters and start turns.
    /// </summary>
    public void StartTurn()
    {
        foreach(Character c in GetControlledCharacters())
        {
            c.ActivateCharacter();
        }
    }

    public void EndTurn()
    {
        foreach (Character c in GetControlledCharacters())
        {
            c.DeactivateCharacter();
        }
    }
}
