using Dreslay.DTO;
using Dreslay.Models;
using Dreslay.Repository;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace Dreslay.Controllers
{
    [Authorize(Roles = "Client,Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClientController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clients = await _unitOfWork.Clients.GetAllAsync();
            var dtos = _mapper.Map<IEnumerable<ClientDto>>(clients);
            return Ok(dtos);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var client = await _unitOfWork.Clients.GetByIdAsync(id);
            if (client == null)
                return NotFound();
            var dto = _mapper.Map<ClientDto>(client);
            return Ok(dto);
        }
        [Authorize(Roles = "Client")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClientDto dto)
        {
            var client = _mapper.Map<Client>(dto);
            await _unitOfWork.Clients.AddAsync(client);
            await _unitOfWork.Clients.SaveAsync();
            return CreatedAtAction(nameof(GetById), new { id = client.Id }, dto);
        }
        [Authorize(Roles = "Client")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ClientDto dto)
        {
            var existing = await _unitOfWork.Clients.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            _mapper.Map(dto, existing);
            _unitOfWork.Clients.Update(existing);
            await _unitOfWork.Clients.SaveAsync();
            return Ok("Client updated.");
        }
        [Authorize(Roles = "Admin,Client")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var client = await _unitOfWork.Clients.GetByIdAsync(id);
            if (client == null)
                return NotFound();

            _unitOfWork.Clients.Delete(client);
            await _unitOfWork.Clients.SaveAsync();
            return Ok("Client deleted.");
        }
    }
}
