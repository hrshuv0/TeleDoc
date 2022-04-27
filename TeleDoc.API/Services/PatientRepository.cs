using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TeleDoc.API.Area.Patients.Models;
using TeleDoc.API.Dtos.PatientsDto;
using TeleDoc.DAL.Entities;

namespace TeleDoc.API.Services;

public class PatientRepository : IPatientRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;

    public PatientRepository(UserManager<ApplicationUser> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<List<Patient>?> GetPatientListAsync()
    {
        var result = await _userManager.Users.ToListAsync();
        
        var data = _mapper.Map<List<Patient>>(result);

        return data;

    }

    public async Task<Patient> GetPatientByEmail(string email)
    {
        var result = await _userManager.FindByEmailAsync(email);
        
        var data = _mapper.Map<Patient>(result);
        var dataToReturn = _mapper.Map<PatientDetailsDto>(data);

        return data;
    }

    public Task<Patient> UpdatePatientByEmail(string email)
    {
        throw new NotImplementedException();
    }
}