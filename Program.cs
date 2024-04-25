using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models.Entities;
using Microsoft.OpenApi.Models;
using personapi_dotnet.Repositories;
using personapi_dotnet.Controllers.Interfaces;
using personapi_dotnet.Controllers.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    options.JsonSerializerOptions.PropertyNamingPolicy = null; // Asegúrate de que las propiedades del JSON coincidan con las de tus clases
});


builder.Services.AddRazorPages(); // Solo si todavía quieres usar Razor Pages para algo más

// HttpClient Configuration
builder.Services.AddHttpClient("API", client =>
{
    var baseUrl = builder.Configuration.GetValue<string>("ApiSettings:BaseUrl");
    client.BaseAddress = new Uri(baseUrl);
});


// Entity Framework Core configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<PersonaDbContext>(options =>
    options.UseSqlServer(connectionString));

// Repositories registration
builder.Services.AddScoped<IPersonaRepository, PersonaRepository>();
builder.Services.AddScoped<ITelefonoRepository, TelefonoRepository>();
builder.Services.AddScoped<IEstudioRepository, EstudioRepository>();
builder.Services.AddScoped<IProfesionRepository, ProfesionRepository>();

// Swagger configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Person API", Version = "v1" });

    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Configure Swagger only if in Development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    // Set the Swagger UI to a specific path
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Person API V1");
        c.RoutePrefix = "swagger"; // Now Swagger is available at http://localhost:<port>/swagger
    });
}

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"); // This sets Home/Index as the default route
    endpoints.MapRazorPages();
});

app.Run();

