using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Week9_Day2_ContactManagementAPI.Models;
using Week9_Day2_ContactManagementAPI.Repositories;
using Week9_Day2_ContactManagementAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMemoryCache();

builder.Services.AddDbContext<ContactDbContext>(options =>
    options.UseInMemoryDatabase("ContactDb"));

builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<IContactService, ContactService>();

builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    options.OnRejected = async (context, token) =>
    {
        context.HttpContext.Response.StatusCode = 429;
        await context.HttpContext.Response.WriteAsync("Too many requests. Please try again later.", token);
    };

    options.AddPolicy("fixed", httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 5,
                Window = TimeSpan.FromSeconds(60),
                QueueLimit = 0,
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst
            }));
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ContactDbContext>();
    if (!db.Contacts.Any())
    {
        db.Contacts.AddRange(
            new Contact { ContactId = 1, Name = "John", Email = "john@test.com", Phone = "9999999999" },
            new Contact { ContactId = 2, Name = "Sara", Email = "sara@test.com", Phone = "8888888888" },
            new Contact { ContactId = 3, Name = "David", Email = "david@test.com", Phone = "7777777777" },
            new Contact { ContactId = 4, Name = "Anita", Email = "anita@test.com", Phone = "6666666666" },
            new Contact { ContactId = 5, Name = "Rahul", Email = "rahul@test.com", Phone = "5555555555" },
            new Contact { ContactId = 6, Name = "Aisha", Email = "aisha@test.com", Phone = "4444444444" },
            new Contact { ContactId = 7, Name = "Karan", Email = "karan@test.com", Phone = "3333333333" },
            new Contact { ContactId = 8, Name = "Neha", Email = "neha@test.com", Phone = "2222222222" },
            new Contact { ContactId = 9, Name = "Manoj", Email = "manoj@test.com", Phone = "1111111111" },
            new Contact { ContactId = 10, Name = "Priya", Email = "priya@test.com", Phone = "1234567890" }
        );
        db.SaveChanges();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRateLimiter();

app.MapControllers();

app.Run();
