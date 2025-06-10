using AutoMapper;
using Dreslay.DTO;
using Dreslay.Models;
using Dreslay.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dreslay.Controllers
{
[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [Authorize(Roles = "Admin,Tailor,Client")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _unitOfWork.Categorys.GetAllAsync();
            var dto = _mapper.Map<IEnumerable<CategoryDto>>(categories);
            return Ok(dto);
        }

        [Authorize(Roles = "Admin,Tailor,Client")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _unitOfWork.Categorys.GetByIdAsync(id);
            if (category == null)
                return NotFound("Category not found.");

            var dto = _mapper.Map<CategoryDto>(category);
            return Ok(dto);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryDto dto)
        {
            var category = _mapper.Map<Category>(dto);
            await _unitOfWork.Categorys.AddAsync(category);
            await _unitOfWork.Categorys.SaveAsync();

            return CreatedAtAction(nameof(GetById), new { id = category.Id }, dto);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryDto dto)
        {
            var existing = await _unitOfWork.Categorys.GetByIdAsync(id);
            if (existing == null)
                return NotFound("Category not found.");

            _mapper.Map(dto, existing);
            _unitOfWork.Categorys.Update(existing);
            await _unitOfWork.Categorys.SaveAsync();

            return Ok("Category updated successfully.");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _unitOfWork.Categorys.GetByIdAsync(id);
            if (category == null)
                return NotFound("Category not found.");

            _unitOfWork.Categorys.Delete(category);
            await _unitOfWork.Categorys.SaveAsync();

            return Ok("Category deleted successfully.");
        }
    }
}
