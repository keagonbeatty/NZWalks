using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.CustomActionFilters;
using NZWalks.Models.Domain;
using NZWalks.Models.DTOs;
using NZWalks.Repositories;


namespace NZWalks.Controllers
{
    //https://localhost:###/api/walks
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWalkRepository _repository;

        public WalksController(IMapper mapper, IWalkRepository repository)
        {
            this._mapper = mapper;
            this._repository = repository;
        }
        //CREATE Walk
        // POST: /api/walks
        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult> Create([FromBody] AddWalkRequestDto walkRequestDto)
        {
            var walkDomain = _mapper.Map<Walk>(walkRequestDto);
        
            await _repository.CreateAsync(walkDomain);
        
            //Map domain to DTO
            return Ok(_mapper.Map<WalkDto>(walkDomain));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalks([FromQuery] string? filterOn, [FromQuery] string? filterQuery, 
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery]int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            var walksDomain = await _repository.GetAllWalksAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);
            
            return Ok(_mapper.Map<List<WalkDto>>(walksDomain));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel = await _repository.GetByIdAsync(id);

            if (walkDomainModel == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<WalkDto>(walkDomainModel));
        }
        
        //Update Walk by ID
        // PUT: /api/Walks
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkRequest walkRequestDto)
        {
                var walkDomainModel = _mapper.Map<Walk>(walkRequestDto);
            
                walkDomainModel = await _repository.UpdateAsync(id, walkDomainModel);

                if (walkDomainModel == null)
                {
                    return NotFound();
                }
            
                return Ok(_mapper.Map<WalkDto>(walkDomainModel));
            
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var walkToDelete = await _repository.DeleteAsync(id);

            if (walkToDelete == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<WalkDto>(walkToDelete));
        }
        
    }
}