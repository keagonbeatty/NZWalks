using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Data;
using NZWalks.Models.Domain;
using NZWalks.Models.DTOs;

namespace NZWalks.Controllers
{
    //https://localhost:###/api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NzWalksSqlServerDbContext dbContext;

        public RegionsController(NzWalksSqlServerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //Get all Regions
        [HttpGet]
        public IActionResult GetAll()
        {
            //Get Data from Database
            var regionsDomain = dbContext.Regions.ToList();

            //Map Domain Models to DTO's

            var regionsDto = new List<RegionDto>();

            foreach (var region in regionsDomain)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = region.Id,
                    Name = region.Name,
                    Code = region.Code,
                    RegionImageUrl = region.RegionImageUrl,
                });
            }

            //return DTO to client
            return Ok(regionsDto);
        }

        //Get Region by ID

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            //var region = dbContext.Regions.Find(id);
            var regionDomain = dbContext.Regions.FirstOrDefault(x => x.Id == id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl,

            };

            return Ok(regionDto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDto regionRequest)
        {
            var regionDomain = new Region
            {
                Id = new Guid(),
                Name = regionRequest.Name,
                Code = regionRequest.Code,
                RegionImageUrl = regionRequest.RegionImageUrl,
            };

            dbContext.Regions.Add(regionDomain);

            dbContext.SaveChanges();

            //Map Domain to DTO

            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl,
            };

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);

        }

        //Update Region

        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionDto updateRegionDto)
        {
            var regionDomainModel = dbContext.Regions.FirstOrDefault(x => x.Id == id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }
            ;

            regionDomainModel.Code = updateRegionDto.Code;
            regionDomainModel.Name = updateRegionDto.Name;
            regionDomainModel.RegionImageUrl = updateRegionDto.RegionImageUrl;

            dbContext.SaveChanges();

            //Convert Domain to DTO

            var regionDTO = new RegionDto
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
            };

            return Ok(regionDTO);

        }

        //Delete Region

        [HttpDelete]
        [Route("{id: Guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var regionDomain = dbContext.Regions.FirstOrDefault(y => y.Id == id);

            if (regionDomain == null)
            {
                return NotFound();
            }
            ;

            dbContext.Regions.Remove(regionDomain);

            dbContext.SaveChanges();

            return Ok();
        }
    }
}
