using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreationManager : MonoBehaviour
{
    [Header("UI Stuff")]
    [Tooltip("An array for all the stats that are shown.")]
    [SerializeField] TextMeshProUGUI[] stats;
    [Tooltip("An array of all the different character sprites for each class.")]
    [SerializeField] Sprite[] classSprites;
    [Tooltip("Reference to the dropdown menu to select character class.")]
    [SerializeField] TMP_Dropdown classSelect;
    [Tooltip("The image box where the class sprite will be shown.")]
    [SerializeField] Image spriteBox;
    [Tooltip("Input field where the player can input their name.")]
    [SerializeField] TMP_InputField playerName;

    // Run through all the stats, randomize the value between 1 and 10, and set the text to match.
    public void RollStats()
    {
        foreach (var stats in stats)
        {
            int score = UnityEngine.Random.Range(1, 11);
            stats.text = score.ToString();
        }
    }

    // Update the displayed sprite to match the chosen class.
    public void UpdateSprite()
    {
        int index = classSelect.value;
        spriteBox.sprite = classSprites[index];
    }

    // Called once we're ready to start the game and end character creation.
    public void StartTheGame()
    {
        // Variable to hold the chosen class.
        Enum_CharacterClass chosenClass;
        // Conditional statements to determine what class they picked and set it.
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
        // Instantiate an ability score object.
        AbilityScore scores = new AbilityScore();
        // Receive all the rolled scores, convert them to ints, and assign them to the correct ability score.
        scores.Strength = int.Parse(stats[0].text);
        scores.Dexterity = int.Parse(stats[1].text);
        scores.Constitution = int.Parse(stats[2].text);
        scores.Wisdom = int.Parse(stats[3].text);
        scores.Intelligence = int.Parse(stats[4].text);
        scores.Charisma = int.Parse(stats[5].text);

        // Instantiate a new character and pass in all the data generated and chosen by the player.
        Character newChar = new Character(playerName.text, Enum_CharacterType.PLAYER, chosenClass, scores, null);

        // Give this newly created player to the game manager character list.
        GameManager.instance.characters[0] = newChar;

        // Load the overworld scene.
        SceneManager.LoadScene(2);
    }
}