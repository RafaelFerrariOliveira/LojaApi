using LojaApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(
builder.Configuration.GetConnectionString(
"DefaultConnection"
)
)
);

builder.Services.AddCors(options =>
{
options.AddPolicy(
"ReactPolicy",
policy =>
{
policy
.WithOrigins(
"http://localhost:3000"
)
.AllowAnyHeader()
.AllowAnyMethod();
});
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
options.SwaggerDoc(
"v1",
new OpenApiInfo
{
Title = "LojaApi",
Version = "v1"
}
);


options.AddSecurityDefinition(
    "Bearer",
    new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description =
            "Cole apenas o token JWT"
    });

options.AddSecurityRequirement(
    new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference =
                    new OpenApiReference
                    {
                        Type =
                            ReferenceType
                                .SecurityScheme,

                        Id =
                            "Bearer"
                    }
            },
            Array.Empty<string>()
        }
    });


});

var chaveJwt =
"MINHA_CHAVE_SUPER_SECRETA_123456";

builder.Services
.AddAuthentication(
JwtBearerDefaults
.AuthenticationScheme
)
.AddJwtBearer(options =>
{
options.TokenValidationParameters =
new TokenValidationParameters
{
ValidateIssuerSigningKey =
true,


        IssuerSigningKey =
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    chaveJwt
                )
            ),

        ValidateIssuer =
            false,

        ValidateAudience =
            false
    };


});

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.UseCors(
"ReactPolicy"
);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

// ---------------------------- ROTA DE LOGIN (necessária para gerar o token usado pelo SalasReuniaoController) ----------------------------
const string usuarioFixoEmail = "teste@teste.com";
const string usuarioFixoSenha = "123";

app.MapPost("/api/login", (LoginRequest login) =>
{
    if (login.Email != usuarioFixoEmail || login.Senha != usuarioFixoSenha)
    {
        return Results.Unauthorized();
    }

    var claims = new[]
    {
        new Claim(JwtRegisteredClaimNames.Sub, login.Email),
        new Claim(ClaimTypes.Name, login.Email)
    };

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveJwt));
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
        claims: claims,
        expires: DateTime.UtcNow.AddHours(2),
        signingCredentials: creds);

    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

    return Results.Ok(new { token = tokenString });
});

app.Run();


public record LoginRequest(string Email, string Senha);