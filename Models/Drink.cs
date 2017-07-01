using System.Collections.Generic;

namespace BuildADrink.Models
{
    public class Drink
    {
        public Drink()
        {
            Ingredients = new List<DrinkIngredient>();
        }

        public IList<DrinkIngredient> Ingredients { get; }

        public int Id { get; set; }

        public string Name { get; set; }
        
        public string Description { get; set; }
    }
}