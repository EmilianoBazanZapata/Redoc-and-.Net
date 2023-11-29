using ApiRedoc;
using ApiRedoc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using ApiRedoc.Middleware;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("swagger", new OpenApiInfo { Title = "Swagger API", Version = "v1" });
    options.SwaggerDoc("redoc", new OpenApiInfo { Title = "Redoc API", Version = "v1" });

    // Filtrar controladores por grupo para Swagger
    options.DocInclusionPredicate((docName, apiDesc) =>
    {
        if (!apiDesc.TryGetMethodInfo(out var methodInfo)) return false;

        var groupAttribute = methodInfo.DeclaringType.GetCustomAttributes(true)
            .OfType<ApiExplorerSettingsAttribute>()
            .FirstOrDefault();

        return groupAttribute?.GroupName == docName;
    });
    
    options.SchemaFilter<DescriptionSchemaFilter>(); 
    
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
    options.EnableAnnotations();
});

builder.Services.AddSwaggerDocument(config =>
{
    config.PostProcess = document =>
    {
        document.Info.Title = "Todo API";
        document.Info.Description = "A simple CRUD API for managing tasks.";
    };
});

var app = builder.Build();
app.UseStaticFiles();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/swagger/swagger.json", "Swagger API");
    c.InjectJavascript("/button-doc.js");
    c.InjectStylesheet("/SwaggerDark.css");
});

app.UseOpenApi();
app.UseSwagger();
//Activate ReDoc
app.UseReDoc(options =>
{
    options.DocumentTitle = "ReDoc Documentation";
    options.SpecUrl = "/swagger/redoc/swagger.json";
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
