using System.Linq;
using Horeko.Data.Entities;
using Horeko.Models;

namespace Horeko.Utils
{
    public static class DishMapper
    {
        public static DishWithParentDto DishWithParentDto(Dish dish)
        {
            var parentDto = dish.ParentId.HasValue ? new ParentDishDto
            {
                Id = dish.Parent.Id,
                Name = dish.Parent.Name
            } : null;
            var ingredientDtos = dish.Ingredients.Select(x => new IngredientDto
            {
                Id = x.IngredientId,
                Name = x.Ingredient.Name,
                Amount = x.Amount
            }).ToList();

            return new DishWithParentDto
            {
                Id = dish.Id,
                Name = dish.Name,
                LastUpdatedOn = dish.UpdatedOn,
                ParentDish = parentDto,
                Ingredients = ingredientDtos
            };
        }

        public static DishDto MapToDishDto(Dish dish)
        {
            var ingredientDtos = dish.Ingredients.Select(x => new SimpleIngredientDto
            {
                Id = x.IngredientId,
                Name = x.Ingredient.Name
            }).ToList();

            return new DishDto
            {
                Id = dish.Id,
                Name = dish.Name,
                LastUpdatedOn = dish.UpdatedOn,
                Ingredients = ingredientDtos
            };
        }

        public static DishWithPriceDto MapToDishWithPriceDto(Dish dish)
        {
            return new DishWithPriceDto
            {
                Id = dish.Id,
                Name = dish.Name,
                Price = dish.Ingredients.Sum(x => x.Ingredient.Price)
            };
        }
    }
}