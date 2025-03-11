using UnityEngine;

[CreateAssetMenu(fileName = "Combat Action", menuName = "New Combat Action")]
public class CombatActions : ScriptableObject
{
    public string DisplayName;
    public AttackType attackType;

    public int Damage;
    public GameObject ProjectilePrefab;

    public int HealAmount;
}
