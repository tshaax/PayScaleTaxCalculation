using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using PayScale.DataAccess.Repository.IRepository;
using PayScale.DataAccess.Repository;
using PayScale.DataAcess.Data;
using PayScale.DataAccess.DbInitializer;
using PayScale.API.Extensions.IApiKeyValidations;
using PayScale.API.Extensions;
using PayScale.API.Extensions.CustomExceptionMiddleware;
using Microsoft.OpenApi.Models;
using System.Reflection;
using PayScale.Models.Constants;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    const string securityDefinition = APIKeyConstants.ApiKeyName;
    options.AddSecurityDefinition(securityDefinition, new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = APIKeyConstants.ApiKeyHeaderName,
        Type = SecuritySchemeType.ApiKey
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Name = APIKeyConstants.ApiKeyHeaderName,
                Type = SecuritySchemeType.ApiKey,
                In = ParameterLocation.Header,
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = securityDefinition }
            },
            new List<string>()
        }
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));


});

builder.Services.AddApiVersioning(opt =>
{
    opt.DefaultApiVersion = new ApiVersion(1, 0);
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.ReportApiVersions = true;
    opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                    new HeaderApiVersionReader("x-api-version"),
                                                    new MediaTypeApiVersionReader("x-api-version"));
}).AddApiExplorer(o => {
    o.GroupNameFormat = "'v'VVV";
    o.SubstituteApiVersionInUrl = true;
 });
builder.Services.AddAuthorization();
builder.Services.AddAuthentication();

builder.Services.AddTransient<IApiKeyValidation, ApiKeyValidation>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
   options.UseInMemoryDatabase("PayScaleDb"));

builder.Services.AddMemoryCache();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();


var app = builder.Build();

using (var scopes = app.Services.CreateScope())
{
    var services = scopes.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();

    DataGenerator.Initialize(services);
}
app.UseExceptionHandler();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();
app.UseMiddleware<ApiKeyMiddleware>();
app.Run();
