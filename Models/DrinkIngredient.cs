
namespace BuildADrink.Models
{
    public class DrinkIngredient : Ingredient
    {
        public decimal Amount { get; set; }

        public Measurement Measurement { get; set; }
    }
}