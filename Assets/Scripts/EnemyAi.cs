using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class EnemyAi : MonoBehaviour
{
    [SerializeField] private CombatCharacter character;
    [SerializeField] private AnimationCurve healRate;

    private void Start()
    {
        CombatEvents.e_onBeginTurn.AddListener(OnBeginTurn);
    }

    public void OnBeginTurn(CombatCharacter c)
    {
        if(character == c)
        {
            DetermineCombatActions();
        }
    }

    public void DetermineCombatActions()
    {
        float healthPercentage = character.GetHealthPercentage();
        bool wantToHeal = Random.value < healRate.Evaluate(healthPercentage);

        CombatActions ca = null;

        if (wantToHeal && DetermineIfHasCombatActionType(AttackType.Heal)) {
            ca = GetCombatActionOfType(AttackType.Heal);
        }

        else if (DetermineIfHasCombatActionType(AttackType.Attack))
        {
            ca = GetCombatActionOfType(AttackType.Attack);
        }

        if (ca != null) { 
            character.CastCombatAction(ca);
        }
        else
        {
            TurnManager.instance.EndTurn();
        }

    }

    private bool DetermineIfHasCombatActionType(AttackType type)
    {
        return character.combatAction.Exists(x => x.attackType == type);
    }

    private CombatActions GetCombatActionOfType(AttackType type)
    {
        List<CombatActions> availableActions = character.combatAction.FindAll(x => x.attackType == type);
        return availableActions[Random.Range(0, availableActions.Count)];
    }
}
