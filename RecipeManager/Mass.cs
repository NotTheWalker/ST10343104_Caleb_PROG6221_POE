using System;
namespace RecipeManager
{
    public class Mass : AbstractMeasurable
    {
        // Conversion factors for mass units
        private const double GRAM = 1000;
        private const double OUNCE = 28349.5;
        private const double POUND = 453592;
        private const double KILOGRAM = 1000000;

        public enum UnitMass // Enum for mass units
        {
            // Units arranged in ascending order for comparison purposes
            Milligram, Gram, Ounce, Pound, Kilogram
        }

        public Mass(double milligrams, UnitMass unit) // Constructor for the Mass class
        {
            BaseValue = milligrams;
            CurrentUnit = unit;
        }

        public override double ConvertTo(Enum unit) => unit switch // Converts the value to the specified unit
        {
            UnitMass.Milligram => BaseValue,
            UnitMass.Gram => BaseValue / GRAM,
            UnitMass.Ounce => BaseValue / OUNCE,
            UnitMass.Pound => BaseValue / POUND,
            UnitMass.Kilogram => BaseValue / KILOGRAM,
            _ => throw new ArgumentException("Invalid unit")
        };

        public override double ConvertToLargest(double Value) // Converts the value to the largest possible unit,
        {
            // The unit used is stored in CurrentUnit
            // Then the value is converted to that unit and returned
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

        public override void ConvertFrom(Enum unit) => BaseValue *= unit switch // Converts the value from the specified unit into the base unit
        {
            UnitMass.Milligram => 1,
            UnitMass.Gram => GRAM,
            UnitMass.Ounce => OUNCE,
            UnitMass.Pound => POUND,
            UnitMass.Kilogram => KILOGRAM,
            _ => throw new ArgumentException("Invalid unit"),
        };
    }
}

