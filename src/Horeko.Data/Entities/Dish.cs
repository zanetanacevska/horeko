using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Horeko.Data.Entities
{
    public class Dish
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public DateTime UpdatedOn { get; set; }

        [JsonIgnore]
        public Dish Parent { get; set; }
        public IList<DishIngredient> Ingredients { get; set; } = new List<DishIngredient>();
    }
}