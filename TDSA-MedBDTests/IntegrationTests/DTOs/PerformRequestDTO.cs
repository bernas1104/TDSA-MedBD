using System.Net.Http;

namespace TDSA_MedBDTest.IntegrationTests.DTOs {
  public class PerformRequestDTO {
    public HttpClient Client { get; set; }
    public int HttpAction { get; set; }
    public string HttpRoute { get; set; }
    public object Data { get; set; }
  }
}
