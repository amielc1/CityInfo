﻿using CityInfo.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/cities/{cityId}/pointsofinterest")]
    public class PointsOfInterestController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetPointsOfInterest(int cityId)
        {
            var city = CitiesDataStore.Current.Cities
                .FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            return Ok(city.PointsOfInterest);
        }

        [HttpGet("{id}", Name = "GetPointOfInterest")]
        public IActionResult GetPointOfInterest(int cityId, int id)
        {
            var city = CitiesDataStore.Current.Cities
                .FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            // find point of interest
            var pointOfInterest = city.PointsOfInterest
                .FirstOrDefault(c => c.Id == id);

            if (pointOfInterest == null)
            {
                return NotFound();
            }

            return Ok(pointOfInterest);
        }

        [HttpPost]
        public IActionResult CreatePointOfInterest(int cityId,
            [FromBody] PointOfInterestForCreationDto pointOfInterest)
        {
            if (pointOfInterest.Description == pointOfInterest.Name)
            {
                ModelState.AddModelError(
                    "Description",
                    "The provided description should be different from the name.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            // demo purposes - to be improved
            var maxPointOfInterestId = CitiesDataStore.Current.Cities.SelectMany(
                             c => c.PointsOfInterest).Max(p => p.Id);

            var finalPointOfInterest = new PointOfInterestDto()
            {
                Id = ++maxPointOfInterestId,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description
            };

            city.PointsOfInterest.Add(finalPointOfInterest);

            return CreatedAtRoute(
                "GetPointOfInterest",
                new { cityId, id = finalPointOfInterest.Id },
                finalPointOfInterest);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePointOfInterest(int cityId, int id,
           [FromBody] PointOfInterestForCreationDto pointOfInterest)
        {
            var currentCity = CitiesDataStore.Current.Cities.FirstOrDefault(city => city.Id == cityId);
            var currentPoi = currentCity.PointsOfInterest.FirstOrDefault(poi => poi.Id == id);
            currentPoi.Name = pointOfInterest.Name;
            currentPoi.Description = pointOfInterest.Description;
            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult PartialliyUpdatePointOfInterest(int cityId, int id,
          [FromBody] JsonPatchDocument<PointOfInterestForUpdateDto> pointOfInterestPatchDoc)
        {
            var currentCity = CitiesDataStore.Current.Cities.FirstOrDefault(city => city.Id == cityId);
            var currentPoi = currentCity.PointsOfInterest.FirstOrDefault(poi => poi.Id == id);

            PointOfInterestForUpdateDto poiUpdate = new PointOfInterestForUpdateDto
            {
                Description = currentPoi.Description,
                Name = currentPoi.Name
            };

            pointOfInterestPatchDoc.ApplyTo(poiUpdate, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isUpdaveValid = TryValidateModel(poiUpdate);

            if (!isUpdaveValid)
            {
                return BadRequest(ModelState);
            }

            currentPoi.Name = poiUpdate.Name;
            currentPoi.Description = poiUpdate.Description;



            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePointOfInterest(int cityId, int id)
        {
            var currentCity = CitiesDataStore.Current.Cities.FirstOrDefault(city => city.Id == cityId);
            var currentPoi = currentCity.PointsOfInterest.FirstOrDefault(poi => poi.Id == id);
            currentCity.PointsOfInterest.Remove(currentPoi);
            return NoContent();
        }

      

    }
}