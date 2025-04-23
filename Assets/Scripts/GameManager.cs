using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using System.Threading.Tasks;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    private Controller activeController;

    private Queue<Controller> turnOrder = new Queue<Controller>();

    private Character CharacterScript;

    private int counter = 0;

    [SerializeField] private GameObject startGameBtn; //Button for starting round, should be replaced if better way to initialize combat is found.

    public static Action OnTurnStart;
    public static Action OnTurnEnd;
    public static Action OnPlayerTurnStart;
    public static Action OnEnemyTurnStart;
    public static Action OnPlayerTurnEnd;
    public static Action OnEnemyTurnEnd;

    public GameObject PlayerTurn;
    public GameObject EnemyTurn;

    private TutorialManager tutorialManager;

    private void Start()
    {
        tutorialManager = GetComponent<TutorialManager>();
        tutorialManager.Init();

        turnOrder.Enqueue(PlayerController.instance);
        turnOrder.Enqueue(AIController.instance);

        PlayerTurn.SetActive(true);
        EnemyTurn.SetActive(false);

        Character.OnCantAct += IncrementCounter;
    }

    /// <summary>
    /// Starts player turn. CHANGE LATER TO ACCEPT AI CONTROLLER
    /// </summary>
    private void StartTurn()
    {
        activeController = turnOrder.Dequeue();
        activeController.GetComponent<Controller>().StartTurn();

        OnTurnStart?.Invoke();
    }

    /// <summary>
    /// Ends turn of the active controller. CHANGE LATER TO ACCEPT AI CONTROLLER
    /// </summary>
    private void EndTurn()
    {
        activeController.GetComponent<Controller>().EndTurn();

        if(activeController.TryGetComponent<AIController>(out AIController aiCont))
        {
            PlayerTurn.SetActive(true);
            EnemyTurn.SetActive(false);
            StartCoroutine(TurnSwitch());
            OnEnemyTurnEnd?.Invoke();
        }
        else if(activeController.TryGetComponent<PlayerController>(out PlayerController pCont))
        {
            EnemyTurn.SetActive(true);
            PlayerTurn.SetActive(false);
            StartCoroutine(TurnSwitch());
            OnPlayerTurnEnd?.Invoke();
        }

        OnTurnEnd?.Invoke();

        turnOrder.Enqueue(activeController);
        counter = 0;

        StartTurn();
    }

    private void Update()
    {
    }

    private void IncrementCounter()
    {
        //Debug.Log("COUNTER INCREMENTED");
        
        counter++;

        if (counter == activeController.GetControlledCharacters().Count - activeController.GetSkippedCharacters().Count)
        {
            EndTurn();
        }

        //Debug.Log("The Counter is at " + counter);
}

    /// <summary>
    /// Starts game so that errors don't arise with Start execution order.
    /// </summary>
    public void StartGame()
    {

    }

    public void FixStart()
    {
        StartTurn();

        startGameBtn.SetActive(false); //References testing button.
    }

    IEnumerator TurnSwitch()
    {
        yield return new WaitForSeconds(3);
        PlayerTurn.SetActive(false);
        EnemyTurn.SetActive(false);
    }
}
