    using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using BusinessLayer.Services.CustomerServices;
using BusinessLayer.Services.UserServices;
using BusinessLayer.Services.CustomerOperationServices;
using BusinessLayer.Mapping;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BusinessLayer.Services.TaskAssignmentServices;
using BusinessLayer.Services.TaskAssignmentFileServices;
using BusinessLayer.Services.CustomerOperationsFileServices;
using Microsoft.Extensions.FileProviders;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>();

builder.Services.AddScoped<ICustomerRepository, EfCustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerOperationRepository, EfCustomerOperationRepository>();
builder.Services.AddScoped<ICustomerOperationService, CustomerOperationService>();
builder.Services.AddScoped<IUserRepository, EfUserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITaskAssignmentRepository, EfTaskAssignmentRepository>();
builder.Services.AddScoped<ITaskAssignmentService, TaskAssignmentService>();
builder.Services.AddScoped<ITaskAssignmentFileRepository, EfTaskAssignmentFileRepository>();
builder.Services.AddScoped<ITaskAssignmentFileService, TaskAssignmentFileService>();
builder.Services.AddScoped<ICustomerOperationFileRepository, EfCustomerOperationFileRepository>();
builder.Services.AddScoped<ICustomerOperationFileService, CustomerOperationFileService>();

builder.Services.AddAutoMapper(typeof(MappingProfile));
var jwtSettings = builder.Configuration.GetSection("JWT");

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("https://ecosistem.runasp.net", "https://localhost:7269")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
            ValidAudience = builder.Configuration["JWT:ValidAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
        };
    });
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads")),
    RequestPath = "/uploads"
});

app.UseCors("AllowSpecificOrigin"); // CORS'u öne alýn
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
