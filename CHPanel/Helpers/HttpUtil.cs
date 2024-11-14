using System.Net;
using System.Net.Http;

namespace CHPanel.Helpers; 

public class HttpUtil {
	private static HttpClient Client => client ??= new HttpClient();
	private static HttpClient? client;

	private const string urlBase = "http://10.0.0.203/";
	
	public static async Task<T?> GetAsync<T>(string endpoint, string apiKey) {
		Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);
		Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("Application/vnd.pterodactyl.v1+json"));
		HttpResponseMessage response = await Client.GetAsync($"{urlBase}{endpoint}");
		
		string content = await response.Content.ReadAsStringAsync();
		
		return response.StatusCode == HttpStatusCode.OK ? Newtonsoft.Json.JsonConvert.DeserializeObject<T>(content) : default;
	}
}