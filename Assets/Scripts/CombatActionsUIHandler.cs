using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatActionsUIHandler : MonoBehaviour
{
    [Header("References")]
    [Tooltip("The parent GameObject that will have all combat action related UI elements as its children.")]
    [SerializeField] GameObject visualContainer;
    [Tooltip("An array of buttons used to cast combat actions.")]
    [SerializeField] Button[] combatActionButtons;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Subscribe (start listening for and respond) to both the begin and end turn events, and call the relevant methods in this class in response.
        CombatEvents.e_onBeginTurn.AddListener(OnBeginTurn);
        CombatEvents.e_onEndTurn.AddListener(OnEndTurn);
    }

    /// <summary>
    ///     <br/> When a character's turn begins, show the combat action UI and assign the character's abilities to the buttons. <br/>
    ///     <br/>
    ///         If the character is not the player, just return and skip the UI display.
    ///         If the character is the player, make the UI visible and assign the abilities to the buttons. 
    ///     <br/>
    /// </summary>
    /// <param name="character"> The current Character taking their turn</param>
    public void OnBeginTurn(CombatCharacter character)
    {
        Debug.Log("Begin Turn");

        // First, check to see if the character is the player.
        if (!character.isPlayer)
        {
            // If not, just return, we obviously don't want to show the UI to choose combat actions for NPCs.
            return;
        }

        // Make the entire combat action UI container visible.
        visualContainer.SetActive(true);

        // Run through each combat action available to the player and assign it to a button.
        for (int i = 0; i < combatActionButtons.Length; i++)
        {
            // For each combat action button, check to see if the player has an ability to tie to it or not.
            if (i < character.combatAction.Count)
            {
                // If so, make that button visible.
                combatActionButtons[i].gameObject.SetActive(true);
                // Store a reference to the combataction that we'll tie to this button.
                CombatActions ca = character.combatAction[i];

                // Access the text of the button, and update the text to match the name of the ability.
                combatActionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = ca.DisplayName;
                // Remove all listeners from this button, just in case it was previously tied to a different ability.
                combatActionButtons[i].onClick.RemoveAllListeners();
                // Add a new listener to the button's onClick event that will trigger the OnClickCombatAction method and pass in this specific ability.
                combatActionButtons[i].onClick.AddListener(() => OnClickCombatAction(ca));
            }
            // Otherwise...
            else
            {
                // The character does not have an abilty to tie to this button, so hide the button.
                combatActionButtons[i].gameObject.SetActive(false);
            }
        }
    }

    // Hides the entire combat action UI when the player's turn ends.
    public void OnEndTurn(CombatCharacter character)
    {
        visualContainer.SetActive(false);
    }

    // Access the currentCharacter in the TurnManager instance and use the passed combat action for them.
    public void OnClickCombatAction(CombatActions combatAction)
    {
        TurnManager.instance.currentCharacter.CastCombatAction(combatAction);
    }
}