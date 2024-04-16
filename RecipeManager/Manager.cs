///<summary>
///Caleb Morse
///ST10343104
///Group 2
///References:
/// https://learn.microsoft.com/en-us/dotnet/csharp/nullable-references#nullable-variable-annotations
/// https://www.c-sharpcorner.com/article/change-console-foreground-and-background-color-in-c-sharp/
///</summary>

using static RecipeManager.Mass;
using static RecipeManager.Volume;

namespace RecipeManager;

public class Manager
{
    private Recipe Recipe { get; set; } // The recipe to be managed
    private bool IsRecipeAdded { get; set; } = false; // Whether a recipe has been added

    public Manager() // Constructor for the Manager class
    {
        Recipe = new Recipe();
    }

    public void Start() // The main menu for the Recipe Manager, controls the flow of the program
    {
        Console.WriteLine("Welcome to the Recipe Manager!");
        do // Loop to keep the program running
        {
            // Main menu
            ColourMessage("What would you like to do?", ConsoleColor.Cyan);
            Console.WriteLine("1. Add a recipe");
            Console.WriteLine("2. View a recipe");
            Console.WriteLine("3. Exit");


            // User input
            string? input = Console.ReadLine();
            switch (input) // Switch statement to handle user input
            {
                case "1": // Add a recipe
                    // Confirmation messages
                    string reset1 = "Would you like to reset the recipe?";
                    string reset2 = "Please confirm that you would like to reset the recipe.";
                    if (IsRecipeAdded && Confirm(reset1) && Confirm(reset2)) // If a recipe is added and the user confirms twice
                    {
                        Recipe = new Recipe(); // Reset the recipe
                        IsRecipeAdded = false; // Set the recipe added flag to false
                    }
                    else if (IsRecipeAdded) // If a recipe is added and the user does not confirm twice
                    {
                        ErrorMessage("Recipe not reset."); // Display an error message
                    }
                    else
                    {
                        AddRecipe(); // Add a recipe
                    }
                    break;
                case "2": // View a recipe
                    if (!IsRecipeAdded)
                    {
                        ErrorMessage("No recipe added. Please add a recipe first."); // Display an error message
                    }
                    else
                    {
                        ViewRecipe(); // View a recipe
                    }
                    break;
                case "3": // Exit the program
                    Environment.Exit(0);
                    break;
                default: // Invalid input
                    ErrorMessage("Invalid input. Please try again."); // Display an error message
                    break;
            }
            // End of main menu
        } while (true); // Loop to keep the program running
    }

    public void AddRecipe() // Method to add a recipe
    {
        Console.WriteLine("Enter the number of ingredients:");
        int numIngredients = int.Parse(Console.ReadLine()!); // Parse the number of ingredients, convert to int

        for (int i = 0; i < numIngredients; i++) // Loop to add ingredients
        {
            try // Try block to catch exceptions, mostly for parsing errors
            {
                Console.WriteLine($"Enter the name of ingredient {i + 1}:"); // Prompt for ingredient name
                string ingredientName = Console.ReadLine()!; // Read the ingredient name

                Console.WriteLine($"Enter the amount of {ingredientName} (amount only):"); // Prompt for ingredient amount
                double amount = double.Parse(Console.ReadLine()!); // Read the ingredient amount

                Console.WriteLine("Select a unit:"); // Prompt for unit selection
                Console.WriteLine("1. Volume");
                Console.WriteLine("2. Mass");
                string unitInput = Console.ReadLine()!; // Read the unit selection
                switch (unitInput) // Switch statement to handle unit selection
                {
                    case "1": // Volume unit
                        UnitVolume unitVolume = ParseVolumeUnit(); // Parse the volume unit
                        Volume volume = new(amount, unitVolume); // Create a new volume object
                        volume.ConvertFrom(unitVolume); // Convert the volume from the selected unit
                        Recipe.AddIngredient(ingredientName, volume); // Add the ingredient to the recipe
                        break;
                    case "2":
                        // The same as the above case, but for mass units
                        UnitMass massUnit = ParseMassUnit();
                        Mass mass = new(amount, massUnit);
                        mass.ConvertFrom(massUnit);
                        Recipe.AddIngredient(ingredientName, mass);
                        break;
                    default:
                        ErrorMessage("Invalid input. Please try again."); // Display an error message
                        i--; // Decrement the loop counter to try again
                        break;
                }
            }
            catch (Exception) // Catch any exceptions
            {
                ErrorMessage("An error occurred. Please try again."); // Display an error message
                i--; // Decrement the loop counter to try again
            }
        }

        Console.WriteLine("Enter the number of steps:"); // Prompt for the number of steps
        int numSteps = int.Parse(Console.ReadLine()!); // Read the number of steps

        string[] steps = new string[numSteps]; // Create an array to store the steps
        for (int i = 0; i < numSteps; i++) // Loop to add steps
        {
            Console.WriteLine($"Enter description for step {i + 1}:"); // Prompt for step description
            steps[i] = Console.ReadLine()!; // Read the step description
        }
        Recipe.SetSteps(steps); // Set the steps for the recipe

        Console.WriteLine("Recipe added successfully!"); // Display a success message
        IsRecipeAdded = true; // Set the recipe added flag to true
    }

