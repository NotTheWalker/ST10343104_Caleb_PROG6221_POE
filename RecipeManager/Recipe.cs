namespace RecipeManager;

public class Recipe
{
    private Dictionary<string, AbstractMeasurable> Ingredients { get; set; }
    private string[] Steps { get; set; }


    public Recipe(Dictionary<string, AbstractMeasurable> ingredients, string[] steps)
    {
        Ingredients = ingredients;
        Steps = steps;
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
}
