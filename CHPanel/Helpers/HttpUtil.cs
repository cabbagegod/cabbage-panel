using System.Net;
using System.Net.Http;
using CHPanel.Data;

namespace CHPanel.Helpers; 

public static class HttpUtil {
	private static HttpClient Client => client ??= new HttpClient();
	private static HttpClient? client;

	private const string urlBase = "http://10.0.0.203/";
	
	public static async Task<T?> GetAsync<T>(string endpoint, string apiKey = "") {
		EnsureHttpHeaders(apiKey);
		HttpResponseMessage response = await Client.GetAsync($"{urlBase}{endpoint}");
		
		string content = await response.Content.ReadAsStringAsync();
		
		return response.StatusCode == HttpStatusCode.OK ? Newtonsoft.Json.JsonConvert.DeserializeObject<T>(content) : default;
	}

	public static async Task<bool> PostAsync<T>(string endpoint, T contentObj) {
		EnsureHttpHeaders(string.Empty);
        
		string stringContent = Newtonsoft.Json.JsonConvert.SerializeObject(contentObj);
		HttpContent content = new StringContent(stringContent, System.Text.Encoding.UTF8, "application/json");
		
		HttpResponseMessage response = await Client.PostAsync($"{urlBase}{endpoint}", content);
		return response.StatusCode == HttpStatusCode.OK;
	}
    
	private static void EnsureHttpHeaders(string apiKey) {
		if (string.IsNullOrEmpty(apiKey))
			apiKey = AuthData.apiKey;
		
		Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);
		if(Client.DefaultRequestHeaders.Accept.Count == 0)
			Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("Application/vnd.pterodactyl.v1+json"));
	}
}