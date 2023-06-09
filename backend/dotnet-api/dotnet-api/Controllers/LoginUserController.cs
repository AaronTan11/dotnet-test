using Microsoft.AspNetCore.Mvc;
using dotnet_api.Models;

namespace dotnet_api.Controllers;

[ApiController]
[Route("[controller]")]

public class LoginUserController : ControllerBase{

    private readonly ApplicationDbContext _dbContext;

    public LoginUserController(ApplicationDbContext dbContext){
        _dbContext = dbContext;
    }

    [HttpGet(Name = "GetUser")]
    public IActionResult Index(){
        var users = _dbContext.User.ToList(); // Retrieve all users from the database

        //Verification here

        return Ok(users);
    }
}