using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NET_CYBER_API.BLL.Interfaces;
using NET_CYBER_API.BLL.Services;
using NET_CYBER_API.DAL.Data;
using NET_CYBER_API.DAL.Interfaces;
using NET_CYBER_API.DAL.Repositories;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(option => option.UseSqlServer(connectionString));

builder.Services.AddScoped<ITicketRepository, TicketEntityRepository>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IUtilisateurRepository, UtilisateurEntityRepository>();
builder.Services.AddScoped<IUtilisateurService, UtilisateurService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddSwaggerGen(c => {
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            };
            //options.Events = new JwtBearerEvents
            //{
            //    OnTokenValidated = context =>
            //    {
            //        // Retrieve the claims principal
            //        var claimsIdentity = context.Principal.Identity as ClaimsIdentity;

            //        // Retrieve the role claim from the principal
            //        var roleClaim = claimsIdentity.Claims.FirstOrDefault(c => c.Type == "Roles");
            //        var idClaim = claimsIdentity.Claims.FirstOrDefault(c => c.Type == "id");

            //        // If role claim is found, add it to the principal's identity
            //        if (roleClaim != null)
            //        {
            //            var role = roleClaim.Value;
            //            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role));
            //        }
            //        if (idClaim != null)
            //        {
            //            var id = idClaim.Value;
            //            claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, id));
            //        }

            //        return Task.CompletedTask;
            //    }
            //};
        });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
