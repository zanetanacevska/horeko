using Horeko.Data.Entities;
using System;
using System.Collections.Generic;

namespace Horeko.Data
{
    public interface IDishRepository
    {
        IEnumerable<Dish> GetAll(string dishName = null, DateTime? lastModifiedOnFrom = null, DateTime? lastModifiedOnTo = null);
        Dish Get(int id);
        int Create(Dish dish);
        bool Exists(string name);
    }
}
