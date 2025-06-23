using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.CustomActionFilters;
using NZWalks.Data;
using NZWalks.Models.Domain;
using NZWalks.Models.DTOs;
using NZWalks.Repositories;

namespace NZWalks.Controllers
{
    //https://localhost:###/api/regions
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RegionsController : ControllerBase
    {
        // private readonly NzWalksSqlServerDbContext dbContext;
        //
        // public RegionsController(NzWalksSqlServerDbContext dbContext)
        // {
        //     this.dbContext = dbContext;
        // }

        private readonly NZWalksDbContextMySQL dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(NZWalksDbContextMySQL dbContext, IRegionRepository regionRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        //Get all Regions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get Data from Database
            var regionsDomain = await regionRepository.GetAllAsync();

            //Map Domain Models to DTO's

            // var regionsDto = new List<RegionDto>();
            //
            // foreach (var region in regionsDomain)
            // {
            //     regionsDto.Add(new RegionDto()
            //     {
            //         Id = region.Id,
            //         Name = region.Name,
            //         Code = region.Code,
            //         RegionImageUrl = region.RegionImageUrl,
            //     });
            // }
            
            var regionsDto = mapper.Map<List<Region>>(regionsDomain);

            //return DTO to client
            return Ok(regionsDto);
        }

        //Get Region by ID

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //var region = dbContext.Regions.Find(id);
            var regionDomain = await regionRepository.GetByIdAsync(id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            var regionDto = mapper.Map<RegionDto>(regionDomain);

            return Ok(regionDto);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto regionRequest)
        {

            var regionDomain = mapper.Map<Region>(regionRequest);

            await regionRepository.CreateAsync(regionDomain);

            //Map Domain to DTO

            var regionDto = mapper.Map<RegionDto>(regionDomain);

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);  
            

        }

        //Update Region

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionDto updateRegionDto)
        {
            var regionDomainModel = mapper.Map<Region>(updateRegionDto);

            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            ;

            //Convert Domain to DTO

            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return Ok(regionDto);

        }

        //Delete Region

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomain = await regionRepository.DeleteAsync(id);
            if (regionDomain == null)
            {
                return NotFound();
            };

            var regionDto = mapper.Map<RegionDto>(regionDomain);

            return Ok(regionDto);
        }
    }
}
