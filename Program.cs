using Products_Crud.BL;
using Products_Crud.DAL;
using Products_Crud.ExceptionMiddleware;
using Products_Crud.Filters;
using Products_Crud.Model;

var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment.EnvironmentName;
Console.WriteLine($"Environment: {env}");
// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddScoped<EmpRepository>();
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
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder
            .SetIsOriginAllowed(origin => true) // Allow all origins
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
    );
});
var app = builder.Build();
// Enable CORS before routing
app.UseCors("AllowAllOrigins");
string conn = builder.Configuration.GetConnectionString("Sql_Connection_String");
Console.WriteLine($"Connection: {conn}");
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionMiddleware>();
app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapGet("/", () => $"Running in {env}");
app.Run();