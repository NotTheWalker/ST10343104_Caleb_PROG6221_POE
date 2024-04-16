///<summary>
///Caleb Morse
///ST10343104
///Group 2
///References:
/// https://www.geeksforgeeks.org/c-sharp-dictionary-with-examples/
/// https://learn.microsoft.com/en-us/dotnet/api/system.math.round?view=net-8.0
///</summary>

namespace RecipeManager;

public class Recipe
{
    private Dictionary<string, AbstractMeasurable> Ingredients { get; set; } // The ingredients and their amounts
    private string[] Steps { get; set; } // The steps to make the recipe

    public enum ScaleFactor // Enum for the scale factors
    {
        Half, Original, Double, Triple
    }
    private ScaleFactor Scale { get; set; } // The scale factor for the recipe

    public Func<ScaleFactor, double> GetScaleFactorValue = (Scale) => // Function to get the scale factor value
    {
        return Scale switch
        {
            ScaleFactor.Half => 0.5,
            ScaleFactor.Original => 1,
            ScaleFactor.Double => 2,
            ScaleFactor.Triple => 3,
            _ => throw new ArgumentException("Invalid scale factor"),
        };
    };

    public void SetScale(ScaleFactor scale) // Sets the scale factor
    {
        Scale = scale;
    }

    public void SetSteps(string[] steps) // Sets the steps for the recipe, currently unused
    {
        Steps = steps;
    }

    public Recipe(Dictionary<string, AbstractMeasurable> ingredients, string[] steps) // Constructor for the Recipe class, currently unused
    {
        Ingredients = ingredients;
        Steps = steps;
        Scale = ScaleFactor.Original;
    }

    public Recipe() // Default constructor for the Recipe class
    {
        Ingredients = new Dictionary<string, AbstractMeasurable>();
        Steps = Array.Empty<string>();
    }

    public void AddIngredient(string name, AbstractMeasurable amount) // Adds an ingredient to the recipe
    {
        Ingredients.Add(name, amount);
    }

    public void AddStep(string step) // Adds a step to the recipe
    {
        string[] resizedSteps = new string[Steps.Length + 1];
        Array.Copy(Steps, resizedSteps, Steps.Length);
        resizedSteps[resizedSteps.Length-1] = step;
        Steps = resizedSteps;
    }

    public void PrintRecipe() // Prints the recipe
    {
        Console.WriteLine("Ingredients:");
        foreach (var ingredient in Ingredients) // Loops through the ingredients
        {
            AbstractMeasurable scaledAmount = ingredient.Value; // Gets the amount of the ingredient
            scaledAmount.SetBaseValue(scaledAmount.GetBaseValue() * GetScaleFactorValue(Scale)); // Scales the amount of the ingredient
            Console.WriteLine($"{ingredient.Key}: {Math.Round(ingredient.Value.ConvertToLargest(), 2)} {ingredient.Value.GetCurrentUnit()}"); // Prints the ingredient
        }

        Console.WriteLine("\nSteps:");
        for (int i = 0; i < Steps.Length; i++) // Loops through the steps
        {
            Console.WriteLine($"{i + 1}. {Steps[i]}");
        }
    }
}
