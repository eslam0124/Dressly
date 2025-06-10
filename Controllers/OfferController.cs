using AutoMapper;
using Dreslay.DTO;
using Dreslay.Models;
using Dreslay.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dreslay.Controllers
{
    [Authorize(Roles = "Admin,Tailor")]
    [ApiController]
    [Route("api/[controller]")]
    public class OfferController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OfferController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin,Tailor")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var offers = await _unitOfWork.Offers.GetAllAsync();
            var dto = _mapper.Map<IEnumerable<OfferDto>>(offers);
            return Ok(dto);
        }

        [Authorize(Roles = "Admin,Tailor")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var offer = await _unitOfWork.Offers.GetByIdAsync(id);
            if (offer == null)
                return NotFound();
            return Ok(_mapper.Map<OfferDto>(offer));
        }

        [Authorize(Roles = "Tailor")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OfferDto dto)
        {
            var offer = _mapper.Map<Offer>(dto);
            await _unitOfWork.Offers.AddAsync(offer);
            await _unitOfWork.Offers.SaveAsync();
            return Ok("Offer created successfully.");
        }

        [Authorize(Roles = "Tailor")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] OfferDto dto)
        {
            var existing = await _unitOfWork.Offers.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            _mapper.Map(dto, existing);
            _unitOfWork.Offers.Update(existing);
            await _unitOfWork.Offers.SaveAsync();
            return Ok("Offer updated successfully.");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var offer = await _unitOfWork.Offers.GetByIdAsync(id);
            if (offer == null)
                return NotFound();
            _unitOfWork.Offers.Delete(offer);
            await _unitOfWork.Offers.SaveAsync();
            return Ok("Offer deleted successfully.");
        }
    }
}