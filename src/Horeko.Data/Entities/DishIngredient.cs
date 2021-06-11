using Newtonsoft.Json;

namespace Horeko.Data.Entities
{
    public class DishIngredient
    {
        public int IngredientId { get; set; }
        public float Amount { get; set; }

        [JsonIgnore]
        public Ingredient Ingredient { get; set; }
    }
}