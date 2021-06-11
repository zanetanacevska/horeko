using System;
using System.Collections.Generic;

namespace Horeko.Models
{
    public class DishDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public IList<SimpleIngredientDto> Ingredients { get; set; } = new List<SimpleIngredientDto>();
    }
}