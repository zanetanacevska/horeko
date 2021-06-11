using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using Horeko.Data.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Horeko.Data
{
    public class DishRepository : IDishRepository
    {
        private readonly string _dishesFilePath;
        private readonly JsonSerializerSettings _settings = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            },
            Formatting = Formatting.Indented
        };
        private readonly IIngredientRepository _ingridientRepository;

        public DishRepository(IIngredientRepository ingridientRepository)
        {
            _dishesFilePath = ConfigurationManager.AppSettings["DishesFilePath"];
            _ingridientRepository = ingridientRepository;
        }

        public IEnumerable<Dish> GetAll(string dishName = null, DateTime? lastModifiedOnFrom = null, DateTime? lastModifiedOnTo = null)
        {
            string dishesData = File.ReadAllText(_dishesFilePath);

            var dishes = JsonConvert.DeserializeObject<IEnumerable<Dish>>(dishesData, _settings);
            var ingredients = _ingridientRepository.GetAll().ToList();

            foreach (var dish in dishes)
            {
                foreach (var dishIngredient in dish.Ingredients)
                {
                    dishIngredient.Ingredient = ingredients.FirstOrDefault(x => x.Id == dishIngredient.IngredientId);
                }
            }

            if (!string.IsNullOrEmpty(dishName))
            {
                dishes = dishes.Where(x => ContainsCaseInsensitive(x.Name, dishName));
            }

            if (lastModifiedOnFrom.HasValue)
            {
                dishes = dishes.Where(x => x.UpdatedOn >= lastModifiedOnFrom);
            }

            if (lastModifiedOnTo.HasValue)
            {
                dishes = dishes.Where(x => x.UpdatedOn <= lastModifiedOnTo);
            }

            return dishes.ToList();
        }

        public Dish Get(int id)
        {
            var dishes = GetAll();

            var dish = dishes.FirstOrDefault(x => x.Id == id);
            if (dish == null)
            {
                return null;
            }

            if (dish.ParentId.HasValue)
            {
                dish.Parent = dishes.FirstOrDefault(x => x.Id == dish.ParentId);
            }

            return dish;
        }

        public int Create(Dish dish)
        {
            var dishes = GetAll().ToList();

            dish.Id = dishes.Max(x => x.Id) + 1;

            dishes.Add(dish);

            string dishesJson = JsonConvert.SerializeObject(dishes, _settings);

            File.WriteAllText(_dishesFilePath, dishesJson);

            return dish.Id;
        }

        public IEnumerable<Dish> GetDishesWithPrices()
        {
            string dishesData = File.ReadAllText(_dishesFilePath);
            var dishes = JsonConvert.DeserializeObject<IEnumerable<Dish>>(dishesData, _settings);
            var ingredients = _ingridientRepository.GetAll().ToList();

            dishes = (IEnumerable<Dish>)dishes.SelectMany(x => x.Ingredients.Select(y => y.Ingredient.Price));

            return dishes.ToList();
        }

        public bool Exists(string name)
        {
            return GetAll().Any(x => string.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase));
        }
        private static bool ContainsCaseInsensitive(string container, string contains)
        {
            return container.IndexOf(contains, StringComparison.OrdinalIgnoreCase) >= 0;
        }
    }
}
