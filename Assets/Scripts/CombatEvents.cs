using UnityEngine;
using UnityEngine.Events;

// All of our combat events in a static class so we don't need to instantiate it, and it is globally accessible by anything in our entire codebase.
public static class CombatEvents
{
    // Remember to initialize the events, or things won't work correctly.
    public static UnityEvent<CombatCharacter> e_onBeginTurn = new UnityEvent<CombatCharacter>();
    public static UnityEvent<CombatCharacter> e_onEndTurn = new UnityEvent<CombatCharacter>();
    public static UnityEvent<CombatCharacter> e_onCharacterDie = new UnityEvent<CombatCharacter>();
    public static UnityEvent e_onHealthChange = new UnityEvent();
}