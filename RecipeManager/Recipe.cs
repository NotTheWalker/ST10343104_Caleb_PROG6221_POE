namespace RecipeManager;

public class Recipe
{
    private Dictionary<string, AbstractMeasurable> Ingredients { get; set; } // The ingredients and their amounts
    private string[] Steps { get; set; } // The steps to make the recipe

    public enum ScaleFactor
    {
        Half, Original, Double, Triple
    }
    private ScaleFactor Scale { get; set; }

    public Func<ScaleFactor, double> GetScaleFactorValue = (Scale) =>
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

    public Recipe(Dictionary<string, AbstractMeasurable> ingredients, string[] steps)
    {
        Ingredients = ingredients;
        Steps = steps;
        Scale = ScaleFactor.Original;
    }

    public Recipe()
    {
        Ingredients = new Dictionary<string, AbstractMeasurable>();
        Steps = new string[0];
    }

    public void AddIngredient(string name, AbstractMeasurable amount)
    {
        Ingredients.Add(name, amount);
    }

    public void PrintRecipe()
    {
        Console.WriteLine("Ingredients:");
        foreach (var ingredient in Ingredients)
        {
            AbstractMeasurable scaledAmount = ingredient.Value;
            scaledAmount.SetBaseValue(scaledAmount.GetBaseValue() * GetScaleFactorValue(Scale));
            Console.WriteLine($"{ingredient.Key}: {ingredient.Value.ConvertToLargest()} {ingredient.Value.GetCurrentUnit()}");
        }

        Console.WriteLine("\nSteps:");
        for (int i = 0; i < Steps.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {Steps[i]}");
        }
    }
}
