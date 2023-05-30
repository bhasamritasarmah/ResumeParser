using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ResumeServices;
using ResumeUploadAndDisplayBackend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<IDatabaseSettings>(
    builder.Configuration.GetSection(nameof(IDatabaseSettings)));

builder.Services.AddSingleton<IDatabaseSettings>(p =>
    p.GetRequiredService<IOptions<IDatabaseSettings>>().Value);

builder.Services.AddSingleton<IMongoClient>(p =>
    new MongoClient(builder.Configuration.GetValue<string>("DatabaseSettings:ConnectionString")));

builder.Services.AddScoped<IResumeService, ResumeService>();

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
