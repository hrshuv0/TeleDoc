using Microsoft.AspNetCore.Mvc;
using TeleDoc.API.Models.Account;
using TeleDoc.API.Services;
using TeleDoc.DAL.Entities;
using TeleDoc.DAL.Enums;
using TeleDoc.DAL.Exceptions;

namespace TeleDoc.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : Controller
{
    private readonly IAuthRepository<ApplicationUser> _authRepo;

    public AuthController(IAuthRepository<ApplicationUser> authRepo)
    {
        _authRepo = authRepo;
    }

    // GET
    public string Index()
    {
        return "Auth Controller is working";
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid) return BadRequest();

        var result = await _authRepo.Register(model);

        return result.Status switch
        {
            ResponseStatus.Succeeded => Ok(result.User),
            ResponseStatus.Duplicate => throw new DuplicateException(model.Email),
            ResponseStatus.Failed => throw new FailedException("register", model.Email),
            _ => BadRequest()
        };
    }
}