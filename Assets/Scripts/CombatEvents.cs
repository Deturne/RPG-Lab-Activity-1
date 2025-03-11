using UnityEngine;
using UnityEngine.Events;

public static class CombatEvents
{
    public static UnityEvent<CombatCharacter> e_onBeginTurn = new UnityEvent<CombatCharacter>();
    public static UnityEvent<CombatCharacter> e_onEndTurn = new UnityEvent<CombatCharacter>();
    public static UnityEvent<CombatCharacter> e_onCharacterDie = new UnityEvent<CombatCharacter>();
    public static UnityEvent e_onHealthChange = new UnityEvent();

}
