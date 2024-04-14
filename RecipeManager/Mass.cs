namespace RecipeManager;

public class Mass : AbstractMeasurable
{
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

    public override double ConvertTo(Enum unit)
    {
        return unit switch
        {
            UnitMass.Milligram => BaseValue,
            UnitMass.Gram => BaseValue / 1000,
            UnitMass.Ounce => BaseValue / 28349.5,
            UnitMass.Pound => BaseValue / 453592,
            UnitMass.Kilogram => BaseValue / 1000000,
            _ => throw new ArgumentException("Invalid unit")
        };
    }

    public override double ConvertToLargest(double Value)
    {
        // The order of the if statements is based on the size of the units
        // The unit used is stored in CurrentUnit
        if(Value > 453592)
        {
            CurrentUnit = UnitMass.Pound;
            return ConvertTo(UnitMass.Pound);
        }
        else if(Value > 1000000)
        {
            CurrentUnit = UnitMass.Kilogram;
            return ConvertTo(UnitMass.Kilogram);
        }
        else if(Value > 28349.5)
        {
            CurrentUnit = UnitMass.Ounce;
            return ConvertTo(UnitMass.Ounce);
        }
        else if(Value > 1000)
        {
            CurrentUnit = UnitMass.Gram;
            return ConvertTo(UnitMass.Gram);
        }
        else
        {
            CurrentUnit = UnitMass.Milligram;
            return BaseValue;
        }
    }

    public override double ConvertToLargest()
    {
        return ConvertToLargest(BaseValue);
    }

    public override void ConvertFrom(Enum unit)
    {
        switch (unit)
        {
            case UnitMass.Milligram:
                BaseValue *= 1;
                break;
            case UnitMass.Gram:
                BaseValue *= 1000;
                break;
            case UnitMass.Ounce:
                BaseValue *= 28349.5;
                break;
            case UnitMass.Pound:
                BaseValue *= 453592;
                break;
            case UnitMass.Kilogram:
                BaseValue *= 1000000;
                break;
            default:
                throw new ArgumentException("Invalid unit");
        }
    }
}
