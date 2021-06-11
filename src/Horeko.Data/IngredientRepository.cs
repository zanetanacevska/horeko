using Horeko.Data.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace Horeko.Data
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly string _ingredientsFilePath;
        private readonly JsonSerializerSettings _settings = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            },
            Formatting = Formatting.Indented
        };

        public IngredientRepository()
        {
            _ingredientsFilePath = ConfigurationManager.AppSettings["IngredientsFilePath"];
        }

        public Ingredient Get(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Ingredient> GetAll()
        {           
            string ingredientsData = File.ReadAllText(_ingredientsFilePath);
            return JsonConvert.DeserializeObject<List<Ingredient>>(ingredientsData, _settings);
        }
    }
}
