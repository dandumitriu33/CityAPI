using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Services;
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
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;

        public CitiesController(ICityInfoRepository cityInfoRepository,
                                IMapper mapper)
        {
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }



        //[HttpGet]
        //public JsonResult GetCities()
        //{
        //    return new JsonResult(CitiesDataStore.Current.Cities);
        //}

        //[HttpGet]
        //public IActionResult GetCities()
        //{
        //    return Ok(CitiesDataStore.Current.Cities);
        //}

        [HttpGet]
        public IActionResult GetCities()
        {
            var cityEntities = _cityInfoRepository.GetCities();
            // mapper should clean this up 
            //var results = new List<CityWithoutPointsOfInterestDto>();
            //foreach (var cityEntity in cityEntities)
            //{
            //    results.Add(new CityWithoutPointsOfInterestDto
            //    {
            //        Id = cityEntity.Id,
            //        Name = cityEntity.Name,
            //        Description = cityEntity.Description
            //    });
            //}
            //return Ok(results);

            return Ok(_mapper.Map<IEnumerable<CityWithoutPointsOfInterestDto>>(cityEntities));
        }
        #region older GetCity json and MEM
        //[HttpGet("{id}")]
        //public JsonResult GetCity(int id)
        //{
        //    return new JsonResult(CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id));
        //}

        //[HttpGet("{id}")]
        //public IActionResult GetCity(int id)
        //{
        //    // find city
        //    var cityToReturn = CitiesDataStore.Current.Cities
        //        .FirstOrDefault(c => c.Id == id);

        //    if (cityToReturn == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(cityToReturn);
        //}
        #endregion
        #region GetCity w/o mapper
        //[HttpGet("{id}")]
        //public IActionResult GetCity(int id, bool includePointsOfInterest = false)
        //{
        //    var city = _cityInfoRepository.GetCity(id, includePointsOfInterest);

        //    if (city == null)
        //    {
        //        return NotFound();
        //    }

        //    if (includePointsOfInterest)
        //    {
        //        var cityResult = new CityDto()
        //        {
        //            Id = city.Id,
        //            Name = city.Name,
        //            Description = city.Description
        //        };

        //        foreach (var poi in city.PointsOfInterest)
        //        {
        //            cityResult.PointsOfInterest.Add(
        //                new PointOfInterestDto()
        //                {
        //                    Id = poi.Id,
        //                    Name = poi.Name,
        //                    Description = poi.Description
        //                });
        //        }

        //        return Ok(cityResult);
        //    }

        //    var cityWithoutPointsOfInterestResult =
        //        new CityWithoutPointsOfInterestDto()
        //        {
        //            Id = city.Id,
        //            Name = city.Name,
        //            Description = city.Description
        //        };

        //    return Ok(cityWithoutPointsOfInterestResult);
        //}
        #endregion

        [HttpGet("{id}")]
        public IActionResult GetCity(int id, bool includePointsOfInterest = false)
        {
            var city = _cityInfoRepository.GetCity(id, includePointsOfInterest);

            if (city == null)
            {
                return NotFound();
            }

            if (includePointsOfInterest)
            {
                return Ok(_mapper.Map<CityDto>(city));
            }

            return Ok(_mapper.Map<CityWithoutPointsOfInterestDto>(city));
        }

    }
}
