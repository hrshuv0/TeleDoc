using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeleDoc.API.Area.Admins.Models;
using TeleDoc.API.Context;

namespace TeleDoc.API.Area.Admins.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;

    public AdminController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public string Index()
    {
        return "Wahhh... Admin controller";
    }

    [HttpPost("ambulance")]
    public async Task<IActionResult> AddAmbulance([FromBody] Ambulance ambulance)
    {
        if (!ModelState.IsValid) return BadRequest(ambulance);
        
         _dbContext.Ambulances!.Add(ambulance);
        await _dbContext.SaveChangesAsync();

        return Ok(ambulance);
    }
    
    [AllowAnonymous]
    [HttpPost("hospital")]
    public async Task<IActionResult> AddHospital([FromBody] Hospital hospital)
    {
        if (!ModelState.IsValid) return BadRequest(hospital);
        
        _dbContext.Hospitals!.Add(hospital);
        await _dbContext.SaveChangesAsync();

        return Ok(hospital);
    }
    
    [AllowAnonymous]
    [HttpGet("hospital")]
    public async Task<IActionResult> GetHospital()
    {
        var hospitals = _dbContext.Hospitals.Include(h => h.Location).ToList();

        return Ok(hospitals);
    }
    


}