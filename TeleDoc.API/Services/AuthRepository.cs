using Microsoft.AspNetCore.Identity;
using TeleDoc.API.Models.Account;
using TeleDoc.DAL.Entities;
using TeleDoc.DAL.Enums;
using TeleDoc.DAL.Extensions;

namespace TeleDoc.API.Services;

public class AuthRepository<T> : IAuthRepository<T> where T : ApplicationUser
{
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthRepository(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<CustomResponse> Register(RegisterViewModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);

        var response = new CustomResponse();
        if (user is not null)
        {
            response.Status = ResponseStatus.Duplicate;
            response.User = null;

            return response;
        }

        user = new ApplicationUser
        {
            UserName = model.Email,
            Email = model.Email
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            response.Status = ResponseStatus.Failed;
            response.User = null;
            return response;
        }

        response.Status = ResponseStatus.Succeeded;
        response.User = user;

        return response;
    }
    
    
    
    
}