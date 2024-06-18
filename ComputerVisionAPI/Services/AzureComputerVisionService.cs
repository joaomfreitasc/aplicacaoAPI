using System.Net.Http.Headers;
using System.Text.Json;

/// <summary>
/// Classe de serviço responsável em buscar as informações dca imagem usando o serviço ComputerVision do Azure.
/// </summary>
public class AzureComputerVisionService
{
  private readonly HttpClient _httpClient;
  private readonly string _endpoint;
  private readonly string _subscriptionKey;

  public AzureComputerVisionService(HttpClient httpClient, IConfiguration configuration)
  {
    _httpClient = httpClient;
    _endpoint = configuration["AzureComputerVision:Endpoint"];
    _subscriptionKey = configuration["AzureComputerVision:SubscriptionKey"];
  }

  public async Task<JsonDocument> AnalyzeImageAsync(byte[] imageBytes)
  {
    var requestUri = $"{_endpoint}/vision/v3.1/analyze?visualFeatures=Tags&language=pt";
    using var content = new ByteArrayContent(imageBytes);
    content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
    _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _subscriptionKey);
    var response = await _httpClient.PostAsync(requestUri, content);
    response.EnsureSuccessStatusCode();
    var responseStream = await response.Content.ReadAsStreamAsync();
    return await JsonDocument.ParseAsync(responseStream);
  }
}
