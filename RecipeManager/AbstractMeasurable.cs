///<summary>
///Caleb Morse
///ST10343104
///Group 2
///References:
/// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/abstract
///</summary>

namespace RecipeManager;

public abstract class AbstractMeasurable
{
    protected double BaseValue { get; set; } //The value of the unit
    protected Enum? CurrentUnit { get; set; } //The last used unit

    public double GetBaseValue() //Returns the value of the unit
    {
        return BaseValue;
    }

    public void SetBaseValue(double value) //Sets the value of the unit
    {
        BaseValue = value;
    }

    public Enum? GetCurrentUnit() //Returns the last used unit
    {
        return CurrentUnit;
    }

    public abstract double ConvertTo(Enum unit); //Converts the value to the specified unit
    public abstract void ConvertFrom(Enum unit); //Converts the value from the specified unit into the base unit
    public abstract double ConvertToLargest(double Value); //Converts the value to the largest unit
    public abstract double ConvertToLargest(); //Converts the base value to the largest unit
}
