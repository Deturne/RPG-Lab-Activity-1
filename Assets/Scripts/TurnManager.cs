using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [Header("Fields")]
    [Tooltip("The delay in seconds between one character ending the turn, and the next character's turn starting.")]
    [SerializeField] float nextTurnDelay = 1.0f;
    private int curCharacterIndex = -1;

    [Header("References")]
    [Tooltip("Array to store all our characters taking part in combat.")]
    [SerializeField] CombatCharacter[] characters;
    [Tooltip("The current characer whose turn it is right now.")]
    public CombatCharacter currentCharacter;

    // Static, globally accessible reference for the TurnManager.
    public static TurnManager instance;

    private void Awake()
    {
        // Making the TurnManager a singleton. First, delete any TurnManager that isn't our instance.
        // Then for the only remaining one, make it the instance.
        // This ensures that there is only ever one TurnManager, and it is the instance.
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Move to the next character in the turn order.
    public void OnBeginTurn()
    {
        // Increase the index by one, effectively moving to the next character in the turn list.
        curCharacterIndex++;

        // Check to make sure the index doesn't exceed the array length, if so, reset back to the first character in the turn order.
        if (curCharacterIndex == characters.Length)
        {
            curCharacterIndex = 0;
        }

        // Update the currentCharacter.
        currentCharacter = characters[curCharacterIndex];
        // Invoke the begin turn event passing in the current character.
        CombatEvents.e_onBeginTurn.Invoke(currentCharacter);
    }

    // End the turn, and move onto the next character in the turn order.
    public void EndTurn()
    {
        // Invoke the end turn event, passing in the current character.
        CombatEvents.e_onEndTurn.Invoke(currentCharacter);

        // This will call the OnBeginTurn method after waiting nextTurnDelay seconds.
        Invoke(nameof(OnBeginTurn), nextTurnDelay);
    }

    private void OnCharacterDie(CombatCharacter character)
    {
        Debug.Log("Character Died");
    }
}
