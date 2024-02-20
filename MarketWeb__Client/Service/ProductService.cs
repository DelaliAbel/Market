using MarketWeb__Client.Service.IService;
using MarketWeb_Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace MarketWeb__Client.Service
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private IConfiguration _configuration;
        private string BaseServeUrl;

        public ProductService(HttpClient i_httpClient, IConfiguration configuration)
        {
            _httpClient = i_httpClient;
            _configuration = configuration;
            BaseServeUrl = _configuration.GetSection("BaseServerUrl").Value;
        }

        public async Task<ProductDTO> Get(int i_productId)
        {
            var response = await _httpClient.GetAsync($"/api/product/{i_productId}");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var product = JsonConvert.DeserializeObject<ProductDTO>(content);
                product.ImageUrl = BaseServeUrl + product.ImageUrl;
                return product;
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorModelDTO>(content);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<IEnumerable<ProductDTO>> GetAll()
        {
            var response = await _httpClient.GetAsync("/api/product");
            if(response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<List<ProductDTO>>(content);
                foreach(var product in products)
                {
                    product.ImageUrl = BaseServeUrl + product.ImageUrl;
                }
                return products;
            }

            return new List<ProductDTO>();
        }
    }
}
