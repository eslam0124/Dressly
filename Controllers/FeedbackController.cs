using AutoMapper;
using Dreslay.DTO;
using Dreslay.Models;
using Dreslay.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Client,Admin")]
[ApiController]
[Route("api/[controller]")]
public class FeedbackController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public FeedbackController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var feedbacks = await _unitOfWork.Feedbacks.GetAllAsync();
        var dtos = _mapper.Map<IEnumerable<FeedbackDto>>(feedbacks);
        return Ok(dtos);
    }

    [Authorize(Roles = "Client")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] FeedbackDto dto)
    {
        var feedback = _mapper.Map<Feedback>(dto);
        feedback.Date = DateTime.Now;

        await _unitOfWork.Feedbacks.AddAsync(feedback);
        await _unitOfWork.Feedbacks.SaveAsync();

        var tailorFeedbacks = await _unitOfWork.Feedbacks.FindAsync(f => f.TailorId == dto.TailorId);
        if (tailorFeedbacks.Any())
        {
            double avg = (double)tailorFeedbacks.Average(f => f.Rating);
            var tailor = await _unitOfWork.Tailors.GetByIdAsync(dto.TailorId);
            if (tailor != null)
            {
                tailor.AvgRating = avg;
                _unitOfWork.Tailors.Update(tailor);
                await _unitOfWork.Tailors.SaveAsync();
            }
        }

        return Ok("Feedback submitted.");
    }
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var feedback = await _unitOfWork.Feedbacks.GetByIdAsync(id);
        if (feedback == null)
            return NotFound("Feedback not found.");

        _unitOfWork.Feedbacks.Delete(feedback);
        await _unitOfWork.Feedbacks.SaveAsync();

        return Ok("Feedback deleted.");
    }
}
