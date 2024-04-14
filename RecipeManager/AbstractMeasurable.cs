namespace RecipeManager;

public abstract class AbstractMeasurable
{
    private double BaseValue { get; set; }
    private Enum CurrentUnit { get; set; }

    public abstract AbstractMeasurable(double baseValue, Enum unit)
    {
        BaseValue = baseValue; // The value of the unit in the base unit
        CurrentUnit = unit; // The current unit of the value
    }

    public abstract double ConvertTo(Enum unit);
    public abstract double ConvertToLargest();
}
