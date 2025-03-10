using Gymnastic.Persistence;
using Gymnastic.Infrastructure;
using Gymnastic.API.Middlewares;
using Gymnastic.Application.UseCases;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.ConfigurePersistenceDependcies();
builder.Services.ConfigureDatabase(builder.Configuration);
builder.Services.ConfigureIdentity();

builder.Services.ConfigureInfrasturctureDependcies();
builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.ConfigureMail(builder.Configuration);

builder.Services.AddInjectionApplication();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Services.SeedDummyData();
app.Services.DatabaseInitialize();

app.AddMiddlewares();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();