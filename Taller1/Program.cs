using System.Security.Claims;
using System.Text;

using API.Middleware;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using Serilog;

using Taller1.Src.Data;
using Taller1.Src.Helpers;
using Taller1.Src.Interfaces;
using Taller1.Src.Models;
using Taller1.Src.Repositories;
using Taller1.Src.Services;
Log.Logger = new LoggerConfiguration().CreateLogger();

try
{
    Log.Information("starting server.");

    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
    });

    builder.Services.AddTransient<ExceptionMiddleware>();
    /**
     builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
     builder.Services.AddScoped<IOrderService, OrderService>();
     builder.Services.AddScoped<IUserService, UserService>();
    **/
    builder.Services.AddScoped<IProductRepository, ProductRepository>();
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
    builder.Services.AddScoped<IOrderRepository, OrderRepository>();
    builder.Services.AddScoped<IShippingAddressRepository, ShippingAddressRepository>();
    builder.Services.AddScoped<ITokenServices, TokenService>();
    builder.Services.AddScoped<IPhotoService, PhotoService>();
    builder.Services.AddScoped<UnitOfWork>();
    builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
    {
        options.SerializerOptions.Converters.Add(new DateOnlyJsonConverter());
    });
    builder
        .Services.AddIdentity<User, IdentityRole>(opt =>
        {
            opt.User.RequireUniqueEmail = true;
            opt.Password.RequireNonAlphanumeric = false;
            opt.Password.RequiredLength = 6;
            opt.SignIn.RequireConfirmedEmail = false;
        })
        .AddEntityFrameworkStores<StoreContext>();
    builder
        .Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(opt =>
        {
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidateAudience = true,
                ValidAudience = builder.Configuration["Jwt:Audience"],
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SignInKey"]!)
                ),
                RoleClaimType = ClaimTypes.Role,
            };
        });
    builder.Services.AddDbContext<StoreContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
    );
    builder.Host.UseSerilog(
        (context, services, configuration) =>
        {
            configuration
                .ReadFrom.Configuration(context.Configuration)
                .Enrich.FromLogContext()
                .Enrich.WithThreadId()
                .Enrich.WithMachineName();
        }
    );
    var corsSettings = builder.Configuration.GetSection("CorsSettings");
    var allowedOrigins = corsSettings.GetSection("AllowedOrigins").Get<string[]>();
    var allowedHeaders = corsSettings.GetSection("AllowedHeaders").Get<string[]>();
    var allowedMethods = corsSettings.GetSection("AllowedMethods").Get<string[]>();

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("DefaultCorsPolicy", policy =>
        {
            policy.WithOrigins(allowedOrigins!)
                  .WithHeaders(allowedHeaders!)
                  .WithMethods(allowedMethods!)
                  .AllowCredentials();
        });
    });

    var isRunningEfTool = AppDomain.CurrentDomain.FriendlyName == "ef";

    var app = builder.Build();
    app.UseMiddleware<ExceptionMiddleware>();
    if (!isRunningEfTool)
    {
        await DbInitializer.InitDb(app);
    }

    app.UseCors("DefaultCorsPolicy");
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "server terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}