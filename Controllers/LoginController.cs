using Microsoft.AspNetCore.Mvc;
using Dreslay.Models;
using Dreslay.Repository;
using Dreslay.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    public LoginController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var admins = await _unitOfWork.Admins.GetAllAsync();
        var admin = admins.FirstOrDefault(a =>
            (a.User_Name == loginDto.UserNameOrEmail || a.Email == loginDto.UserNameOrEmail) &&
            a.Password == loginDto.Password);
        if (admin != null)
            return Ok(new { Role = "Admin", Message = "Login successful" });

        var clients = await _unitOfWork.Clients.GetAllAsync();
        var client = clients.FirstOrDefault(c =>
            (c.User_Name == loginDto.UserNameOrEmail || c.Email == loginDto.UserNameOrEmail) &&
            c.Password == loginDto.Password);
        if (client != null)
            return Ok(new { Role = "Client", Message = "Login successful" });

        var tailors = await _unitOfWork.Tailors.GetAllAsync();
        var tailor = tailors.FirstOrDefault(t =>
            (t.User_Name == loginDto.UserNameOrEmail || t.Email == loginDto.UserNameOrEmail) &&
            t.Password == loginDto.Password);
        if (tailor != null)
            return Ok(new { Role = "Tailor", Message = "Login successful" });

        return Unauthorized("Invalid username/email or password");
    }
}
