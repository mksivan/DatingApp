using System.Text;
using API.Data;
using API.Extensions;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var _myAllowOrigins = "capp";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(x => {
    x.AddPolicy(_myAllowOrigins, 
    p => {
        p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        //p.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServices(builder.Configuration);
//builder.Services.AddScoped<ITokenService, TokenService>();
// builder.Services.AddDbContext<DataContext>(options => {
//     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
// });



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseCors("capp");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
//app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"));
app.UseCors(_myAllowOrigins);
//app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
