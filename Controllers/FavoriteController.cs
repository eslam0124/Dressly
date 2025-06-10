using AutoMapper;
using Dreslay.DTO;
using Dreslay.Models;
using Dreslay.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Client")]
[ApiController]
[Route("api/[controller]")]
public class FavoriteController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public FavoriteController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet("{clientId}")]
    public async Task<IActionResult> GetByClient(int clientId)
    {
        var favorites = await _unitOfWork.Favorites.FindAsync(f => f.ClientId == clientId);
        var dto = _mapper.Map<IEnumerable<FavoriteDto>>(favorites);
        return Ok(dto);
    }

    [HttpPost]
    public async Task<IActionResult> Add(FavoriteDto dto)
    {
        var favorite = _mapper.Map<Favorite>(dto);
        await _unitOfWork.Favorites.AddAsync(favorite);
        await _unitOfWork.Favorites.SaveAsync();
        return Ok("Added to favorites.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var favorite = await _unitOfWork.Favorites.GetByIdAsync(id);
        if (favorite == null) return NotFound();
        _unitOfWork.Favorites.Delete(favorite);
        await _unitOfWork.Favorites.SaveAsync();
        return Ok("Removed from favorites.");
    }
}