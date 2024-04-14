namespace RecipeManager;

public class Volume : AbstractMeasurable
{
    public enum UnitVolume
    {
        // Units arranged in ascending order for comparison purposes
        Milliliter, Teaspoon, Tablespoon, Cup, Pint, Quart, Liter, Gallon
    }

    public Volume(double milliliters, UnitVolume unit)
    {
        BaseValue = milliliters;
        CurrentUnit = unit;
    }

    public override double ConvertTo(Enum unit)
    {
        return unit switch
        {
            UnitVolume.Milliliter => BaseValue,
            UnitVolume.Teaspoon => BaseValue / 4.92892,
            UnitVolume.Tablespoon => BaseValue / 14.7868,
            UnitVolume.Cup => BaseValue / 236.588,
            UnitVolume.Pint => BaseValue / 473.176,
            UnitVolume.Quart => BaseValue / 946.353,
            UnitVolume.Liter => BaseValue / 1000,
            UnitVolume.Gallon => BaseValue / 3785.41,
            _ => throw new ArgumentException("Invalid unit")
        };
    }

    public override double ConvertToLargest(double Value)
    {
        // The order of the if statements is based on the size of the units
        // The unit used is stored in CurrentUnit
        if(Value > 3785.41)
        {
            CurrentUnit = UnitVolume.Gallon;
            return ConvertTo(UnitVolume.Gallon);
        }
        else if(Value > 1000)
        {
            CurrentUnit = UnitVolume.Liter;
            return ConvertTo(UnitVolume.Liter);
        }
        else if(Value > 946.353)
        {
            CurrentUnit = UnitVolume.Quart;
            return ConvertTo(UnitVolume.Quart);
        }
        else if(Value > 473.176)
        {
            CurrentUnit = UnitVolume.Pint;
            return ConvertTo(UnitVolume.Pint);
        }
        else if(Value > 236.588)
        {
            CurrentUnit = UnitVolume.Cup;
            return ConvertTo(UnitVolume.Cup);
        }
        else if(Value > 14.7868)
        {
            CurrentUnit = UnitVolume.Tablespoon;
            return ConvertTo(UnitVolume.Tablespoon);
        }
        else if(Value > 4.92892)
        {
            CurrentUnit = UnitVolume.Teaspoon;
            return ConvertTo(UnitVolume.Teaspoon);
        }
        else
        {
            CurrentUnit = UnitVolume.Milliliter;
            return Value;
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
            case UnitVolume.Milliliter:
                BaseValue *= 1;
                break;
            case UnitVolume.Teaspoon:
                BaseValue *= 4.92892;
                break;
            case UnitVolume.Tablespoon:
                BaseValue *= 14.7868;
                break;
            case UnitVolume.Cup:
                BaseValue *= 236.588;
                break;
            case UnitVolume.Pint:
                BaseValue *= 473.176;
                break;
            case UnitVolume.Quart:
                BaseValue *= 946.353;
                break;
            case UnitVolume.Liter:
                BaseValue *= 1000;
                break;
            case UnitVolume.Gallon:
                BaseValue *= 3785.41;
                break;
            default:
                throw new ArgumentException("Invalid unit");
        }
    }
}
