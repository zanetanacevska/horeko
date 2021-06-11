using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Horeko.Data;
using Horeko.Data.Entities;
using Horeko.Models;
using Horeko.Utils;

namespace Horeko.Controllers
{
    [RoutePrefix("dishes")]
    public class DishesController : ApiController
    {
        private readonly IDishRepository _dishRepository;

        public DishesController(IDishRepository dishRepository)
        {
            _dishRepository = dishRepository;
        }

        /// <summary>
        /// Get all dishes with all the related information.
        /// </summary>
        /// <param name="dishName">The name or part of the name of a dish.</param>
        /// <param name="lastModifiedOnFrom">Formatted as yyyy-MM-dd HH:mm:ss</param>
        /// <param name="lastModifiedOnTo">Formatted as yyyy-MM-dd HH:mm:ss</param>
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll(string dishName = null, DateTime? lastModifiedOnFrom = null, DateTime? lastModifiedOnTo = null)
        {
            var dishes = _dishRepository.GetAll(dishName, lastModifiedOnFrom, lastModifiedOnTo);

            var dtos = dishes.Select(x => DishMapper.MapToDishDto(x));

            return Ok(dtos);
        }

        /// <summary>
        /// Get a dish by id.
        /// </summary>
        /// <param name="id"></param>
        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage Get(int id)
        {
            var dish = _dishRepository.Get(id);

            if (dish == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Conflict, $"Dish with id: {id} is not found");
            }

            var dto = DishMapper.DishWithParentDto(dish);

            return Request.CreateResponse(HttpStatusCode.OK, dto);
        }

        /// <summary>
        /// Create a new dish.
        /// </summary>
        /// <param name="model"></param>
        [HttpPost]
        [Route("")]
        public HttpResponseMessage Create([FromBody] CreateDishDto model)
        {
            if (_dishRepository.Exists(model.Name))
            {
                ModelState.AddModelError("model.Name", $"Dish with name: {model.Name} already exists");
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelStateHelper.ToDictionary(ModelState);

                return Request.CreateResponse(HttpStatusCode.Conflict, errors);
            }

            var dish = new Dish
            {
                Name = model.Name,
                UpdatedOn = DateTime.UtcNow,
                Ingredients = model.Ingredients
            };

            var id = _dishRepository.Create(dish);

            return Request.CreateResponse(HttpStatusCode.Created, id);
        }

        /// <summary>
        /// Get a list of dishes with prices.
        /// </summary>
        [HttpGet]
        [Route("prices")]
        public IHttpActionResult Prices()
        {
            var dishes = _dishRepository.GetAll();

            var dtos = dishes.Select(x => DishMapper.MapToDishWithPriceDto(x));

            return Ok(dtos);
        }
    }
}
