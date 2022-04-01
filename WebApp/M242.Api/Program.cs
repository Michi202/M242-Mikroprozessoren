using M242.Api.Filters;
using M242.Model.Util;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddNHibernate(builder.Configuration);
builder.Services.AddControllers();

builder.Services.AddRouting();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            );
});
builder.Services.AddMvc(options =>
{
    options.Filters.Add(new TransactionAttribute());
    options.EnableEndpointRouting = false;
}).AddJsonOptions(o =>
{
    // o.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (bool.Parse(builder.Configuration["Development"]))
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();


app.UseHttpsRedirection();

app.UseMvc(routes =>
{
    routes.MapRoute(
        "DefaultRoutes",
        "api/{controller=Home}/{action=Index}/{id?}");
});

app.Run();
