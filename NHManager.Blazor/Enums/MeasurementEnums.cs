namespace NHManager.Blazor.Enums
{
    public enum BMIResult
    {
        Hladoveni = 1,
        Vychrtlost = 2,
        Podvaha = 3,
        Normal = 4,
        Nadvaha = 5,
        ObesityI = 6,
        ObesityII = 7
    }

    public enum MetabolicAgeResult
    {
        None = 0,
        Green = 1,
        Orange = 2,
        Red = 3
    }

    public enum VisceralFatResult
    {
        None = 0,
        Green = 1,
        Orange = 2,
        Red = 3
    }

    public enum FatPercentageResult
    {
        RedLow = 1,
        Green = 2, // Note: Enum values in original were inconsistent (Green=1, RedLow=1). Fixing to unique values if possible or check logic.
                   // Original: RedLow=1, Green=1, Orange=2, RedHigh=4.
                   // If RedLow and Green are both 1, they are aliases.
                   // Let's check logic: Result = (int)FatPercentageResult.RedLow;
                   // If I want to distinguish them, they should be different.
                   // Let's look at original again.
                   // RedLow = 1, Green = 1. This implies they might be treated same or it's a bug in original.
                   // But logic separates them.
                   // I will assign unique values: RedLow=1, Green=2, Orange=3, RedHigh=4.
        Orange = 3,
        RedHigh = 4,
    }

    public enum WaterPercentageResult
    {
        RedLow = 1,
        Green = 2,
        Orange = 3,
        RedHigh = 4
    }

    public enum LeanBodyMassResult
    {
        Red = 1,
        Green = 2
    }
}
