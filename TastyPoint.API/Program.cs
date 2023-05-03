using Microsoft.EntityFrameworkCore;

using Microsoft.OpenApi.Models;

using TastyPoint.API.Publishing.Domain.Repositories;
using TastyPoint.API.Publishing.Domain.Services;
using TastyPoint.API.Publishing.Persistence.Repositories;
using TastyPoint.API.Publishing.Services;

using TastyPoint.API.Selling.Domain.Repositories;
using TastyPoint.API.Selling.Domain.Services;
using TastyPoint.API.Selling.Persistence.Repositories;
using TastyPoint.API.Selling.Services;

using TastyPoint.API.Ordering.Domain.Repositories;
using TastyPoint.API.Ordering.Domain.Services;
using TastyPoint.API.Ordering.Persistence.Repositories;
using TastyPoint.API.Ordering.Services;
using TastyPoint.API.Profiles.Domain.Repositories;
using TastyPoint.API.Profiles.Domain.Services;
using TastyPoint.API.Profiles.Persistence.Repositories;
using TastyPoint.API.Profiles.Services;
using TastyPoint.API.Security.Authorization.Handlers.Implementations;
using TastyPoint.API.Security.Authorization.Handlers.Interfaces;
using TastyPoint.API.Security.Authorization.Middleware;
using TastyPoint.API.Security.Authorization.Settings;
using TastyPoint.API.Security.Domain.Repositories;
using TastyPoint.API.Security.Domain.Services;
using TastyPoint.API.Security.Persistence;
using TastyPoint.API.Security.Services;
using TastyPoint.API.Shared.Domain.Repositories;
using TastyPoint.API.Shared.Persistence.Contexts;
using TastyPoint.API.Shared.Persistence.Repositories;
using TastyPoint.API.Subscription.Domain.Repositories;
using TastyPoint.API.Subscription.Domain.Services;
using TastyPoint.API.Subscription.Persistence.Repositories;
using TastyPoint.API.Subscription.Services;
using TastyPoint.API.Social.Domain.Repositories;
using TastyPoint.API.Social.Domain.Services;
using TastyPoint.API.Social.Persistence.Repositories;
using TastyPoint.API.Social.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Add Database Connection

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseMySQL(connectionString)
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors());

//Add Lowercase Routes
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Dependency Injection Configuration

// Shared Bounded Context Dependency Injection Configuration

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//Selling Bounded Context Dependency Injection Configuration

builder.Services.AddScoped<IPackRepository, PackRepository>();
builder.Services.AddScoped<IPackService, PackService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

//Subscription Bounded Context Dependency Injection Configuration

builder.Services.AddScoped<IBusinessPlanRepository, BusinessPlanRepository>();
builder.Services.AddScoped<IBusinessPlanService, BusinessPlanService>();

//Publishing Bounded Context Dependency Injection Configuration

builder.Services.AddScoped<IPromotionService, PromotionService>();
builder.Services.AddScoped<IPromotionRepository, PromotionRepository>();

//Ordering Bounded Context Dependency Injection Configuration

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();

//Security Bounded Context Dependency Injection Configuration
builder.Services.AddScoped<IJwtHandler, JwtHandler>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

//User Profile Bounded Context Dependency Injection Configuration
builder.Services.AddScoped<IUserProfileRepository, UserProfileRepository>();
builder.Services.AddScoped<IUserProfileService, UserProfileService>();

//Social Bounded Context Dependency Injection Configuration
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IFoodStoreRepository, FoodStoreRepository>();
builder.Services.AddScoped<IFoodStoreService, FoodStoreService>();

//AutoMapper Configuration

builder.Services.AddAutoMapper(
    typeof(TastyPoint.API.Selling.Mapping.ModelToResourceProfile),
    typeof(TastyPoint.API.Selling.Mapping.ResourceToModelProfile),
    typeof(TastyPoint.API.Subscription.Mapping.ModelToResourceProfile),
    typeof(TastyPoint.API.Subscription.Mapping.ResourceToModelProfile),
    typeof(TastyPoint.API.Publishing.Mapping.ModelToResourceProfile),
    typeof(TastyPoint.API.Publishing.Mapping.ResourceToModelProfile),
    typeof(TastyPoint.API.Ordering.Mapping.ModelToResourceProfile),
    typeof(TastyPoint.API.Ordering.Mapping.ResourceToModelProfile),
    typeof(TastyPoint.API.Security.Mapping.ModelToResourceProfile),
    typeof(TastyPoint.API.Security.Mapping.ResourceToModelProfile),
    typeof(TastyPoint.API.Profiles.Mapping.ModelToResourceProfile),
    typeof(TastyPoint.API.Profiles.Mapping.ResourceToModelProfile),
    typeof(TastyPoint.API.Social.Mapping.ModelToResourceProfile),
    typeof(TastyPoint.API.Social.Mapping.ResourceToModelProfile));

builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Tasty Point API",
            Description = "Tasty Point Restfull API",
            TermsOfService = new Uri("http://TastyPoint/TermsOfService"),
            Contact = new OpenApiContact
            {
                Name = "Tasty Point Studio",
                Url = new Uri("https://tastypoint-appweb.web.app/")
            },
            License = new OpenApiLicense
            {
                Name = "Tasty Point Resources Licenses",
                Url = new Uri("http://TastyPoint/Licenses")
            }
        });
        options.EnableAnnotations(); 
    }
);

//AppSettings Configuration
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

var app = builder.Build();

//Validation for ensuring Database Objects are created

using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<AppDbContext>())
{
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("v1/swagger.json", "v1");
        options.RoutePrefix = "swagger";
    });
}

//Configure CORS
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseMiddleware<JwtMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program{}