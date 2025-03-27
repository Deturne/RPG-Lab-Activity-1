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
        CombatEvents.e_onHealthChange.AddListener(OnHealthUpdate);
        healthSlider.maxValue = character.maxHp;
        healthSlider.minValue = 0;
        healthSlider.value = character.curHp;
        healthtxt.text = character.curHp + " / " + character.maxHp;
    }

    public void OnHealthUpdate()
    {
        healthtxt.text = character.curHp + " / " + character.maxHp;
        healthSlider.value = character.curHp;
    }
}
