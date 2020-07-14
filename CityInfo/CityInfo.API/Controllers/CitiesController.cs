using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/cities")]
    //[Route("api/[controller]")] // same as above as cities is the name of the controller
    public class CitiesController : ControllerBase
    {
        //[HttpGet]
        //public JsonResult GetCities()
        //{
        //    return new JsonResult(CitiesDataStore.Current.Cities);
        //}

        [HttpGet]
        public IActionResult GetCities()
        {
            return Ok(CitiesDataStore.Current.Cities);
        }

        //[HttpGet("{id}")]
        //public JsonResult GetCity(int id)
        //{
        //    return new JsonResult(CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id));
        //}

        [HttpGet("{id}")]
        public IActionResult GetCity(int id)
        {
            // find city
            var cityToReturn = CitiesDataStore.Current.Cities
                .FirstOrDefault(c => c.Id == id);

            if (cityToReturn == null)
            {
                return NotFound();
            }

            return Ok(cityToReturn);
        }
    }
}
