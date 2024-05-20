using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using G64.PedidoAPI.DTOs;

namespace G64.PedidoAPI.Services
{
	public class PagamentoClient
	{
		private readonly HttpClient _httpClient;
		private readonly string _baseUrl;

		public PagamentoClient(HttpClient httpClient, IConfiguration configuration)
		{
			_httpClient = httpClient;
			_baseUrl = configuration["ApiGateway:BaseUrl"];
		}

		public async Task<PagamentoResponseDTO> ProcessPaymentAsync(PagamentoRequestDTO paymentRequest)
		{
			var json = JsonSerializer.Serialize(paymentRequest);
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			var response = await _httpClient.PostAsync($"{_baseUrl}/pagamento/api/pagamento", content);

			if (response.IsSuccessStatusCode)
			{
				var responseStream = await response.Content.ReadAsStreamAsync();
				var result = await JsonSerializer.DeserializeAsync<PagamentoResponseDTO>(responseStream);
				result.IsSuccess = true;
				return result;
			}
			else
			{
				return new PagamentoResponseDTO { IsSuccess = false };
			}
		}
	}
}
