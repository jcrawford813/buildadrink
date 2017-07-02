using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

using BuildADrink.Models;

namespace BuildADrink.Data
{
    public class DrinkData : DataBase
    {
        public async Task<IList<Drink>> GetDrinks(string firstLetter)
        {
            const string sql = @"SELECT * FROM Drink WHERE DrinkName LIKE @firstLetter + '%'";
            
            return await ExecuteAndMap(async (cmd) => {
                var drinks = new List<Drink>();
                
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@firstLetter", firstLetter);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        drinks.Add(
                            new Drink()
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString(),
                                Description =  reader["Description"]?.ToString()
                            }
                        );
                    }
                }
                
                return drinks;
            });
        }

        public async Task<IList<Drink>> GetDrinks(int[] ingredientIds)
        {
            const string sql =
                @"SELECT d.* FROM Drink d JOIN DrinkIngredient di ON d.DrinkId = di.DrinkId JOIN @ingredients i ON di.IngredientId = i.Id";

            return await ExecuteAndMap(async (cmd) =>
            {
                var drinks = new List<Drink>();
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                var ingredients = cmd.Parameters.AddWithValue("@ingredients", ingredientIds.ToIdTableParameter());
                ingredients.SqlDbType = SqlDbType.Structured;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        drinks.Add(
                            new Drink()
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString(),
                                Description = reader["Description"]?.ToString()
                            }
                        );
                    }
                }
                
                return drinks;
            });         
        }

        public async Task<Drink> GetDrink(int drinkId)
        {
            const string sql = @"SELECT * FROM Drink WHERE Id = @drinkId";

            return await ExecuteAndMap(async (cmd) =>
            {
                var drink = new Drink();

                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@drinkId", drinkId);


                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        drink.Id = Convert.ToInt32(reader["Id"]);
                        drink.Name = reader["Name"].ToString();
                        drink.Description = reader["Description"]?.ToString();
                    }
                }

                return drink;
            });
        }

        public async Task<IList<DrinkIngredient>> GetDrinkIngredients(int drinkId)
        {
            const string sql =
                @"SELECT i.Name AS [IngredientName], Amount, m.Name as MeasurementName FROM DrinkIngredient di JOIN Ingredient i ON di.IngredientId = i.IngredientId JOIN Measurement m ON m.MeasurementId = di.MeasurementId WHERE d.DrinkId = @drinkId";
            
            return await ExecuteAndMap(async (cmd) =>
            {
                var ingredients = new List<DrinkIngredient>();

                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@drinkId", drinkId);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var ingredient = new DrinkIngredient()
                        {
                            Name = reader["IngredientName"].ToString(),
                            Amount = Convert.ToDecimal(reader["Amount"]),
                            Measurement = new Measurement()
                            {
                                Name = reader["MeasurementName"].ToString()
                            }
                        };
                        
                        ingredients.Add(ingredient);
                    }
                }

                return ingredients;
            });
        }
    }
}