using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Products_Crud.ExceptionMiddleware;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Products_Crud.Interfaces;
using Products_Crud.Services;
using Products_Crud.Filters;
using Products_Crud.Model;
using Products_Crud.BAL;
using Products_Crud.DAL;
using Products_Crud.BL;

var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment.EnvironmentName;
Console.WriteLine($"Environment: {env}");
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Sql_Connection_String")));

builder.Services.AddDbContext<AppDbContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("Sql_Connection_String")));

builder.Services.AddScoped<IEmployeeRepository, AddEmployeeRepository>();
builder.Services.AddScoped<IEmployeeUpdateRepository, UpdateEmployeeRepository>();
builder.Services.AddScoped<IEmployeeUpdateService, UpdatEmployeeServices>();
builder.Services.AddScoped<IEmployeeDeleteRepository, EmployeeDelete>();
builder.Services.AddScoped<ICustomerCreate, SqlInvoiceRepository>();
builder.Services.AddScoped<ICustomerRead, SqlInvoiceRepository>();
builder.Services.AddScoped<IInvoiceCreate, SqlInvoiceRepository>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IInvoiceRead, SqlInvoiceRepository>();
builder.Services.AddScoped<ApiKeyAuthorizationFilter>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();
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
// if (app.Environment.IsDevelopment())
// {
//     app.UseDeveloperExceptionPage();
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }
app.UseDeveloperExceptionPage();
app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ExceptionMiddleware>();
app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapGet("/", () => $"Running in {env}");
app.Run();