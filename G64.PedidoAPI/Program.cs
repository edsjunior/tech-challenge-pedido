using G64.PedidoAPI.Context;
using G64.PedidoAPI.Repositories;
using G64.PedidoAPI.Services;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.Security.Authentication;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
//var mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(mySqlConnection,
											//ServerVersion.AutoDetect(mySqlConnection)));

// Access configuration
var host = builder.Configuration["DBHOST"] ?? "localhost";
var port = builder.Configuration["DBPORT"] ?? "3306";
var password = builder.Configuration["DBPASSWORD"] ?? "g64soat";

// Create the connection string
var connectionString = $"Server={host};Port={port};Database=g64soatpedido;User=root;Password={password};SslMode=None;";

// Use the connection string in the services configuration
//builder.Services.AddDbContext<AppDbContext>(options =>
//	options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));




builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<PedidoService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHttpClient<PagamentoClient>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Apply migrations at startup
using (var scope = app.Services.CreateScope())
{
	var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
	dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
	// Disable HTTPS redirection in Development environment
	//app.UseHttpsRedirection();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
