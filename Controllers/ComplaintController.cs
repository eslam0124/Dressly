using AutoMapper;
using Dreslay.DTO;
using Dreslay.Models;
using Dreslay.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Client,Admin")]
[Route("api/[controller]")]
[ApiController]
public class ComplaintController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ComplaintController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpPost]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> Create([FromBody] ComplaintDto dto)
    {
        var complaint = _mapper.Map<Complaint>(dto);
        await _unitOfWork.Complaints.AddAsync(complaint);
        await _unitOfWork.Complaints.SaveAsync();
        return Ok("Complaint submitted.");
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll()
    {
        var complaints = await _unitOfWork.Complaints.GetAllAsync();
        var dto = _mapper.Map<IEnumerable<ComplaintDto>>(complaints);
        return Ok(dto);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var complaint = await _unitOfWork.Complaints.GetByIdAsync(id);
        if (complaint == null) return NotFound();
        _unitOfWork.Complaints.Delete(complaint);
        await _unitOfWork.Complaints.SaveAsync();
        return Ok("Deleted");
    }
}
