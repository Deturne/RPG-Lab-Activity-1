using System;
public struct AbilityScore
{
    private int strength, dexterity, constitution, intelligence, wisdom, charisma;

    public int Strength { 
        get => strength;
        set => strength =  Math.Clamp(value,0,10); 
    }
    public int Dexterity
    {
        get => dexterity;
        set => dexterity = Math.Clamp(value, 0, 10);
    }
    public int Constitution
    {
        get => constitution;
        set => constitution = Math.Clamp(value, 0, 10);
    }
    public int Intelligence
    {
        get => intelligence;
        set => intelligence = Math.Clamp(value, 0, 10);
    }
    public int Wisdom
    {
        get => wisdom;
        set => wisdom = Math.Clamp(value, 0, 10);
    }
    public int Charisma
    {
        get => charisma;
        set => charisma = Math.Clamp(value, 0, 10);
    }

    public AbilityScore(int strength, int dexterity, int constitution, int intelligence, int wisdom, int charisma)
    {
        Strength = strength;
        Dexterity = dexterity;
        Constitution = constitution;
        Intelligence = intelligence;
        Wisdom = wisdom;
        Charisma = charisma;
    }

    /// <summary>
    /// Returns the modifier for the ability score
    /// </summary>
    /// <param name="name">Give a enum type of AbilityScoreNames </param>
    /// <returns>A int modifer based on the current stats</returns>
    public int getModifier(Enum_AbilityScoreNames name)
    {
        //Each ability has a max score of 10, and the range for the bonus is the following: 0-2 (-1), 3-4 (0), 5-6 (1), 7-8 (2), 9-10 (3).
        int modifer = 0;
        switch(name):
            case Enum_AbilityScoreNames.STRENGTH:
            modifer = Strength;
            break;
        case Enum_AbilityScoreNames.DEXTERITY:
            modifer = Dexterity;
            break;
        case Enum_AbilityScoreNames.CONSTITUTION:
            modifer = Constitution;
            break;
        case Enum_AbilityScoreNames.INTELLIGENCE:
            modifer = Intelligence;
            break;
        case Enum_AbilityScoreNames.WISDOM:
            modifer = Wisdom;
            break;
        case Enum_AbilityScoreNames.CHARISMA:
            modifer = Charisma;
            break;
        }

        if (modifer <= 2)
        {
            return -1;
        }
        else if (modifer <= 4)
        {
            return 0;
        }
        else if (modifer <= 6)
        {
            return 1;
        }
        else if (modifer <= 8)
        {
            return 2;
        }
        else
        {
            return 3;
        }
    }
