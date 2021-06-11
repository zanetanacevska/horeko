using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Attributes;
using Horeko.Data.Entities;

namespace Horeko.Models
{
    [Validator(typeof(CreateDishDtoValidator))]
    public class CreateDishDto
    {
        public string Name { get; set; }
        public IList<DishIngredient> Ingredients { get; set; } = new List<DishIngredient>();
    }

    public class CreateDishDtoValidator : AbstractValidator<CreateDishDto>
    {
        public CreateDishDtoValidator()
        {
            RuleFor(x => x.Name).Length(50);
            RuleFor(x => x.Ingredients).NotEmpty();
        }
    }
}