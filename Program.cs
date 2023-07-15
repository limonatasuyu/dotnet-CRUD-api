global using dotnet_rpg.Models; // when `global` keyboard used, whole application knows this reference and we never have to add using directive again
global using dotnet_rpg.Services.CharacterService;
global using dotnet_rpg.Dtos.Character;
global using AutoMapper;
global using Microsoft.EntityFrameworkCore;
global using dotnet_rpg.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<DataContext>(options =>
options.UseSqlServer (builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<ICharacterService, CharacterService>();


var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<dotnet_rpg.AutoMapperProfile>();
});

IMapper mapper = mapperConfig.CreateMapper();

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
