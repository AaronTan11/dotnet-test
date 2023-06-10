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
    public async Task<IActionResult> RegisterUser([FromQuery] User user){

        // Add the user to the database
        _dbContext.User.Add(user);
        int affectedRows = await _dbContext.SaveChangesAsync();

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