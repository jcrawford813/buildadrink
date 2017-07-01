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
            var drinks = new List<Drink>();

            const string sql = @"SELECT * FROM Drink WHERE DrinkName LIKE @firstLetter + '%'";
            using (var cmd = Connection.CreateCommand())
            {
                await Connection.OpenAsync();

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
                                Description =  reader["Description"].ToString()
                            }
                        );
                    }
                }
            }

            return drinks;
        }

        public async Task<IList<Drink>> GetDrinks(int[] ingredientIds)
        {
            var drinks = new List<Drink>();

            const string sql =
                @"SELECT d.* FROM Drink d JOIN DrinkIngredient di ON d.DrinkId = di.DrinkId JOIN @ingredients i ON di.IngredientId = i.Id";
            
            using (var cmd = Connection.CreateCommand())
            {
                await Connection.OpenAsync();

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
                                Description =  reader["Description"].ToString()
                            }
                        );
                    }
                }
            }
            
            return drinks;
        }

        public async Task<Drink> GetDrink(int drinkId)
        {
            const string sql = "SELECT * FROM Drink WHERE Id = @drinkId";
            Drink drink = new Drink();
            
            using (var cmd = Connection.CreateCommand())
            {
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@drinkId", drinkId);


                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        drink.Id = Convert.ToInt32(reader["Id"]);
                        drink.Name = reader["Name"].ToString();
                        drink.Description = reader["Description"].ToString();
                    }
                }
            }

            return drink;
        }
    }
}