using Microsoft.AspNetCore.Authentication.JwtBearer;
using Products_Crud.ExceptionMiddleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Products_Crud.Interfaces;
using Products_Crud.Services;
using Products_Crud.Filters;
using Products_Crud.Model;
using Products_Crud.BAL;
using Products_Crud.DAL;
using Products_Crud.BL;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
// var env = builder.Environment.EnvironmentName;
// Console.WriteLine($"Environment: {env}");
// Configure JSON serialization to ignore circular references
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });
    
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("Sql_Connection_String")
    )
);
builder.Host.UseDefaultServiceProvider(option =>
{
    option.ValidateScopes = true; // catch scope mismatches
    option.ValidateOnBuild = true; // catch missing registrations
});
/*
the above three lines code explanation
// If I forgot to register ISchemeService and a controller needs it:
// App throws at startup:
// "Unable to resolve service for type 'ISchemeService'
//  while attempting to activate 'BillingController'"
*/


builder.Services.AddScoped<IEmployeeRepository, AddEmployeeRepository>();
builder.Services.AddScoped<IEmployeeUpdateRepository, UpdateEmployeeRepository>();
builder.Services.AddScoped<IEmployeeUpdateService, UpdatEmployeeServices>();
builder.Services.AddScoped<IEmployeeDeleteRepository, EmployeeDelete>();
builder.Services.AddScoped<ICustomerCreate, CustomerRepository>();
builder.Services.AddScoped<ICustomerRead, CustomerRepository>();
builder.Services.AddScoped<IInvoiceCreate, InvoiceRepository>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IInvoiceRead, InvoiceRepository>();
builder.Services.AddScoped<ApiKeyAuthorizationFilter>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<DashboardService>();
builder.Services.AddScoped<PasswordService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<JwtService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder
            .SetIsOriginAllowed(origin => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
    );
});
var jwtSettings = builder.Configuration.GetSection("Jwt");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings["Key"]!)
            )
        };
    });
var app = builder.Build();
app.UseCors("AllowAllOrigins");
app.UseDeveloperExceptionPage();
app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ExceptionMiddleware>();
app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();