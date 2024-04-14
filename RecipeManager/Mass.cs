namespace RecipeManager;

public class Mass
{
    public enum UnitMass
    {
        // Units arranged in ascending order for comparison purposes
        Milligram, Gram, Ounce, Pound, Kilogram
    }

    private double Milligrams { get; set; } // Always stored in milligrams
    private UnitMass Unit { get; set; } // The current unit of measurement

    public Mass(double milligrams, UnitMass unit)
    {
        Milligrams = milligrams;
        Unit = unit;
    }

    public double ToMilligrams()
    {
        return Milligrams;
    }

    public double ConvertTo(UnitMass unit)
    {
        return unit switch
        {
            UnitMass.Milligram => Milligrams,
            UnitMass.Gram => Milligrams / 1000,
            UnitMass.Ounce => Milligrams / 28349.5,
            UnitMass.Pound => Milligrams / 453592,
            UnitMass.Kilogram => Milligrams / 1000000,
            _ => throw new ArgumentException("Invalid unit")
        };
    }

    public double ConvertToLargest()
    {
        if(Milligrams > 453592)
        {
            Unit = UnitMass.Pound;
            return ConvertTo(UnitMass.Pound);
        }
        else if(Milligrams > 1000000)
        {
            Unit = UnitMass.Kilogram;
            return ConvertTo(UnitMass.Kilogram);
        }
        else if(Milligrams > 28349.5)
        {
            Unit = UnitMass.Ounce;
            return ConvertTo(UnitMass.Ounce);
        }
        else if(Milligrams > 1000)
        {
            Unit = UnitMass.Gram;
            return ConvertTo(UnitMass.Gram);
        }
        else
        {
            Unit = UnitMass.Milligram;
            return Milligrams;
        }
    }
}
