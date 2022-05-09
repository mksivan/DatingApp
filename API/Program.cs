using API.Data;
using Microsoft.EntityFrameworkCore;

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
// builder.Services.AddCors(options => {
//     options.AddDefaultPolicy(policy => {
//         policy.WithOrigins("http://localhost:4200");
//     });
// });
// builder.Services.AddCors(options => {
//     options.AddPolicy("capp", builder => builder.WithOrigins("*"));    
// });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});



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

app.UseAuthorization();

app.MapControllers();

app.Run();
