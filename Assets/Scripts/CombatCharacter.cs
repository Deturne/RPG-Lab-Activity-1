using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatCharacter : MonoBehaviour
{
    public bool isPlayer;
    public List<CombatActions> combatAction;

    public int curHp;
    public int maxHp;

    [SerializeField] CombatCharacter opponent;
    private Vector3 startPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damageToTake)
    {
        Debug.Log($"Damage to take: {damageToTake}");
        curHp -= damageToTake;

        CombatEvents.e_onHealthChange.Invoke();

        if (curHp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        CombatEvents.e_onCharacterDie.Invoke(this);
        Destroy(gameObject);
    }

    public void Heal(int healAmount)
    {
        curHp += healAmount;

        CombatEvents.e_onHealthChange.Invoke();

        if (curHp > maxHp)
        {
            curHp = maxHp;
        }
    }

    public void CastCombatAction(CombatActions combatAction)
    {
        if (combatAction.Damage > 0)
        {
            // Add damage later.
        }
        else if (combatAction.ProjectilePrefab != null)
        {
            GameObject proj = Instantiate(combatAction.ProjectilePrefab, transform.position, Quaternion.identity);
        }
        else if (combatAction.HealAmount > 0)
        {
            Heal(combatAction.HealAmount);
            TurnManager.instance.EndTurn();
        }
        else
        {
            TurnManager.instance.EndTurn();
        }
    }

    IEnumerator AttackOpponent(CombatActions combatAction)
    {
        while (transform.position != opponent.transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, opponent.transform.position, 50 * Time.deltaTime);
            yield return null;
        }

        opponent.TakeDamage(combatAction.Damage);

        while (transform.position != startPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos, 20 * Time.deltaTime);
        }

        TurnManager.instance.EndTurn();
    }
}
