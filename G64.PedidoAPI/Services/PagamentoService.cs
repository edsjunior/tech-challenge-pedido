using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using G64.PedidoAPI.DTOs;
using G64.PedidoAPI.Models;

namespace G64.PedidoAPI.Services
{
    public class PagamentoService
    {
        private readonly HttpClient _httpClient;

        public PagamentoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PagamentoResponse> CriaPagamentoAsync(PagamentoRequestDTO pagamentoRequest)
        {
            // Serializar o objeto PagamentoRequest para JSON
            var jsonRequest = JsonSerializer.Serialize(pagamentoRequest);

            // Criar o conteúdo da requisição HTTP
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            // Enviar a requisição POST para a API Java
            var response = await _httpClient.PostAsync("/v1/pagamento", content);

            // Garantir que a resposta foi bem-sucedida
            response.EnsureSuccessStatusCode();

            // Ler e desserializar a resposta JSON
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<PagamentoResponse>(jsonResponse);
        }
    }
}
