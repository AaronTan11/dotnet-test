using Microsoft.AspNetCore.Mvc;
using dotnet_api.Models;

namespace dotnet_api.Controllers;

[ApiController]
[Route("[controller]")]

public class RegisterUserController : ControllerBase{

    private readonly ApplicationDbContext _dbContext;

    public RegisterUserController(ApplicationDbContext dbContext){
        _dbContext = dbContext;
    }

    [HttpPost(Name = "PostUser")]
    public IActionResult Index([FromQuery] User user){
        // Perform verification and validation on the user input

        // Add the user to the database
        _dbContext.User.Add(user);
        int affectedRows = _dbContext.SaveChanges();

        if (affectedRows > 0)
        {
            return Ok(user); // Changes were made, return success
        }
        else
        {
            return BadRequest("Failed to register user"); // No changes were made, return failure
        }
    }
}