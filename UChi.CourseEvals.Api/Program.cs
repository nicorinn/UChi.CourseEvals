using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using UChi.CourseEvals.Api.Services;
using UChi.CourseEvals.Api.Services.Interfaces;
using UChi.CourseEvals.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseNpgsql(connectionString));

builder.Services.AddControllers()
    .AddJsonOptions(o =>
    { 
        o.JsonSerializerOptions
            .ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add application services
builder.Services.AddScoped<ICoursesService, CoursesService>();
builder.Services.AddScoped<ISectionsService, SectionsService>();
builder.Services.AddScoped<IInstructorService, InstructorService>();

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