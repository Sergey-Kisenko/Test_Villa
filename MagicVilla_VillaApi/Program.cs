using MagicVilla_VillaApi.CustomLogs;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Log.Logger = new LoggerConfiguration().MinimumLevel.Information().WriteTo.File("log/villaLogs.txt", rollingInterval: RollingInterval.Day).CreateLogger();
//builder.Host.UseSerilog(); //



builder.Services.AddControllers(option => option.ReturnHttpNotAcceptable = false).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IMyLogs, MyLogs>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
