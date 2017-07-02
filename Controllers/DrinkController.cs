using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using BuildADrink.Data;
using BuildADrink.Models;

namespace BuildADrink.Controllers
{
    [Route("/[controller]")]
    public class DrinkController : Controller
    {
        public DrinkController()
        {
            _data = new DrinkData();
        }
        
        private readonly DrinkData _data;

        [HttpGet]
        public async Task<IList<Drink>> GetDrinks(string firstLetter)
        {
            return await _data.GetDrinks(firstLetter);
        }

        [HttpGet]
        public async Task<IList<Drink>> GetDrinks(int[] ingredients)
        {
            return await _data.GetDrinks(ingredients);
        }

        [HttpGet("{id}")]
        public async Task<Drink> GetDrink(int id)
        {
            return await _data.GetDrink(id);
        }

        [HttpGet("{id}/Ingredients")]
        public async Task<IList<DrinkIngredient>> GetIngredients(int id)
        {
            return await _data.GetDrinkIngredients(id);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _data?.Dispose();
            
            base.Dispose(disposing);
        }
    }
}