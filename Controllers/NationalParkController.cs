﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkyAPI.Models;
using ParkyAPI.Models.Dtos;
using ParkyAPI.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NationalParkController : Controller
    {
        private INationalParkRepository _npRepo;
        private IMapper _mapper;

        public NationalParkController(INationalParkRepository npRepo, IMapper mapper)
        {
            _npRepo = npRepo;
            _mapper = mapper;

        }




        [HttpGet]
        public IActionResult GetNationalParks()
        {
            var objList = _npRepo.GetNationalParks();

            var objDto = new List<NationalParkDto>();

            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<NationalParkDto>(obj));
            }

            return Ok(objDto);
        }

        [HttpGet("{nationalParkId:int}", Name = "GetNationalPark")]
         public IActionResult GetNationalPark(int nationalParkId)
        {
            var obj = _npRepo.GetNationalPark(nationalParkId);
           if(obj == null)
            {
                return NotFound();
            }
            var objDto = _mapper.Map<NationalParkDto>(obj);

            return Ok(objDto);
        }

        [HttpPost]
        public IActionResult CreateNationalPark([FromBody] NationalParkDto nationalParkDto)
        {
            if (nationalParkDto == null)
            {
                return BadRequest(ModelState);
            }

            if(_npRepo.NationalParkExists(nationalParkDto.Name))
            {
                ModelState.AddModelError("", "National Park Exists");
                return StatusCode(404, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var nationalParkObj = _mapper.Map<NationalPark>(nationalParkDto); 

            if(!_npRepo.CreateNationalPark(nationalParkObj))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {nationalParkObj.Name}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetNationalPark", new {nationalParkId = nationalParkObj.Id }, nationalParkObj);
        }

        [HttpPatch("{nationalParkId:int}", Name = "UpdateNationalPark")]
        public IActionResult UpdateNationalPark(int nationalParkId, [FromBody] NationalParkDto nationalParkDto)
        {
            if (nationalParkDto == null || nationalParkId != nationalParkDto.Id )
            {
                return BadRequest(ModelState);
            }

            var updateParkObj = _mapper.Map<NationalPark>(nationalParkDto);

            if (!_npRepo.UpdateNationalPark(updateParkObj))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {updateParkObj.Name}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetNationalPark", new { nationalParkId = updateParkObj.Id }, updateParkObj);


        }

        [HttpDelete("{nationalParkId:int}", Name = "DeleteNationalPark")]
        public IActionResult DeleteNationaPark(int nationalParkId)
        {
            var obj = _npRepo.GetNationalPark(nationalParkId);
            if (obj == null)
            {
                return NotFound();
            }

           var dltobj = _mapper.Map<NationalPark>(obj);
            var deleteobj = _npRepo.DeleteNationalPark(dltobj);

            if (!deleteobj)
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {dltobj.Name}");
            }

            return NoContent();

        }

    }



}