using UnityEngine;

public class TestCharacterClass : MonoBehaviour
{
    [SerializeField] private Enum_CharacterClass _characterClass;
    [SerializeField] private Enum_CharacterType _characterType;
    [SerializeField] private AbilityScore _abilityScore;
    [SerializeField] private Character _character;
    private void Awake()
    {
        _abilityScore = new AbilityScore(10, 8, 5, 3, 1, 0);
        _character = new Character("Test", Enum_CharacterType.PLAYER, _characterClass, _abilityScore, gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log(_character);

            //0-2 (-1), 3-4 (0), 5-6 (1), 7-8 (2), 9-10 (3).
            Debug.Log(_character.AbilityScore.getModifier(Enum_AbilityScoreNames.STRENGTH));        //3
            Debug.Log(_character.AbilityScore.getModifier(Enum_AbilityScoreNames.DEXTERITY));       //1
            Debug.Log(_character.AbilityScore.getModifier(Enum_AbilityScoreNames.CONSTITUTION));    //0
            Debug.Log(_character.AbilityScore.getModifier(Enum_AbilityScoreNames.INTELLIGENCE));    //-1
            Debug.Log(_character.AbilityScore.getModifier(Enum_AbilityScoreNames.WISDOM));          //-1
            Debug.Log(_character.AbilityScore.getModifier(Enum_AbilityScoreNames.CHARISMA));        //-1
        }
    }
}
