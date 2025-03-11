using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatActionsUIHandler : MonoBehaviour
{
    [SerializeField] GameObject visualContainer;
    [SerializeField] Button[] combatActionButtons;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CombatEvents.e_onBeginTurn.AddListener(OnBeginTurn);
        CombatEvents.e_onEndTurn.AddListener(OnEndTurn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBeginTurn(CombatCharacter character)
    {
        if (!character.isPlayer)
        {
            return;
        }

        visualContainer.SetActive(true);

        for (int i = 0; i < combatActionButtons.Length; i++)
        {
            if (i < character.combatAction.Count)
            {
                combatActionButtons[i].gameObject.SetActive(true);
                CombatActions ca = character.combatAction[i];

                combatActionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = ca.DisplayName;
                combatActionButtons[i].onClick.RemoveAllListeners();
                combatActionButtons[i].onClick.AddListener(() => OnClickCombatAction(ca));
            }
            else
            {
                combatActionButtons[i].gameObject.SetActive(false);
            }
        }
    }

    public void OnEndTurn(CombatCharacter character)
    {
        visualContainer.SetActive(false);
    }

    public void OnClickCombatAction(CombatActions combatAction)
    {
        TurnManager.instance.currentCharacter.CastCombatAction(combatAction);
    }
}
