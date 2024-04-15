using static RecipeManager.Mass;
using static RecipeManager.Volume;

namespace RecipeManager;

public class Manager
{
    private Recipe Recipe { get; set; }
    private bool IsRecipeAdded { get; set; } = false;

    public Manager()
    {
        Recipe = new Recipe();
    }

    public void Start()
    {
        Console.WriteLine("Welcome to the Recipe Manager!");
        do
        {
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. Add a recipe");
            Console.WriteLine("2. View a recipe");
            Console.WriteLine("3. Exit");
    
            string reset1 = "Would you like to reset the recipe?";
            string reset2 = "Please confirm that you would like to reset the recipe.";
            string? input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    if (IsRecipeAdded && Confirm(reset1) && Confirm(reset2))
                    {
                        Recipe = new Recipe();
                        IsRecipeAdded = false;
                    }
                    else if (IsRecipeAdded)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Recipe not reset.");
                        Console.ResetColor();
                    }
                    else
                    {
                        AddRecipe();
                    }
                    break;
                case "2":
                    ViewRecipe();
                    break;
                case "3":
                    Environment.Exit(0);
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input. Please try again.");
                    Console.ResetColor();
                    Start();
                    break;
            }
        } while (true);
    }

    public void AddRecipe()
    {
        Console.WriteLine("Enter the number of ingredients:");
        int numIngredients = int.Parse(Console.ReadLine()!);

        for (int i = 0; i < numIngredients; i++)
        {
            try
            {
                Console.WriteLine($"Enter the name of ingredient {i + 1}:");
                string ingredientName = Console.ReadLine()!;

                Console.WriteLine($"Enter the amount of {ingredientName} (amount only):");
                double amount = double.Parse(Console.ReadLine()!);

                Console.WriteLine("Select a unit:");
                Console.WriteLine("1. Volume");
                Console.WriteLine("2. Mass");
                string unitInput = Console.ReadLine()!;
                switch (unitInput)
                {
                    case "1":
                        UnitVolume unitVolume = ParseVolumeUnit();
                        Volume volume = new(amount, unitVolume);
                        volume.ConvertFrom(unitVolume);
                        Recipe.AddIngredient(ingredientName, volume);
                        break;
                    case "2":
                        UnitMass massUnit = ParseMassUnit();
                        Mass mass = new(amount, massUnit);
                        mass.ConvertFrom(massUnit);
                        Recipe.AddIngredient(ingredientName, mass);
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid unit. Please try again.");
                        Console.ResetColor();
                        i--;
                        break;
                }
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("An error occurred. Please try again.");
                Console.ResetColor();
                i--;
            }
        }

        Console.WriteLine("Enter the number of steps:");
        int numSteps = int.Parse(Console.ReadLine()!);

        string[] steps = new string[numSteps];
        for (int i = 0; i < numSteps; i++)
        {
            Console.WriteLine($"Enter description for step {i + 1}:");
            steps[i] = Console.ReadLine()!;
        }
        Recipe.SetSteps(steps);

        Console.WriteLine("Recipe added successfully!");
        IsRecipeAdded = true;
        Start();
    }

    public void ViewRecipe()
    {
        Console.WriteLine("Would you like to scale the recipe?");
        Console.WriteLine("1. Half");
        Console.WriteLine("2. Original");
        Console.WriteLine("3. Double");
        Console.WriteLine("4. Triple");

        string? scaleInput = Console.ReadLine();
        Recipe.SetScale(scaleInput switch
        {
            "1" => Recipe.ScaleFactor.Half,
            "2" => Recipe.ScaleFactor.Original,
            "3" => Recipe.ScaleFactor.Double,
            "4" => Recipe.ScaleFactor.Triple,
            _ => throw new ArgumentException("Invalid scale factor"),
        });
        Recipe.PrintRecipe();

        Console.WriteLine("Would you like to reset the scale?");
        Console.WriteLine("1. Yes");
        Console.WriteLine("2. No");

        string? resetInput = Console.ReadLine();
        if (resetInput == "1")
        {
            Recipe.SetScale(Recipe.ScaleFactor.Original);
        }

        Start();
    }

    private static bool Confirm(string message)
    {
        Console.WriteLine(message);
        Console.WriteLine("1. Yes");
        Console.WriteLine("2. No");
        string? input = Console.ReadLine();
        return input switch
        {
            "1" => true,
            "2" => false,
            _ => throw new ArgumentException("Invalid input"),
        };
    }

    private static UnitVolume ParseVolumeUnit()
    {
        Console.WriteLine("Select a volume unit:");
        Console.WriteLine("1. Milliliter");
        Console.WriteLine("2. Teaspoon");
        Console.WriteLine("3. Tablespoon");
        Console.WriteLine("4. Cup");
        Console.WriteLine("5. Pint");
        Console.WriteLine("6. Quart");
        Console.WriteLine("7. Liter");
        Console.WriteLine("8. Gallon");

        string? volumeUnitInput = Console.ReadLine();
        return volumeUnitInput switch
        {
            "1" => UnitVolume.Milliliter,
            "2" => UnitVolume.Teaspoon,
            "3" => UnitVolume.Tablespoon,
            "4" => UnitVolume.Cup,
            "5" => UnitVolume.Pint,
            "6" => UnitVolume.Quart,
            "7" => UnitVolume.Liter,
            "8" => UnitVolume.Gallon,
            _ => throw new ArgumentException("Invalid volume unit"),
        };
    }

    private static UnitMass ParseMassUnit()
    {
        Console.WriteLine("Select a mass unit:");
        Console.WriteLine("1. Milligram");
        Console.WriteLine("2. Gram");
        Console.WriteLine("3. Ounce");
        Console.WriteLine("4. Pound");
        Console.WriteLine("5. Kilogram");

        string? massUnitInput = Console.ReadLine();
        return massUnitInput switch
        {
            "1" => UnitMass.Milligram,
            "2" => UnitMass.Gram,
            "3" => UnitMass.Ounce,
            "4" => UnitMass.Pound,
            "5" => UnitMass.Kilogram,
            _ => throw new ArgumentException("Invalid mass unit"),
        };
    }
}
