using DiabetesOnContainer.Configuration;
using DiabetesOnContainer.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//jsonpatch package
builder.Services.AddControllers()
                   .AddNewtonsoftJson();


builder.Services.AddDbContext<DiabetesOnContainersContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("dbconn"));
});

//mapping config survices
builder.Services.AddAutoMapper(typeof(MapperConfig));

//add CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", b => b.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(
       options =>
    {
        options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = "DiabetesOnContainer",
            Version = "v1",
            Description = "api first version for creating database of patient records"
        });
        var xmlfile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlpath = Path.Combine(AppContext.BaseDirectory, xmlfile);

        options.IncludeXmlComments(xmlpath);
    })  
            .AddSwaggerGenNewtonsoftSupport();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/api/Error");
}

app.UseHttpsRedirection();

//ADD midlleware of cors
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