    public void ViewRecipe() // Method to view a recipe
    {
        ColourMessage("\nWould you like to scale the recipe?", ConsoleColor.Cyan); // Prompt to scale the recipe
        // Display the scale options
        Console.WriteLine("1. Half");
        Console.WriteLine("2. Original");
        Console.WriteLine("3. Double");
        Console.WriteLine("4. Triple");

        string? scaleInput = Console.ReadLine(); // Read the scale input
        Recipe.SetScale(scaleInput switch // Switch statement to handle the scale input
        {
            "1" => Recipe.ScaleFactor.Half,
            "2" => Recipe.ScaleFactor.Original,
            "3" => Recipe.ScaleFactor.Double,
            "4" => Recipe.ScaleFactor.Triple,
            _ => throw new ArgumentException("Invalid scale factor"),
        });
        Recipe.PrintRecipe(); // Print the recipe

        if (Confirm("Would you like to reset the scale?")) // If the user confirms to reset the scale 
        {
            Recipe.SetScale(Recipe.ScaleFactor.Original); // Reset the scale to the original
        }
    }

    private static bool Confirm(string message) // Method to confirm an action
    {
        ColourMessage(message, ConsoleColor.Yellow); // Display the confirmation message
        Console.WriteLine("1. Yes"); 
        Console.WriteLine("2. No");
        string? input = Console.ReadLine(); // Read the confirmation input
        return input switch // Return a boolean based on the input
        {
            "1" => true,
            "2" => false,
            _ => throw new ArgumentException("Invalid input"),
        };
    }

    private static void ErrorMessage(string message) // Method to display an error message
    {
        ColourMessage(message, ConsoleColor.Red); // Display the error message
    }

    private static void ColourMessage(string message, ConsoleColor color) // Method to display a message with a specified color
    {
        Console.ForegroundColor = color; // Set the console foreground color
        Console.WriteLine(message); // Display the message
        Console.ResetColor(); // Reset the console color
    }

    private static UnitVolume ParseVolumeUnit() // Method to parse a volume unit
    {
        Console.WriteLine("Select a volume unit:"); // Prompt for volume unit selection
        // Display the volume unit options
        Console.WriteLine("1. Milliliter");
        Console.WriteLine("2. Teaspoon");
        Console.WriteLine("3. Tablespoon");
        Console.WriteLine("4. Cup");
        Console.WriteLine("5. Pint");
        Console.WriteLine("6. Quart");
        Console.WriteLine("7. Liter");
        Console.WriteLine("8. Gallon");

        string? volumeUnitInput = Console.ReadLine(); // Read the volume unit input
        return volumeUnitInput switch // Return the volume unit based on the input
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

    private static UnitMass ParseMassUnit() // Same as the above method, but for mass units
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
