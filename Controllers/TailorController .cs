using Dreslay.DTO;
using Dreslay.Models;
using Dreslay.Repository;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace Dreslay.Controllers
{
    [Authorize(Roles = "Tailor,Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class TailorController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TailorController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var tailors = await _unitOfWork.Tailors.GetAllAsync();
            var dto = _mapper.Map<IEnumerable<TailorDto>>(tailors);
            return Ok(dto);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var tailor = await _unitOfWork.Tailors.GetByIdAsync(id);
            if (tailor == null)
                return NotFound("Tailor not found.");

            var dto = _mapper.Map<TailorDto>(tailor);
            return Ok(dto);
        }
        [Authorize(Roles = "Tailor ,Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TailorDto dto)
        {
            var tailor = _mapper.Map<Tailor>(dto);
            await _unitOfWork.Tailors.AddAsync(tailor);
            await _unitOfWork.Tailors.SaveAsync();
            return CreatedAtAction(nameof(GetById), new { id = tailor.Id }, dto);
        }
        [Authorize(Roles = "Tailor")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTailor(int id, TailorDto tailorDto)
        {
            var existingTailor = await _unitOfWork.Tailors.GetByIdAsync(id);
            if (existingTailor == null)
                return NotFound("Tailor not found.");

            _mapper.Map(tailorDto, existingTailor);
            _unitOfWork.Tailors.Update(existingTailor);
            await _unitOfWork.Tailors.SaveAsync();

            return Ok("Tailor updated successfully.");
        }
        [Authorize(Roles = "Admin,Tailor")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTailor(int id)
        {
            var tailor = await _unitOfWork.Tailors.GetByIdAsync(id);
            if (tailor == null)
                return NotFound("Tailor not found.");

            _unitOfWork.Tailors.Delete(tailor);
            await _unitOfWork.Tailors.SaveAsync();

            return Ok("Tailor deleted successfully.");
        }
    }
}
