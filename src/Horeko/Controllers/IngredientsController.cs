using System.Web.Http;
using Horeko.Data;
using Horeko.Utils;

namespace Horeko.Controllers
{
    [RoutePrefix("ingredients")]
    public class IngredientsController : ApiController
    {
        private readonly IIngredientRepository _ingridientRepository;
        private readonly IDishRepository _dishRepository;

        public IngredientsController(IIngredientRepository ingridientRepository, IDishRepository dishRepository)
        {
            _ingridientRepository = ingridientRepository;
            _dishRepository = dishRepository;
        }

        /// <summary>
        /// Get a list of ingredients and information how much of these ingredients are used in dishes.
        /// </summary>
        [HttpGet]
        [Route("usage")]
        public IHttpActionResult Usage()
        {
            var ingredients = _ingridientRepository.GetAll();
            var dishes = _dishRepository.GetAll();

            var dtos = IngredientMapper.MapToUsageDtos(ingredients, dishes);

            return Ok(dtos);
        }
    }
}
