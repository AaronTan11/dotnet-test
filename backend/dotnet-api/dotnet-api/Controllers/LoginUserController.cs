using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using dotnet_api.Models;

namespace dotnet_api.Controllers;

[ApiController]
[Route("/login")]

public class LoginUserController : ControllerBase{

    private readonly ApplicationDbContext _dbContext;

    public LoginUserController(ApplicationDbContext dbContext){
        _dbContext = dbContext;
    }

        [HttpPost]
        public IActionResult Login()
        {
            var users = _dbContext.User.ToList(); // Retrieve all users from the database
            return Ok(users);
        }

        //Not done, requires frontend to send data to backend for validation
        [HttpPost("google")]
        public async Task<IActionResult> GoogleLogin() //cannot accept parameters for now
        {
            // Get the authenticated user's information from the token
            var userName = User.FindFirst("name")?.Value;
            var userEmail = User.FindFirst("email")?.Value;
            var userPassword = User.FindFirst("password")?.Value;

            // Check if the user already exists in the database
            var existingUser = await _dbContext.User.FirstOrDefaultAsync(u => u.email == userEmail);

            if (existingUser != null)
            {
                return Ok("Google authentication successful");
            }
            else 
            {
                var newUser = new User{
                    name = userName,
                    email = userEmail,
                    password = userPassword
                };

                // Register the user in the database if record doesnt exist
                _dbContext.User.Add(newUser);

                await _dbContext.SaveChangesAsync();

                return Ok("Google authentication successful"); // Changes were made, return success
            }
    }
}