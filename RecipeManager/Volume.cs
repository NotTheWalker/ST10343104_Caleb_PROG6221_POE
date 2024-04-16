using System;
namespace RecipeManager
{
    public class Volume : AbstractMeasurable
    {
        // Conversion factors for volume units
        private const double TEASPOON = 4.92892;
        private const double TABLESPOON = 14.7868;
        private const double CUP = 236.588;
        private const double PINT = 473.176;
        private const double QUART = 946.353;
        private const double LITER = 1000;
        private const double GALLON = 3785.41;

        public enum UnitVolume // Enum for volume units
        {
            // Units arranged in ascending order for comparison purposes
            Milliliter, Teaspoon, Tablespoon, Cup, Pint, Quart, Liter, Gallon
        }

        public Volume(double milliliters, UnitVolume unit) // Constructor for the Volume class
        {
            BaseValue = milliliters;
            CurrentUnit = unit;
        }

        public override double ConvertTo(Enum unit) => unit switch // Converts the value to the specified unit
        {
            UnitVolume.Milliliter => BaseValue,
            UnitVolume.Teaspoon => BaseValue / TEASPOON,
            UnitVolume.Tablespoon => BaseValue / TABLESPOON,
            UnitVolume.Cup => BaseValue / CUP,
            UnitVolume.Pint => BaseValue / PINT,
            UnitVolume.Quart => BaseValue / QUART,
            UnitVolume.Liter => BaseValue / LITER,
            UnitVolume.Gallon => BaseValue / GALLON,
            _ => throw new ArgumentException("Invalid unit")
        };

        public override double ConvertToLargest(double Value) // Converts the value to the largest possible unit
        {
            // The unit used is stored in CurrentUnit
            // Then the value is converted to that unit and returned
            switch (Value)
            {
                case >= GALLON:
                    CurrentUnit = UnitVolume.Gallon;
                    return ConvertTo(UnitVolume.Gallon);
                case >= LITER:
                    CurrentUnit = UnitVolume.Liter;
                    return ConvertTo(UnitVolume.Liter);
                case >= QUART:
                    CurrentUnit = UnitVolume.Quart;
                    return ConvertTo(UnitVolume.Quart);
                case >= PINT:
                    CurrentUnit = UnitVolume.Pint;
                    return ConvertTo(UnitVolume.Pint);
                case >= CUP:
                    CurrentUnit = UnitVolume.Cup;
                    return ConvertTo(UnitVolume.Cup);
                case >= TABLESPOON:
                    CurrentUnit = UnitVolume.Tablespoon;
                    return ConvertTo(UnitVolume.Tablespoon);
                case >= TEASPOON:
                    CurrentUnit = UnitVolume.Teaspoon;
                    return ConvertTo(UnitVolume.Teaspoon);
                default:
                    CurrentUnit = UnitVolume.Milliliter;
                    return Value;
            }
        }

        public override double ConvertToLargest() => ConvertToLargest(BaseValue);

        public override void ConvertFrom(Enum unit) => BaseValue *= unit switch // Converts the value from the specified unit into the base unit
        {
            UnitVolume.Milliliter => 1,
            UnitVolume.Teaspoon => TEASPOON,
            UnitVolume.Tablespoon => TABLESPOON,
            UnitVolume.Cup => CUP,
            UnitVolume.Pint => PINT,
            UnitVolume.Quart => QUART,
            UnitVolume.Liter => LITER,
            UnitVolume.Gallon => GALLON,
            _ => throw new ArgumentException("Invalid unit"),
        };
    }
}

