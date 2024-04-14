namespace RecipeManager;

public class Volume 
{
    public enum UnitVolume
    {
        // Units arranged in ascending order for comparison purposes
        Milliliter, Teaspoon, Tablespoon, Cup, Pint, Quart, Liter, Gallon
    }

    private double Milliliters { get; set; } // Always stored in milliliters
    private UnitVolume Unit { get; set; } // The current unit of measurement

    public Volume(double milliliters, UnitVolume unit)
    {
        Milliliters = milliliters;
        Unit = unit;
    }

    public double ToMilliliters()
    {
        return Milliliters;
    }

    public double ConvertTo(UnitVolume unit)
    {
        return unit switch
        {
            UnitVolume.Milliliter => Milliliters,
            UnitVolume.Teaspoon => Milliliters / 4.92892,
            UnitVolume.Tablespoon => Milliliters / 14.7868,
            UnitVolume.Cup => Milliliters / 236.588,
            UnitVolume.Pint => Milliliters / 473.176,
            UnitVolume.Quart => Milliliters / 946.353,
            UnitVolume.Liter => Milliliters / 1000,
            UnitVolume.Gallon => Milliliters / 3785.41,
            _ => throw new ArgumentException("Invalid unit")
        };
    }

    public double ConvertToLargest()
    {
        if(Milliliters > 3785.41)
        {
            Unit = UnitVolume.Gallon;
            return ConvertTo(UnitVolume.Gallon);
        }
        else if(Milliliters > 1000)
        {
            Unit = UnitVolume.Liter;
            return ConvertTo(UnitVolume.Liter);
        }
        else if(Milliliters > 946.353)
        {
            Unit = UnitVolume.Quart;
            return ConvertTo(UnitVolume.Quart);
        }
        else if(Milliliters > 473.176)
        {
            Unit = UnitVolume.Pint;
            return ConvertTo(UnitVolume.Pint);
        }
        else if(Milliliters > 236.588)
        {
            Unit = UnitVolume.Cup;
            return ConvertTo(UnitVolume.Cup);
        }
        else if(Milliliters > 14.7868)
        {
            Unit = UnitVolume.Tablespoon;
            return ConvertTo(UnitVolume.Tablespoon);
        }
        else if(Milliliters > 4.92892)
        {
            Unit = UnitVolume.Teaspoon;
            return ConvertTo(UnitVolume.Teaspoon);
        }
        else
        {
            Unit = UnitVolume.Milliliter;
            return Milliliters;
        }
    }
}
