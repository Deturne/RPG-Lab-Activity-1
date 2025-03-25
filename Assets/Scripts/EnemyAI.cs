using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] CombatCharacter character;
    public AnimationCurve healRate;


    //add a listener to the onBeginTurn event
    private void Start()
    {
        CombatEvents.e_onBeginTurn.AddListener(OnBeginTurn);
    }


    //Determine the combat action to take
    public void OnBeginTurn(CombatCharacter c)
    {
        if (character == c)
        {
            DetermineCombatAction();
        }
    }

    /// <summary>
    /// Determine the combat action to take.
    /// Heal if health is low, the chance of healing increases propertionally to the health lossed,
    /// otherwise attack.
    /// </summary>
    public void DetermineCombatAction()
    {
        float healthPercentage = character.GetHealthPercentage();
        bool wantToHeal = Random.value < healRate.Evaluate(healthPercentage);

        CombatActions ca = null;

        if (wantToHeal && DetermineIfHasCombatActionType(AttackType.Heal))
        {
            ca = GetCombatActionOfType(AttackType.Heal);
        }
        else if (DetermineIfHasCombatActionType(AttackType.Attack))
        {
            ca = GetCombatActionOfType(AttackType.Attack);
        }

        if (ca != null)
        {
            character.CastCombatAction(ca);
        }
        else
        {
            TurnManager.instance.EndTurn();
        }
    }

    /// <summary>
    /// Returns true if the character has a combat action of the specified type within it list of valid combat actions.
    /// </summary>
    /// <param name="type"> The attacktype to find </param>
    /// <returns></returns>
    private bool DetermineIfHasCombatActionType(AttackType type)
    {
        return character.combatAction.Exists(x => x.attackType == type);
    }


    /// <summary>
    /// Returns a random combat action of the specified type from the character's list of valid combat actions.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private CombatActions GetCombatActionOfType(AttackType type)
    {
        List<CombatActions> availableActions = character.combatAction.FindAll(x => x.attackType == type);

        return availableActions[Random.Range(0, availableActions.Count)];
    }
}