namespace RecipeManager;

public class Mass : AbstractMeasurable
{
    private const double GRAM = 1000;
    private const double OUNCE = 28349.5;
    private const double POUND = 453592;
    private const double KILOGRAM = 1000000;

    public enum UnitMass
    {
        // Units arranged in ascending order for comparison purposes
        Milligram, Gram, Ounce, Pound, Kilogram
    }

    public Mass(double milligrams, UnitMass unit)
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
