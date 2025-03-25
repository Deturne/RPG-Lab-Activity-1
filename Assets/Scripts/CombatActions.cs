using UnityEngine;

// This will create a new menu item inside the Unity editor called "New Combat Action".
// We can use this to create new instances of this scriptable object.
[CreateAssetMenu(fileName = "Combat Action", menuName = "New Combat Action")]
// Notice that this inherits from ScriptableObject as opposed to Monobehaviour or nothing.
public class CombatActions : ScriptableObject
{
    // The name of this action.
    public string DisplayName;

    // What type of attack this is. -> should be renamed to actiontype
    public AttackType attackType;

    // How much damage this attack deals.
    public int Damage;
    // The projectile used for this attack.
    public GameObject ProjectilePrefab;

    // If this is a healing action, how much it heals for.
    public int HealAmount;
}