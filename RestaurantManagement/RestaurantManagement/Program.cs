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
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình Identity
builder.Services.AddIdentity<Customer, Role>()
    .AddEntityFrameworkStores<FoodDbContext>()
    .AddDefaultTokenProviders();

// Cấu hình HTTP Client
builder.Services.AddHttpClient<FoodService>();
builder.Services.AddControllersWithViews();

// Cấu hình DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<FoodDbContext>(options =>
    options.UseSqlServer(connectionString));

var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = jwtSettings["Key"] ?? throw new InvalidOperationException("JWT Key is missing in configuration");
var keyBytes = Encoding.UTF8.GetBytes(key);

// Cấu hình Authentication (JWT + Cookie) chỉ gọi 1 lần
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
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
            if (string.IsNullOrEmpty(context.Token) && context.Request.Cookies.ContainsKey("accessToken"))
            {
                context.Token = context.Request.Cookies["accessToken"];
            }
            return Task.CompletedTask;
        }
    };
})
.AddCookie(options =>
{
    options.LoginPath = "/Auth/Login";
    options.LogoutPath = "/Auth/Logout";
});

// Cấu hình Dependency Injection
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IPasswordHasher<Customer>, PasswordHasher<Customer>>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IFoodService, FoodService>();

builder.Services.AddScoped(typeof(IGenericDAO<>), typeof(GenericDAO<>));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddScoped<IFoodDAO, FoodDAO>();
builder.Services.AddScoped<ICartItemDAO, CartItemDAO>();
builder.Services.AddScoped<IFoodFavoriteDAO, FoodFavoriteDAO>();
builder.Services.AddScoped<IFoodFeedbackDAO, FoodFeedbackDAO>();
builder.Services.AddScoped<IFoodImageDAO, FoodImageDAO>();
builder.Services.AddScoped<IPaymentMethodDAO, PaymentMethodDAO>();
builder.Services.AddScoped<IFoodCategoryDAO, FoodCategoryDAO>();
builder.Services.AddScoped<IFoodOrderDAO, FoodOrderDAO>();
builder.Services.AddScoped<IFoodOrderDetailDAO, FoodOrderDetailDAO>();

builder.Services.AddScoped<IFoodRepository, FoodRepository>();
builder.Services.AddScoped<ICartItemRepository, CartItemRepository>();
builder.Services.AddScoped<IFoodFavoriteRepository, FoodFavoriteRepository>();
builder.Services.AddScoped<IFoodFeedbackRepository, FoodFeedbackRepository>();
builder.Services.AddScoped<IFoodImageRepository, FoodImageRepository>();
builder.Services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
builder.Services.AddScoped<IFoodCategoryRepository, FoodCategoryRepository>();
builder.Services.AddScoped<IFoodOderRepository, FoodOderRepository>();
builder.Services.AddScoped<IFoodOderDetailRepository, FoodOderDetailRepository>();

var app = builder.Build();

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();