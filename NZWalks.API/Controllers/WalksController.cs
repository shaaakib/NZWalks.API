﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repository.Interface;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;

        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            // Map the DTO to the domain model
            var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

            await walkRepository.CreateAsync(walkDomainModel);


            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

        //Get: /api/Walks/filterOn?=Name&filterQuery=Track&sortBy=Name&isAscending=true&pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
           [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
           [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            var walksDomainModel = await walkRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true,
                pageNumber, pageSize);

            // Map Doamin Model to Dto

            return Ok(mapper.Map<List<WalkDto>>(walksDomainModel));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.GetByIdAsync(id);

            if (walkDomainModel == null) return NotFound();

            // Map Domain Model to DTO
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalkRequestDto updateWalkRequestDto)
        {
            // Map DTO to Domain Model
            var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);
            walkDomainModel = await walkRepository.UpdatedAsync(id, walkDomainModel);

            if (walkDomainModel == null) return NotFound();

            // Map Dmain Model to DTO
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

        // Delete a Walk By Id
        // DELETE: /api/Walks/{id}
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deleteWalkDomainsModel = await walkRepository.DeleteAsync(id);

            if (deleteWalkDomainsModel == null) return NotFound();

            // Map Domain Model to DTO

            return Ok(mapper.Map<WalkDto>(deleteWalkDomainsModel));

        }
    }
}
