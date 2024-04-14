namespace RecipeManager;

public abstract class AbstractMeasurable
{
    protected double BaseValue { get; set; }
    protected Enum? CurrentUnit { get; set; }

    public double GetBaseValue()
    {
        return BaseValue;
    }

    public void SetBaseValue(double value)
    {
        BaseValue = value;
    }

    public Enum? GetCurrentUnit()
    {
        return CurrentUnit;
    }

    public abstract double ConvertTo(Enum unit);
    public abstract void ConvertFrom(Enum unit);
    public abstract double ConvertToLargest(double Value);
    public abstract double ConvertToLargest();
}
