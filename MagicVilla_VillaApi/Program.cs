using MagicVilla_VillaApi;
using MagicVilla_VillaApi.Data;
using MagicVilla_VillaApi.Repository;
using MagicVilla_VillaApi.Repository.Interfaces;
using MagicVilla_VillaApi.Service;
using MagicVilla_VillaApi.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDBContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddControllers(option => option.ReturnHttpNotAcceptable = false).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddScoped<IVillaRepository, VillaRepository>();
builder.Services.AddScoped<IVillaNumberRepository, VillaNumberRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IVillaServices, VillaService>();
builder.Services.AddScoped<IVillaNumberService, VillaNumberService>();


var key = builder.Configuration.GetValue<string>("ApiSettings:Secret");
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(x => {
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

//builder.Services.AddSingleton<IMyLogs, MyLogs>(); пример подключения зависимостей
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme { 
    Description="JWT Authorization header using the Bearer scham. \r\n\r\n"+
    "Enter 'Bearer' [space] and then your token in the text input below. \r\n\r\n"+
    "Example: \"Bearer 12345abcdef \" "
    ,
    Name="Authorization",
    In=ParameterLocation.Header,
    Scheme="Bearer"
    
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id="Bearer"
                },
                Scheme = "oauth2",
                Name ="Bearer",
                In = ParameterLocation.Header
                
            },
            new List<string>()
        }
    });
});
var app = builder.Build();


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
