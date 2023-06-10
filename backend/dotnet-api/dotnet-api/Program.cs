using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);
var service = builder.Services;
var configuration = builder.Configuration;

// Add services to the container.
service.AddEntityFrameworkNpgsql().AddDbContext<ApplicationDbContext>(opt =>
        opt.UseNpgsql(builder.Configuration.GetConnectionString("PostgresServer")));
//Adding Google Auth
service.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => options.LoginPath = "/login")
    .AddGoogle(options =>
    {
        options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

        options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
        options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];

        options.Scope.Add("email"); // to get primary email address
        options.Scope.Add("profile"); // to get personal public info like name, image etc.
    });
service.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
service.AddEndpointsApiExplorer();
service.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication(); //Enable Authentications

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

