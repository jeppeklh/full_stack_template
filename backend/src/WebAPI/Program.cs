using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Domain.Interfaces.Repositories;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Diagnostics;
using Domain.Interfaces.Services;
using Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<VagtplanDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<User, IdentityRole<Guid>>(opts => opts.User.RequireUniqueEmail = true)
        .AddEntityFrameworkStores<VagtplanDbContext>()
        .AddDefaultTokenProviders();

builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserEmploymentRepository, UserEmploymentRepository>();

// adds traceId to http responses for correlation in logs
builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = context =>
    {
        context.ProblemDetails.Extensions["traceId"] = context.HttpContext.TraceIdentifier;
    };
});

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<VagtplanDbContext>();
    try
    {
        context.Database.Migrate();
        context.SeedData();
    }
    catch
    {
        //get fuckt
    }
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Maps exceptions to ProblemDetails JSON with relevant status codes
app.UseExceptionHandler(appError =>
{
    appError.Run(async context =>
    {
        var feature = context.Features.Get<IExceptionHandlerFeature>();
        var exception = feature?.Error;
        var status = exception switch
        {
            KeyNotFoundException => StatusCodes.Status404NotFound,
            ArgumentException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };
        context.Response.StatusCode = status;
        await Results.Problem(
            title: exception?.GetType().Name,
            detail: exception?.Message,
            statusCode: status
        ).ExecuteAsync(context);
    });
});

app.UseHttpsRedirection();


app.MapControllers();
app.Run();
