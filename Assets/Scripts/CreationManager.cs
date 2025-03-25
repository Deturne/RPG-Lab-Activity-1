using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreationManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] stats;
    [SerializeField] Sprite[] classSprites;
    [SerializeField] TMP_Dropdown classSelect;
    [SerializeField] Image spriteBox;
    [SerializeField] TMP_InputField playerName;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RollStats()
    {
        foreach (var stats in stats)
        {
            int score = Random.Range(1, 11);
            stats.text = score.ToString();
        }
    }

    public void UpdateSprite()
    {
        int index = classSelect.value;
        spriteBox.sprite = classSprites[index];
    }

    public void StartTheGame()
    {
        Enum_CharacterClass chosenClass;
        if (classSelect.value == 0)
        {
            chosenClass = Enum_CharacterClass.WIZARD;
        }
        else if (classSelect.value == 1)
        {
            chosenClass = Enum_CharacterClass.CLERIC;
        }
        else
        {
            chosenClass = Enum_CharacterClass.FIGHTER;
        }
        AbilityScore scores = new AbilityScore();
        int str = int.Parse(stats[0].text);
        scores.Strength = int.Parse(stats[0].text);
        Character newChar = new Character(playerName.text, Enum_CharacterType.PLAYER, chosenClass, scores, null);
    }
}