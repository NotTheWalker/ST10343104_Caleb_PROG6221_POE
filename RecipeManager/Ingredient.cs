namespace RecipeManager;

public class Ingredient
{
    private string Name { get; set; }

    // Either Volume or Mass will be null, as an ingredient can only have one or the other
    private Volume? Volume { get; set; }
    private Mass? Mass { get; set; }

    // This property is used to determine which of the two fields is not null
    // Unsure if this is permanent
    private sealed bool IsLiquid { get;}
    
    // Constructor for liquid ingredients
    public Ingredient(string name, Volume volume)
    {
        Name = name;
        Volume = volume;
        IsLiquid = true;
    }
    
    // Constructor for solid ingredients
    public Ingredient(string name, Mass mass)
    {
        Name = name;
        Mass = mass;
        IsLiquid = false;
    }

    // Constructor for ingredients with a specified unit, which will determine whether the ingredient is liquid or solid
    // This is the constructor that will be used most often
    public Ingredient(string name, double amount, string unit)
    {
        Name = name;
        string[] volumeUnits = Enum.GetNames(typeof(Volume.UnitVolume));
        string[] massUnits = Enum.GetNames(typeof(Mass.UnitMass));
        if(volumeUnits.Contains(unit))
        {
            Volume = new Volume(amount, (Volume.UnitVolume)Enum.Parse(typeof(Volume.UnitVolume), unit));
            IsLiquid = true;
        }
        else if(massUnits.Contains(unit))
        {
            Mass = new Mass(amount, (Mass.UnitMass)Enum.Parse(typeof(Mass.UnitMass), unit));
            IsLiquid = false;
        }
        else
        {
            throw new ArgumentException("Invalid unit");
        }
    }
}
