namespace Api.Services
{
    public interface IExchangeRateService
    {
        Task<decimal> GetCopRateAsync();
    }

    public class ExchangeRateService : IExchangeRateService
    {
        private readonly HttpClient _httpClient;

        public ExchangeRateService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<decimal> GetCopRateAsync()
        {
            try
            {
                // Simulando llamada a API externa: GET https://example.com/exchangerate
                // En un escenario real: 
                // var response = await _httpClient.GetFromJsonAsync<ExchangeRateResponse>("https://example.com/exchangerate");
                // return response?.CopRate ?? 4300;

                await Task.Delay(100); // Simulando latencia
                return 4300m; // Tasa simulada
            }
            catch (Exception)
            {
                // Manejo de errores: Si la API falla, retornamos una tasa base o lanzamos error según política
                return 4000m; 
            }
        }
    }

    public class ExchangeRateResponse
    {
        public decimal UsdRate { get; set; }
        public decimal CopRate { get; set; }
    }
}
