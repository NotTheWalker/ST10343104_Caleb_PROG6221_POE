using System;
namespace RecipeManager
{
    public abstract class AbstractMeasurable // Abstract class for mass and volume, allows a dictionary to store both types of measurements
    {
        protected double BaseValue { get; set; } // The value in the base unit, either milligrams or milliliters
        protected Enum CurrentUnit { get; set; } // The current unit of measurement, set during unit upscaling

        public double GetBaseValue() // Getter for BaseValue
        {
            return BaseValue;
        }

        public void SetBaseValue(double value) // Setter for BaseValue
        {
            BaseValue = value;
        }

        public Enum GetCurrentUnit() // Getter for CurrentUnit, currently unused
        {
            return CurrentUnit;
        }

        // Abstract methods to be implemented in the Volume and Mass classes
        public abstract double ConvertTo(Enum unit); // Converts the value to the specified unit
        public abstract void ConvertFrom(Enum unit); // Converts the value from the specified unit into the base unit
        public abstract double ConvertToLargest(double Value); // Converts the value to the largest possible unit
        public abstract double ConvertToLargest(); // Converts the value to the largest possible unit, using the base value
    }
}

