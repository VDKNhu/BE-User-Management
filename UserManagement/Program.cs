using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using UserManagement.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod()
     .AllowAnyHeader());
});

// Add services to the container.
builder.Services.AddControllers();

IMvcBuilder mvcBuilder = builder.Services.AddMvc(option => option.EnableEndpointRouting = false)
    .SetCompatibilityVersion(version: Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0)
    .AddNewtonsoftJson(
        opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore); 

builder.Services.AddDbContext<UserManagementContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:UserManagementDB"]);
});

// Config camelCase naming

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    gen =>
    {
        gen.SwaggerDoc("v1.0", new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = "User management API",
            Version = "v1.0.0",
        });
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(ui =>
    {
        ui.SwaggerEndpoint("/swagger/v1.0/swagger.json", "User management API");
    });
}

app.UseCors(
    builder =>
    {
        builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    }
);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
