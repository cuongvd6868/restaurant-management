using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RestaurantManagement.DAOs.Impl;
using RestaurantManagement.DAOs;
using RestaurantManagement.Models;
using RestaurantManagement.Repositories;
using RestaurantManagement.Repositories.Impl;
using RestaurantManagement.Services;
using RestaurantManagement.Services.Impl;
using System.Text;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddIdentity<Customer, Role>()
    .AddEntityFrameworkStores<FoodDbContext>()
    .AddDefaultTokenProviders();

// Cấu hình DbContext trước khi build app
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<FoodDbContext>(options =>
    options.UseSqlServer(connectionString));

var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = jwtSettings["Key"] ?? throw new InvalidOperationException("JWT Key is missing in configuration");

var keyBytes = Encoding.UTF8.GetBytes(key);

// Cấu hình Authentication với JWT Bearer
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ClockSkew = TimeSpan.Zero
    };

    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            // Nếu token đã có trong Header thì không cần lấy từ Cookies
            if (string.IsNullOrEmpty(context.Token))
            {
                if (context.Request.Cookies.ContainsKey("accessToken"))
                {
                    context.Token = context.Request.Cookies["accessToken"];
                }
            }
            return Task.CompletedTask;
        }
    };
});



// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IPasswordHasher<Customer>, PasswordHasher<Customer>>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IAuthService, AuthService>();


builder.Services.AddScoped(typeof(IGenericDAO<>), typeof(GenericDAO<>)); // Đăng ký Generic DAO
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>)); // Đăng ký Generic Repository

builder.Services.AddScoped<IFoodOrderDAO, FoodOrderDAO>();
builder.Services.AddScoped<IFoodOderRepository, FoodOderRepository>();
builder.Services.AddScoped<IFoodCategoryDAO, FoodCategoryDAO>();
builder.Services.AddScoped<IFoodCategoryRepository, FoodCategoryRepository>();


builder.Services.AddScoped<IFoodDAO, FoodDAO>();
builder.Services.AddScoped<ICartItemDAO, CartItemDAO>();
builder.Services.AddScoped<IFoodFavoriteDAO, FoodFavoriteDAO>();
builder.Services.AddScoped<IFoodFeedbackDAO, FoodFeedbackDAO>();
builder.Services.AddScoped<IFoodImageDAO, FoodImageDAO>();



builder.Services.AddScoped<IFoodRepository, FoodRepository>();
builder.Services.AddScoped<ICartItemRepository, CartItemRepository>();
builder.Services.AddScoped<IFoodFavoriteRepository, FoodFavoriteRepository>();
builder.Services.AddScoped<IFoodFeedbackRepository, FoodFeedbackRepository>();
builder.Services.AddScoped<IFoodImageRepository, FoodImageRepository>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Đảm bảo tệp tĩnh có thể được phục vụ
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
