using AutoMapper;
using Dreslay.DTO;
using Dreslay.Models;
using Dreslay.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dreslay.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FabricController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FabricController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var fabrics = await _unitOfWork.Fabrics.GetAllAsync();
            var dtos = _mapper.Map<IEnumerable<FabricDto>>(fabrics);
            return Ok(dtos);
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var fabric = await _unitOfWork.Fabrics.GetByIdAsync(id);
            if (fabric == null)
                return NotFound();

            var dto = _mapper.Map<FabricDto>(fabric);
            return Ok(dto);
        }
        [Authorize(Roles = "Tailor,Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FabricDto dto)
        {
            var fabric = _mapper.Map<Fabric>(dto);
            await _unitOfWork.Fabrics.AddAsync(fabric);
            await _unitOfWork.SaveAsync();

            return CreatedAtAction(nameof(GetById), new { id = fabric.Id }, dto);
        }
        [Authorize(Roles = "Tailor,Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] FabricDto dto)
        {
            var existing = await _unitOfWork.Fabrics.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            _mapper.Map(dto, existing);
            _unitOfWork.Fabrics.Update(existing);
            await _unitOfWork.SaveAsync();

            return Ok("Fabric updated.");
        }
        [Authorize(Roles = "Tailor,Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var fabric = await _unitOfWork.Fabrics.GetByIdAsync(id);
            if (fabric == null)
                return NotFound();

            _unitOfWork.Fabrics.Delete(fabric);
            await _unitOfWork.SaveAsync();

            return Ok("Fabric deleted.");
        }
    }
}
