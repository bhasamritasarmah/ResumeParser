using DatabaseConnectionSettings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ResumeServices;
using ResumeUploadAndDisplayBackend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

/* The following line connects the file 'DatabaseSettings' to the 
 * details of the section 'DatabaseSettings' in appsettings.json.
 * This helps to get the connection string, database name, and 
 * collection name. */
builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection(nameof(DatabaseSettings)));

/* The following line connects the interface 'IDatabaseSettings' to the
 * corresponding file 'DatabaseSettings' which contains the implementation. 
 * AddSingleton creates only a single connection throughout the entire program
 * and provides a single point of access to various codes in the program. This
 * type of connection is useful for heavy connections like that of a database. */
builder.Services.AddSingleton<IDatabaseSettings>(p =>
    p.GetRequiredService<IOptions<DatabaseSettings>>().Value);

/* The following line provides the database connection string from the appsettings.json
 * file to the IMongoClient. This helps the client to establish connections to the 
 * given database and collections. This is also a Singleton connection. */
builder.Services.AddSingleton<IMongoClient>(p =>
    new MongoClient(builder.Configuration.GetValue<string>("DatabaseSettings:ConnectionString")));

/* The following line connects the interface 'IResumeService' to the file 'ResumeService'.
 * AddScoped provides a new object or instance with each new HTTP request. */
builder.Services.AddScoped<IResumeService, ResumeService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var provider = builder.Services.BuildServiceProvider();
var configuration = provider.GetRequiredService<IConfiguration>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
