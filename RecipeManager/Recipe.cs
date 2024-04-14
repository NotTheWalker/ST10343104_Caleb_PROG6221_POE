namespace RecipeManager;

public class Recipe
{
    private int IngredientCount { get; set; } // The number of ingredients in the recipe
    private List<Ingredient> Ingredients { get; set; } // The ingredient names and amounts
    private int StepCount { get; set; } // The number of steps in the recipe
    private List<string> Steps { get; set; } // The description of each step

    private enum ScaleFactor
    {
        Half, Original, Double, Triple
    }
    private ScaleFactor Scale { get; set; } // The scale factor for the recipe

    public Recipe()
    {
        Ingredients = new List<Ingredient>();
        Steps = new List<string>();
    }

    public void AddIngredient(Ingredient ingredient)
    {
        Ingredients.Add(ingredient);
        IngredientCount++;
    }

    public void AddStep(string step)
    {
        Steps.Add(step);
        StepCount++;
    }
}
