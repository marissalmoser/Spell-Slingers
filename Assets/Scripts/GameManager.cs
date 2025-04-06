using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using System.Threading.Tasks;

public class GameManager : MonoBehaviour
{
    private Controller activeController;

    private Queue<Controller> turnOrder = new Queue<Controller>();

    [SerializeField] private GameObject startGameBtn; //Button for starting round, should be replaced if better way to initialize combat is found.

    private void Start()
    {
        turnOrder.Enqueue(PlayerController.instance);

        Character.OnCantAct += IncrementCounter;
    }

    /// <summary>
    /// Starts player turn. CHANGE LATER TO ACCEPT AI CONTROLLER
    /// </summary>
    private void StartTurn()
    {
        activeController = turnOrder.Dequeue();
        activeController.GetComponent<PlayerController>().StartTurn();

    }

    /// <summary>
    /// Ends turn of the active controller. CHANGE LATER TO ACCEPT AI CONTROLLER
    /// </summary>
    private void EndTurn()
    {
        activeController.GetComponent<PlayerController>().EndTurn();
        turnOrder.Enqueue(activeController);
        
        //Check end conditions.

        //StartTurn() if end conditions not met.
    }

    private void IncrementCounter()
    {
        Debug.Log("COUNTER INCREMENTED");
    }

    /// <summary>
    /// Starts game so that errors don't arise with Start execution order.
    /// </summary>
    public void StartGame()
    {
        StartTurn();

        startGameBtn.SetActive(false); //References testing button.
    }
}
