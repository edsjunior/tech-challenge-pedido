using G64.PedidoAPI.Context;
using G64.PedidoAPI.Repositories;
using G64.PedidoAPI.Services;
using Microsoft.EntityFrameworkCore;
using System.Net;


var builder = WebApplication.CreateBuilder(args);

// Configure TLS 1.2
ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

builder.Services.AddDbContext<AppDbContext>(options =>
		options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<PedidoService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adiciona HttpClient ao container de dependências
builder.Services.AddHttpClient<PagamentoService>(client =>
{
	var pagamentoApiUrl = builder.Configuration["PAGAMENTO_API_URL"];
	client.BaseAddress = new Uri(pagamentoApiUrl);
});

// Registrar o consumidor RabbitMQ como serviço hospedado
builder.Services.AddHostedService<RabbitMqConsumer>();

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
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
