using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public bool canAct = false;

    //Ability[] attacks;

    private controller controllerType;

    public enum controller { none, player, ai}

    public controller ControllerType { get => controllerType; }

    public Character()
    {

    }

    public Character(controller controllerType)
    {
        this.controllerType = controllerType;
    }


    public Controller GetController()
    {
        throw new System.NotImplementedException();
    }

    public void ActivateCharacter()
    {

    }

    public void DeactivateCharacter()
    {

    }

    //SelectCharacter, might be an Action instead of a method.


}
