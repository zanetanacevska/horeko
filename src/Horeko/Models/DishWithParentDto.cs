using System;
using System.Collections.Generic;

namespace Horeko.Models
{
    public class DishWithParentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public ParentDishDto ParentDish { get; set; }
        public IList<IngredientDto> Ingredients { get; set; } = new List<IngredientDto>();
    }
}