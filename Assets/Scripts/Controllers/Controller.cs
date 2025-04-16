using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private List<Character> controlledCharacters = new List<Character>();
    private List<Character> skippedCharacters = new List<Character>();

    /// <summary>
    /// Returns the list of controlled characters managed by this controller.
    /// </summary>
    /// <returns></returns>
    public List<Character> GetControlledCharacters() 
    {
        return controlledCharacters;
    }

    /// <summary>
    /// Adds a character to be controlled by the controller.
    /// </summary>
    /// <param name="character"></param>
    public void AddControlledCharacter(Character character)
    {
        controlledCharacters.Add(character);
    }

    /// <summary>
    /// Removes a character from the characters this controller controls.
    /// </summary>
    /// <param name="character"></param>
    public void RemoveControlledCharacter(Character character)
    {
        controlledCharacters.Remove(character);
    }

    /// <summary>
    /// Gets the list of skipped characters.
    /// </summary>
    /// <returns></returns>
    public List<Character> GetSkippedCharacters()
    {
        return skippedCharacters;
    }

    /// <summary>
    /// Adds a skipped character to the list of skipped characters.
    /// </summary>
    /// <param name="character"></param>
    public void AddSkippedCharacter(Character character)
    {
        skippedCharacters.Add(character);
    }

    /// <summary>
    /// Removes a skipped character from the list of skipped characters.
    /// </summary>
    /// <param name="character"></param>
    public void RemoveSkippedCharacter(Character character)
    {
        skippedCharacters.Remove(character);
    }

    /// <summary>
    /// Clears list of skipped characters.
    /// </summary>
    public void ClearSkippedCharacters()
    {
        skippedCharacters.Clear();
    }

    public virtual void StartTurn()
    {
        Debug.Log("STARTING TURN");
    }

    public virtual void EndTurn()
    {
        Debug.Log("ENDING TURN");
    }
}
