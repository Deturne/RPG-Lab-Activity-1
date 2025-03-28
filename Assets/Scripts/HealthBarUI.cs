using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    //references the character
    [SerializeField] private CombatCharacter character;

    //reference UI
    [SerializeField] private TextMeshProUGUI healthtxt;
    [SerializeField] private Slider healthSlider;

    private void Start()
    {
        // Start listening for any health change event and respond with the OnHealthUpdate() method.
        CombatEvents.e_onHealthChange.AddListener(OnHealthUpdate);
        // Set the health bar maximum to match the character's max health.
        healthSlider.maxValue = character.maxHp;
        // Make sure the minimum value for the health bar is 0.
        healthSlider.minValue = 0;
        // Set the health bar to match the character's current HP value.
        healthSlider.value = character.curHp;
        // Change the text to display the current health / max health.
        healthtxt.text = character.curHp + " / " + character.maxHp;
    }

    // Method to update the text to match the current health and then update the slider fill amount.
    public void OnHealthUpdate()
    {
        healthtxt.text = character.curHp + " / " + character.maxHp;
        healthSlider.value = character.curHp;
    }
}
