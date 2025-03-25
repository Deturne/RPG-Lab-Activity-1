using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    // Reference to the CombatCharacter script on the character this health bar is for.
    [SerializeField] CombatCharacter character;

    // Reference to the health text and slider on the UI.
    [SerializeField] TMP_Text healthText;
    [SerializeField] Slider healthSlider;

    //Adds the OnHealthUpdate method to the onHealthChange event.
    //Sets the max value of the health slider to the character's max health.
    private void Start()
    {
        CombatEvents.e_onHealthChange.AddListener(OnHealthUpdate);
        healthSlider.maxValue = character.maxHp;
        healthSlider.value = character.curHp;
        healthText.text = character.curHp + " / " + character.maxHp;
    }

    //Updates the health text and slider to the current health of the character.
    public void OnHealthUpdate()
    {
        healthText.text = character.curHp + " / " + character.maxHp;
        healthSlider.value = character.curHp;
    }
}