using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MONKEY5.DataAccessObjects;
using MONKEY5.BusinessObjects;


var builder = WebApplication.CreateBuilder(args);

// ✅ Register DbContext with SQL Server using Dependency Injection
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyDB"))
);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
