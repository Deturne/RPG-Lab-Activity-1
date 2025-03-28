using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class EnemyAi : MonoBehaviour
{
    [Tooltip("Reference to the CombatCharacter script on this enemy.")]
    [SerializeField] private CombatCharacter character;
    [Tooltip("Reference to the animation curve used for heal rate.")]
    [SerializeField] private AnimationCurve healRate;

    private void Start()
    {
        // Start listening for the start of a new turn and respond by calling OnBeginTurn().
        CombatEvents.e_onBeginTurn.AddListener(OnBeginTurn);
    }

    // We'll use this to check to see if the current turn is THIS instance of the script, then run through the available combat actions.
    public void OnBeginTurn(CombatCharacter c)
    {
        if(character == c)
        {
            DetermineCombatActions();
        }
    }

    // Look for all combat actions available.
    public void DetermineCombatActions()
    {
        // Get the current HP of the character as a percentage of their max hp.
        float healthPercentage = character.GetHealthPercentage();
        // Randomly decide if this character wants to heal or not, using the healthRate curve.
        bool wantToHeal = Random.value < healRate.Evaluate(healthPercentage);

        // Make sure no CombatAction is chosen.
        CombatActions ca = null;

        // If this character wants to heal, first check if they even have a heal spell, if so, set it as the active CombatAction.
        if (wantToHeal && DetermineIfHasCombatActionType(AttackType.Heal)) {
            ca = GetCombatActionOfType(AttackType.Heal);
        }

        // Otherwise, check if this character has an attack type CombatAction and set it as the active ability.
        else if (DetermineIfHasCombatActionType(AttackType.Attack))
        {
            ca = GetCombatActionOfType(AttackType.Attack);
        }

        // Make sure they actually have a CombatAbility set as the active ability and cast it.
        if (ca != null) { 
            character.CastCombatAction(ca);
        }
        // Otherwise, end this character's turn.
        else
        {
            TurnManager.instance.EndTurn();
        }

    }

    // Checks if this character has a specified type of CombatAction.
    private bool DetermineIfHasCombatActionType(AttackType type)
    {
        return character.combatAction.Exists(x => x.attackType == type);
    }

    // Find all CombatActions of a specific type and randomly choose one.
    private CombatActions GetCombatActionOfType(AttackType type)
    {
        List<CombatActions> availableActions = character.combatAction.FindAll(x => x.attackType == type);
        return availableActions[Random.Range(0, availableActions.Count)];
    }
}
