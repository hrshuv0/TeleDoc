using Microsoft.AspNetCore.Mvc;

namespace TeleDoc.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : Controller
{
    // GET
    public string Index()
    {
        return "Auth Controller is working";
    }
}