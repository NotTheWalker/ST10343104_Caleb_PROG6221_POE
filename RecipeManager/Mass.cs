///<summary>
///Caleb Morse
///ST10343104
///Group 2
///References:
/// https://www.w3schools.com/cs/cs_method_overloading.php
/// https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/functional/pattern-matching#compare-discrete-values
/// https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/functional/pattern-matching#relational-patterns
///</summary>

namespace RecipeManager;

public class Mass : AbstractMeasurable // Mass is a subclass of AbstractMeasurable
{
    // Constants for the conversion factors
    private const double GRAM = 1000;
    private const double OUNCE = 28349.5;
    private const double POUND = 453592;
    private const double KILOGRAM = 1000000;

    public enum UnitMass // Enum for the units of mass
    {
        // Units arranged in ascending order for comparison purposes
        Milligram, Gram, Ounce, Pound, Kilogram
    }

    public Mass(double milligrams, UnitMass unit) // Constructor for the Mass class
    {
        BaseValue = milligrams;
        CurrentUnit = unit;
    }

    public override double ConvertTo(Enum unit) => unit switch
    {
        UnitMass.Milligram => BaseValue,
        UnitMass.Gram => BaseValue / GRAM,
        UnitMass.Ounce => BaseValue / OUNCE,
        UnitMass.Pound => BaseValue / POUND,
        UnitMass.Kilogram => BaseValue / KILOGRAM,
        _ => throw new ArgumentException("Invalid unit")
    };

    public override double ConvertToLargest(double Value)
    {
        // The order of the if statements is based on the size of the units
        // The unit used is stored in CurrentUnit
        switch (Value)
        {
            case >= KILOGRAM:
                CurrentUnit = UnitMass.Kilogram;
                return ConvertTo(UnitMass.Kilogram);
            case >= POUND:
                CurrentUnit = UnitMass.Pound;
                return ConvertTo(UnitMass.Pound);
            case >= OUNCE:
                CurrentUnit = UnitMass.Ounce;
                return ConvertTo(UnitMass.Ounce);
            case >= GRAM:
                CurrentUnit = UnitMass.Gram;
                return ConvertTo(UnitMass.Gram);
            default:
                CurrentUnit = UnitMass.Milligram;
                return BaseValue;
        }
    }

    public override double ConvertToLargest() => ConvertToLargest(BaseValue);

    public override void ConvertFrom(Enum unit) => BaseValue *= unit switch
    {
        UnitMass.Milligram => 1,
        UnitMass.Gram => GRAM,
        UnitMass.Ounce => OUNCE,
        UnitMass.Pound => POUND,
        UnitMass.Kilogram => KILOGRAM,
        _ => throw new ArgumentException("Invalid unit"),
    };
}
