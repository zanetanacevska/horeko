using System.Collections.Generic;
using System.Linq;
using Horeko.Data.Entities;
using Horeko.Models;

namespace Horeko.Utils
{
    public static class IngredientMapper
    {
        public static IEnumerable<UsageDto> MapToUsageDtos(IEnumerable<Ingredient> ingredients, IEnumerable<Dish> dishes)
        {
            var dtos = new List<UsageDto>();

            foreach (var ingredient in ingredients)
            {
                var dishIngredients = dishes.SelectMany(x => x.Ingredients).Where(x => x.IngredientId == ingredient.Id);

                var dto = new UsageDto
                {
                    Id = ingredient.Id,
                    Name = ingredient.Name,
                    TotalAmount = dishIngredients.Sum(x => x.Amount),
                    NumberOfDishes = dishIngredients.Count()
                };

                dtos.Add(dto);
            }

            return dtos;
        }
    }
}