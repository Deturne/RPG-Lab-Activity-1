using System;

//Structs are used to create custom data types that can hold multiple variables of different types.
//They are useful for grouping data together to create a single object, and are not reference types.
//This means that they are passed by value, and not by reference, and therefore not serializable.
public struct AbilityScore
{
    // The fields of this struct.
    private int _strength, _dexterity, _constitution, _intelligence, _wisdom, _charisma;

    // Public getter and setter methods for every ability score.
    // They also cap the value to 10.
    public int Strength
    {
        get => _strength;
        set => _strength = Math.Clamp(value, 0, 10);
    }
    public int Dexterity
    {
        get => _dexterity;
        set => _dexterity = Math.Clamp(value, 0, 10);
    }
    public int Constitution
    {
        get => _constitution;
        set => _constitution = Math.Clamp(value, 0, 10);
    }
    public int Intelligence
    {
        get => _intelligence;
        set => _intelligence = Math.Clamp(value, 0, 10);
    }
    public int Wisdom
    {
        get => _wisdom;
        set => _wisdom = Math.Clamp(value, 0, 10);
    }
    public int Charisma
    {
        get => _charisma;
        set => _charisma = Math.Clamp(value, 0, 10);
    }

    //Makes sure that the ability scores are within the range of 0-10
    // Public constructor for a new AbilityScore struct, note it also forces the max to be 10.
    public AbilityScore(int strength, int dexterity, int constitution, int intelligence, int wisdom, int charisma)
    {
        _strength   = Math.Clamp(strength, 0, 10);
        _dexterity  = Math.Clamp(dexterity, 0, 10);
        _constitution = Math.Clamp(constitution, 0, 10);
        _intelligence = Math.Clamp(intelligence, 0, 10);
        _wisdom     = Math.Clamp(wisdom, 0, 10);
        _charisma   = Math.Clamp(charisma, 0, 10);
    }

    /// <summary>
    /// Returns the modifier for the ability score
    /// </summary>
    /// <param name="name">Give a enum type of AbilityScoreNames </param>
    /// <returns>A int modifer based on the current stats</returns>
    public int getModifier(Enum_AbilityScoreNames name)
    {
        //Each ability has a max score of 10, and the range for the bonus is the following:
        //0-2 (-1), 3-4 (0), 5-6 (1), 7-8 (2), 9-10 (3).

        int modifer = 0;
        switch (name)
        {
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

        //Checks the range of the modifer and returns the correct value.
        //Smaller ranges are placed before larger ranges to return the modifer earlier,
        //which ensures the correct value is returned.
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

    public override string ToString()
    {
        return $"Strength: {Strength}, Dexterity: {Dexterity}, Constitution: {Constitution}, " +
            $"Intelligence: {Intelligence}, Wisdom: {Wisdom}, Charisma: {Charisma}";
    }
}