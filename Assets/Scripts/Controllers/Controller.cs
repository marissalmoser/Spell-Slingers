using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private Character[] controlledCharacters;

    public Character[] GetControlledCharacters() 
    {
        return controlledCharacters;
    }
}
