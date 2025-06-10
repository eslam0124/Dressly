using Dreslay.DTO;
using Dreslay.Models;
using Dreslay.Repository;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
namespace Dreslay.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AdminController(IUnitOfWork unitOfWork , IMapper mapper )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var admins = await _unitOfWork.Admins.GetAllAsync();
            var dtos = _mapper.Map<IEnumerable<AdminDto>>(admins);
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var admin = await _unitOfWork.Admins.GetByIdAsync(id);
            if (admin == null)
                return NotFound();
            var dto = _mapper.Map<AdminDto>(admin);
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AdminDto dto)
        {
            var admin = _mapper.Map<Admin>(dto);
            await _unitOfWork.Admins.AddAsync(admin);
            await _unitOfWork.Admins.SaveAsync();
            return CreatedAtAction(nameof(GetById), new { id = admin.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AdminDto dto)
        {
            var existing = await _unitOfWork.Admins.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            _mapper.Map(dto, existing);
            _unitOfWork.Admins.Update(existing);
            await _unitOfWork.Admins.SaveAsync();
            return Ok("Admin updated.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var admin = await _unitOfWork.Admins.GetByIdAsync(id);
            if (admin == null)
                return NotFound();

            _unitOfWork.Admins.Delete(admin);
            await _unitOfWork.Admins.SaveAsync();
            return Ok("Admin deleted.");
        }
    }
}
