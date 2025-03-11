using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField] CombatCharacter[] characters;
    [SerializeField] float nextTurnDelay = 1.0f;

    private int curCharacterIndex = -1;
    public CombatCharacter currentCharacter;

    public static TurnManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void OnBeginTurn()
    {
        curCharacterIndex++;

        if (curCharacterIndex == characters.Length)
        {
            curCharacterIndex = 0;
        }

        currentCharacter = characters[curCharacterIndex];
        CombatEvents.e_onBeginTurn.Invoke(currentCharacter);
    }

    public void EndTurn()
    {
        CombatEvents.e_onEndTurn.Invoke(currentCharacter);

        Invoke(nameof(OnBeginTurn), nextTurnDelay);
    }

    private void OnCharacterDie(CombatCharacter character)
    {
        Debug.Log("Character Died");
    }
}
