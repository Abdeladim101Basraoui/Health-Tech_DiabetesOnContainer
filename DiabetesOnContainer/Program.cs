using DiabetesOnContainer.Configuration;
using DiabetesOnContainer.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

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

        options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
            Description = "Standard Authorisation using the Bearer Scheme (\"{bearer {token}})",
            In = ParameterLocation.Header,
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey
        });

        options.OperationFilter<SecurityRequirementsOperationFilter>();
    })
            .AddSwaggerGenNewtonsoftSupport();


builder.Services.AddAuthentication(
    JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes
            (builder.Configuration.GetSection("AppSettings:Token").Value))
            ,
                ValidateIssuer = false,
                ValidateAudience = false

            };

        });

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
