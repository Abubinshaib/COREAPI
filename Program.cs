using COREAPI.Configurations;
using COREAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



// Add connection string from configuration
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

builder.Services.AddScoped<IUserService, UserService>();


builder.Services.AddAutoMapper(typeof(UserProfile));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
