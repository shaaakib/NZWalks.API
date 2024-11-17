﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    // https://localhost:1234/api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;

        public RegionsController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET ALL REGIONS
        // https://localhost:1234/api/regions
        [HttpGet]
        public IActionResult GetAll()
        {  
            // Get Data From Database - Domain models
            var regionsDomain = dbContext.Regions.ToList();

            // Map Dmain Models to DTOS

            var regionsDto = new List<RegionDto>();
            foreach (var regionDomain in regionsDomain)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl

                });

            }
                // Return DTOs
                return Ok(regionsDto);
        }

        // GET SINGLE REgion (Get Region by ID)
        // GET: https://localhost:portnumber/api/regions/{id}

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            // Get Region Dmain Model From Database
            var regionDomain =  dbContext.Regions.Find(id);
            //var region = dbContext.Regions.FirstOrDefault(x => x.Id == id);
           
           if(regionDomain == null)
            {
                return NotFound();
            }

            // Map/Convert Region Domain Model to Region DTO

            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

           // Return DTO back to client
           return Ok(regionDto);

        }


        // POST: to create new Region
        // POST: https://localhost:portnumber/api/region

        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // Map or Convert Dto to Domain Model
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };

            // Use Domain Model to create Region

            dbContext.Regions.Add(regionDomainModel);
            dbContext.SaveChanges();

            // Map Domain model back to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, regionDto);
        }


        // Update Region
        // PUT: https://localhost:portnumber/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
          // Check if region exists
           var regionDomainModel = dbContext.Regions.FirstOrDefault(x => x.Id == id);

            if(regionDomainModel == null)
            {
                return NotFound();
            }

            // Map DTO to Domain model
            regionDomainModel.Code = updateRegionRequestDto.Code;
            regionDomainModel.Name = updateRegionRequestDto.Name;
            regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            dbContext.SaveChanges();

            // Convert Domain Model to DTO

            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return Ok(regionDto);

        }
    }
}
