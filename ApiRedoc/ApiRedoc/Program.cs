using ApiRedoc;
using ApiRedoc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using ApiRedoc.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    c.DocInclusionPredicate((docName, apiDesc) =>
    {
        if (apiDesc.TryGetMethodInfo(out var methodInfo))
        {
            return methodInfo.DeclaringType == typeof(TodoController);
        }
        return false;
    });
});
//Select a specific controller for ReDoc
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("doc", new OpenApiInfo { Title = "My Redoc API", Version = "doc" });

    c.DocInclusionPredicate((docName, apiDesc) =>
    {
        if (apiDesc.TryGetMethodInfo(out var methodInfo))
        {
            return methodInfo.DeclaringType == typeof(TodoRedocController);
        }
        return false;
    });
    
    c.SchemaFilter<DescriptionSchemaFilter>(); 
    
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
    c.EnableAnnotations();

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
    c.InjectJavascript("/button-doc.js");
    c.InjectStylesheet("/SwaggerDark.css");
});

app.UseOpenApi();
app.UseSwagger();
//Activate ReDoc
app.UseReDoc(options =>
{
    options.DocumentTitle = "ReDoc Documentation";
    options.SpecUrl = "/swagger/doc/swagger.json";
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
