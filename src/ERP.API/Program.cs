using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using Products_Crud.DAL;
using Products_Crud.ExceptionMiddleware;

using ERP.Modules.Shared.Application;
using ERP.Modules.Shared.Contracts;
using ERP.Modules.Shared.Domain;
using ERP.Modules.Shared.Filters;
using ERP.Modules.Shared.Infrastructure;

using ERP.Modules.Retail.Application;
using ERP.Modules.Retail.Contracts;
using ERP.Modules.Retail.Controllers;
using ERP.Modules.Retail.Domain;
using ERP.Modules.Retail.DTOs;
using ERP.Modules.Retail.Enums;
using ERP.Modules.Retail.Infrastructure;

using Erp.Bl.CurrentTenantImplementation;
using Erp.Bl.TenantSaveChanges;
using Erp.Filters.ResponseAPI;

var builder = WebApplication.CreateBuilder(args);


// ============================================================
// Controllers
// ============================================================

builder.Services
    .AddControllers(options =>
    {
        options.Filters.Add<ApiResponseFilter>();
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler =
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    // Prevents SwaggerGeneratorException when the same route/method
    // combination is discovered more than once (e.g. a controller
    // assembly loaded/registered twice). This picks the first
    // matching action instead of throwing.
    options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});


// ============================================================
// Database
// ============================================================

builder.Services.AddDbContext<UserDbContext>((serviceProvider, options) =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("Sql_Connection_String")
    );

    options.AddInterceptors(
        serviceProvider.GetRequiredService<TenantSaveChangesInterceptor>()
    );
});


// ============================================================
// Dependency Injection Validation
// ============================================================

builder.Host.UseDefaultServiceProvider(options =>
{
    options.ValidateScopes = true;
    options.ValidateOnBuild = true;
});


// ============================================================
// Employee
// ============================================================

builder.Services.AddScoped<IEmployeeRepository, AddEmployeeRepository>();
builder.Services.AddScoped<IEmployeeUpdateRepository, UpdateEmployeeRepository>();
builder.Services.AddScoped<IEmployeeUpdateService, UpdateEmployeeService>();
builder.Services.AddScoped<IEmployeeDeleteRepository, EmployeeDelete>();


// ============================================================
// Customer
// ============================================================

builder.Services.AddScoped<ICustomerCreate, CustomerRepository>();
builder.Services.AddScoped<ICustomerRead, CustomerRepository>();
builder.Services.AddScoped<IRetailCustomerService, RetailCustomerService>();


// ============================================================
// Invoice
// ============================================================

builder.Services.AddScoped<IInvoiceCreate, InvoiceRepository>();
builder.Services.AddScoped<IInvoiceRead, InvoiceRepository>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();


// ============================================================
// Product
// ============================================================

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ProductService>();


// ============================================================
// User / Authentication
// ============================================================

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<PasswordService>();
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<IUsersListRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();


// ============================================================
// Supplier
// ============================================================

builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<ISupplierService, SupplierService>();


// ============================================================
// Purchase Invoice
// ============================================================

builder.Services.AddScoped<IPurchaseInvoiceRepository, PurchaseInvoiceRepository>();
builder.Services.AddScoped<IPurchaseInvoiceService, PurchaseInvoiceService>();


// ============================================================
// Purchase Cards
// ============================================================

builder.Services.AddScoped<IPurchaseCardsRepository, PurchaseCardsRepository>();
builder.Services.AddScoped<IPurchaseCardService, PurchaseCardService>();


// ============================================================
// Dashboard
// ============================================================

builder.Services.AddScoped<DashboardService>();


// ============================================================
// Filters
// ============================================================

builder.Services.AddScoped<ApiKeyAuthorizationFilter>();


// ============================================================
// HTTP Context / Tenant
// ============================================================

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<TenantSaveChangesInterceptor>();

builder.Services.AddScoped<ICurrentTenantService, CurrentTenantService>();


// ============================================================
// Shared DbContext
// ============================================================

builder.Services.AddScoped<ISharedDbContext>(
    serviceProvider => serviceProvider.GetRequiredService<UserDbContext>()
);

builder.Services.AddScoped<IRetailDbContext>(
    serviceProvider => serviceProvider.GetRequiredService<UserDbContext>()
);


// ============================================================
// CORS
// ============================================================

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy
            .SetIsOriginAllowed(origin => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});


// ============================================================
// JWT Authentication
// ============================================================
Console.WriteLine("====================================");
Console.WriteLine($"Current Directory: {Directory.GetCurrentDirectory()}");
Console.WriteLine($"JWT Key: {builder.Configuration["Jwt:Key"]}");
Console.WriteLine($"JWT Issuer: {builder.Configuration["Jwt:Issuer"]}");
Console.WriteLine("====================================");

var jwtSettings = builder.Configuration.GetSection("Jwt");

var jwtKey = jwtSettings["Key"];
var jwtIssuer = jwtSettings["Issuer"];
var jwtAudience = jwtSettings["Audience"];

if (string.IsNullOrWhiteSpace(jwtKey))
{
    throw new InvalidOperationException(
        "JWT Key is missing from appsettings.json."
    );
}

if (string.IsNullOrWhiteSpace(jwtIssuer))
{
    throw new InvalidOperationException(
        "JWT Issuer is missing from appsettings.json."
    );
}

if (string.IsNullOrWhiteSpace(jwtAudience))
{
    throw new InvalidOperationException(
        "JWT Audience is missing from appsettings.json."
    );
}

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey)
            ),

            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();


// ============================================================
// Build Application
// ============================================================

var app = builder.Build();


// ============================================================
// Middleware Pipeline
// ============================================================

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();

app.UseSwaggerUI();

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AllowAllOrigins");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();