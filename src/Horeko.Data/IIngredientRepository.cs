using Horeko.Data.Entities;
using System.Collections.Generic;

namespace Horeko.Data
{
    public interface IIngredientRepository
    {
        Ingredient Get(int id);
        IEnumerable<Ingredient> GetAll();
    }
}
