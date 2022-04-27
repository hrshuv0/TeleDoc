using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TeleDoc.API.Area.Doctors.Models;
using TeleDoc.API.Context;
using TeleDoc.API.Dtos.DoctorsDto;
using TeleDoc.DAL.Entities;

namespace TeleDoc.API.Services;

public class DoctorRepository : IDoctorRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public DoctorRepository(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext, IMapper mapper)
    {
        _userManager = userManager;
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<List<Doctor>?> GetDoctorListAsync()
    {
        var result = await _userManager.Users.ToListAsync();
        
        var data = _mapper.Map<List<Doctor>>(result);

        return data;
    }

    public async Task<Doctor> GetDoctorByEmail(string email)
    {
        var result = await _userManager.FindByEmailAsync(email);
        
        var data = _mapper.Map<Doctor>(result);
        var dataToReturn = _mapper.Map<DoctorDetailsDto>(data);

        return data;
    }

    public async Task<Doctor> GetDoctorByName(string name)
    {
        // var result = await _userManager.FindByNameAsync(name);
        var result = await _dbContext.Users.FirstOrDefaultAsync(d => d.Name != null && d.Name.Contains(name));
        var data = _mapper.Map<Doctor>(result);
        
        var dataToReturn = _mapper.Map<DoctorDetailsDto>(data);
        
        return data;
    }

    public Task<Doctor> GetDoctorBySpeciality(string speciality)
    {
        throw new NotImplementedException();
    }

    public Task<Doctor> UpdateDoctorByEmail(Doctor doctor)
    {
        throw new NotImplementedException();
    }
}