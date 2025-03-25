using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatCharacter : MonoBehaviour
{
    [Header("Fields")]
    [Tooltip("Is this character the player?")]
    public bool isPlayer;
    [Tooltip("The current health of this character.")]
    public int curHp;
    [Tooltip("The maximum health of this character.")]
    public int maxHp;
    [Tooltip("Used to store the position of this character at the start of combat.")]
    private Vector3 startPos;

    [Header("References")]
    [Tooltip("A list of all the scriptableObject combat actions this character can perform.")]
    public List<CombatActions> combatAction;

    [Tooltip("A reference to the opponent of this character during this battle sequence.")]
    [SerializeField] CombatCharacter opponent;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Make the startPos be the position of the character immediately when they load in.
        startPos = transform.position;
    }

    // Handles all things that happen when this character takes damage.
    public void TakeDamage(int damageToTake)
    {
        Debug.Log($"Damage to take: {damageToTake}");

        // Subtract the damage dealt from the current health.
        curHp -= damageToTake;

        // Invoke the onHealthChange event.
        CombatEvents.e_onHealthChange.Invoke();

        // Check to make sure the player is still alive, if not, call their Die() method.
        if (curHp <= 0)
        {
            Die();
        }
    }

    // Handles the death of the character.
    private void Die()
    {
        // Invoke the onCharacterDie event and pass in this character.
        CombatEvents.e_onCharacterDie.Invoke(this);
        // Remove this character from the game and clear up the memory.
        Destroy(gameObject);
    }

    // Handles when this character is healed.
    public void Heal(int healAmount)
    {
        // Increase the current health by the heal amount.
        curHp += healAmount;

        // Invoke the onHealthChange event.
        CombatEvents.e_onHealthChange.Invoke();

        // Check to see if the current health exceeds the max health, and if so, cap it at max health.
        if (curHp > maxHp)
        {
            curHp = maxHp;
        }
    }

    // Handles using an ability (combatAction).
    public void CastCombatAction(CombatActions combatAction)
    {
        // First check to see if the ability being used does any damage.
        if (combatAction.Damage > 0)
        {
            StartCoroutine(AttackOpponent(combatAction));
        }
        // Then check to see if the ability uses a projectile.
        else if (combatAction.ProjectilePrefab != null)
        {
            // If so, spawn the projectile at the location of the character.
            GameObject proj = Instantiate(combatAction.ProjectilePrefab, transform.position, Quaternion.identity);
        }
        // Then check to see if the ability heals the character.
        else if (combatAction.HealAmount > 0)
        {
            // If so, call the Heal method to apply the healing.
            Heal(combatAction.HealAmount);
            // Then end this character's turn.
            TurnManager.instance.EndTurn();
        }
        // If none of the above are true...
        else
        {
            // End the turn for this character.
            TurnManager.instance.EndTurn();
        }
    }

    // Coroutine used for attacking with a combat ability.
    IEnumerator AttackOpponent(CombatActions combatAction)
    {
        // Loop to move the character towards their opponent.
        while (transform.position != opponent.transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, opponent.transform.position, 50 * Time.deltaTime);
            yield return null;
        }

        // Apply the damage to the opponent once the character is at their location.
        opponent.TakeDamage(combatAction.Damage);

        // Run a second loop to move the character back to their starting position.
        while (transform.position != startPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos, 20 * Time.deltaTime);
        }

        // After the attack is finished and the character is back at their starting position, end their turn.
        TurnManager.instance.EndTurn();
    }

    public float GetHealthPercentage()
    {
        return (float) curHp / maxHp;
    }
}