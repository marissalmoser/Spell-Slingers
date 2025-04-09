using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : Controller
{
    public static PlayerController instance;

    private Character selectedCharacter;

    [SerializeField] private GameObject actionUI;
    [SerializeField] private Button attackBtn;
    [SerializeField] private Button waitBtn;
    [SerializeField] private GameObject attacksMenu;
    [SerializeField] private VerticalLayoutGroup attackGroup;
    [SerializeField] private GameObject abilityBtnPrefab;

    [SerializeField] private GameObject[] attackButtons;
    [SerializeField] private Button cancelAttackBtn;

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

    /// <summary>
    /// Constructs Attack Choice UI.
    /// </summary>
    /// <param name="abilities"></param>
    public void ConstructUI(Ability[] abilities)
    {
        int index = 0;
        attacksMenu.SetActive(true);

        foreach(Ability a in abilities)
        {
            GameObject temp = attackButtons[index];
            temp.SetActive(true);
            temp.GetComponentInChildren<TextMeshProUGUI>().text = a.abilityName;
            temp.GetComponent<AttackButton>().attackIndex = index;
            temp.GetComponent<Button>().onClick.AddListener(temp.GetComponent<AttackButton>().SetAttackOnCharacter);
        }

        cancelAttackBtn.onClick.AddListener(selectedCharacter.CloseAttackSelection);
    }

    /// <summary>
    /// Destroys Attack Choice UI.
    /// </summary>
    public void DestroyUI()
    {
        foreach(GameObject btn in attackButtons)
        {
            btn.GetComponent<Button>().onClick.RemoveAllListeners();
            btn.SetActive(false);
        }

        cancelAttackBtn.onClick.RemoveAllListeners();
    }
}
