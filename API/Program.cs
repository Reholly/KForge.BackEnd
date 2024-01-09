using API.Extensions;
using Application.Extensions;
using Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("config.json");

var config = builder.Configuration;

builder.Services.AddSwagger();
builder.Services.AddControllers();
builder.Services.AddErrorHandling();

builder.Services.AddConfiguredIdentity();
builder.Services.AddInfrastructure(config); 

if (builder.Environment.IsDevelopment())
{
    //builder.Services.AddTestingDataInfrastructure(config); 
}
else
{
    
}
   
    
builder.Services.AddApplicationServices();
builder.Services.AddHandlers();
builder.Services.AddValidators();

builder.Services.AddJwtAuthentication(config);
builder.Services.AddAuthorization();
builder.Services.AddMemoryCache();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseErrorHandling();

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();