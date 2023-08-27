using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Http.Headers;
using Order.Mobile.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using IdentityModel.OidcClient;

namespace Order.Mobile.Services.Backend;

public class BackendService : IBackendService
{
    private readonly JsonSerializerSettings _serializerSettings;

    public BackendService()
    {
        _serializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            DateTimeZoneHandling = DateTimeZoneHandling.Utc,
            NullValueHandling = NullValueHandling.Ignore
        };
        _serializerSettings.Converters.Add(new StringEnumConverter());
    }

    public async Task<TResult> GetAsync<TResult>(string uri)
    {
        HttpClient httpClient = CreateHttpClient(); // maybe toch terug met token
        HttpResponseMessage response = await httpClient.GetAsync(uri);

        await HandleResponse(response);
        string serialized = await response.Content.ReadAsStringAsync();

        TResult result = await Task.Run(() =>
            JsonConvert.DeserializeObject<TResult>(serialized, _serializerSettings));

        return result;
    }

    /*
         public async Task<TResult> GetAsync<TResult>(string uri, string token = "")
    {
        HttpClient httpClient = CreateHttpClient(token);
        HttpResponseMessage response = await httpClient.GetAsync(uri);

        await HandleResponse(response);
        string serialized = await response.Content.ReadAsStringAsync();

        TResult result = await Task.Run(() =>
            JsonConvert.DeserializeObject<TResult>(serialized, _serializerSettings));

        return result;
    }

     */

    //public async Task<TResult> PostAsync<TResult>(string uri, TResult data, string token = "")
    //{
    //    HttpClient httpClient = CreateHttpClient(token);
    //
    //    var content = new StringContent(JsonConvert.SerializeObject(data));
    //    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
    //    HttpResponseMessage response = await httpClient.PostAsync(uri, content);
    //
    //    await HandleResponse(response);
    //    string serialized = await response.Content.ReadAsStringAsync();
    //
    //    TResult result = await Task.Run(() =>
    //        JsonConvert.DeserializeObject<TResult>(serialized, _serializerSettings));
    //
    //    return result;
    //}

    public async Task<TResult> PostAsync<TResult>(string uri, TResult data)
    {
        HttpClient httpClient = CreateHttpClient();

        var content = new StringContent(JsonConvert.SerializeObject(Guid.NewGuid()));
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        HttpResponseMessage response = await httpClient.PostAsync(uri, content);

        await HandleResponse(response);
        string serialized = await response.Content.ReadAsStringAsync();

        TResult result = await Task.Run(() =>
            JsonConvert.DeserializeObject<TResult>(serialized, _serializerSettings));

        return result;
    }

    public async Task<TResult> PostAsyncWithGuid<TResult>(string uri, Guid data)
    {
        HttpClient httpClient = CreateHttpClient();

        var content = new StringContent(JsonConvert.SerializeObject(data));
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        HttpResponseMessage response = await httpClient.PostAsync(uri, content);

        await HandleResponse(response);
        string serialized = await response.Content.ReadAsStringAsync();

        TResult result = await Task.Run(() =>
            JsonConvert.DeserializeObject<TResult>(serialized, _serializerSettings));

        return result;
    }

    public async Task<TResult> PostAsync<TResult>(string uri, string data, string clientId = "", string clientSecret = "")
    {
        HttpClient httpClient = CreateHttpClient(string.Empty);

        if (!string.IsNullOrWhiteSpace(clientId) && !string.IsNullOrWhiteSpace(clientSecret))
        {
            AddBasicAuthenticationHeader(httpClient, clientId, clientSecret);
        }

        var content = new StringContent(data);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
        HttpResponseMessage response = await httpClient.PostAsync(uri, content);

        await HandleResponse(response);
        string serialized = await response.Content.ReadAsStringAsync();

        TResult result = await Task.Run(() =>
            JsonConvert.DeserializeObject<TResult>(serialized, _serializerSettings));

        return result;
    }

    public async Task<TResult> PutAsync<TResult>(string uri, TResult data, string token = "")
    {
        HttpClient httpClient = CreateHttpClient(token);

        var content = new StringContent(JsonConvert.SerializeObject(data));
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        HttpResponseMessage response = await httpClient.PutAsync(uri, content);

        await HandleResponse(response);
        string serialized = await response.Content.ReadAsStringAsync();

        TResult result = await Task.Run(() =>
            JsonConvert.DeserializeObject<TResult>(serialized, _serializerSettings));

        return result;
    }

    public async Task<TResult> PutAsyncWithGuid<TResult>(string uri, Guid data)
    {
        HttpClient httpClient = CreateHttpClient();

        var content = new StringContent(JsonConvert.SerializeObject(data));
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        HttpResponseMessage response = await httpClient.PutAsync(uri, content);

        await HandleResponse(response);
        string serialized = await response.Content.ReadAsStringAsync();

        TResult result = await Task.Run(() =>
            JsonConvert.DeserializeObject<TResult>(serialized, _serializerSettings));

        return result;
    }

    public async Task DeleteAsync(string uri, string token = "")
    {
        HttpClient httpClient = CreateHttpClient(token);
        await httpClient.DeleteAsync(uri);
    }

    public async Task<TResult> DeleteAsyncWithResult<TResult>(string uri)
    {
        HttpClient httpClient = CreateHttpClient();
        HttpResponseMessage response = await httpClient.DeleteAsync(uri);

        await HandleResponse(response);
        string serialized = await response.Content.ReadAsStringAsync();

        TResult result = await Task.Run(() =>
            JsonConvert.DeserializeObject<TResult>(serialized, _serializerSettings));

        return result;
    }

    private HttpClient CreateHttpClient(string token = "")
    {
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        if (!string.IsNullOrEmpty(token))
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        return httpClient;
    }


    private async Task HandleResponse(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.Forbidden ||
                response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new BackendAuthenticationException(content);
            }

            throw new BackendHttpException(response.StatusCode, content);
        }
    }

    private void AddBasicAuthenticationHeader(HttpClient httpClient, string clientId, string clientSecret)
    {
        if (httpClient == null)
            return;

        if (string.IsNullOrWhiteSpace(clientId) || string.IsNullOrWhiteSpace(clientSecret))
            return;

        httpClient.DefaultRequestHeaders.Authorization = new BasicAuthenticationHeaderValue(clientId, clientSecret);
    }

    public async Task<TResult> PostAsyncWithString<TResult>(string uri, string data)
    {
        HttpClient httpClient = CreateHttpClient();

       
        var content = new StringContent(JsonConvert.SerializeObject(data));

        //content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        HttpResponseMessage response = await httpClient.PostAsync(uri, content);

        await HandleResponse(response);
        string serialized = await response.Content.ReadAsStringAsync();

        TResult result = await Task.Run(() =>
            JsonConvert.DeserializeObject<TResult>(serialized, _serializerSettings));

        return result;
    }

    public async Task<TResult> PutAsyncWithString<TResult>(string uri, string data)
    {
        HttpClient httpClient = CreateHttpClient();

        var content = new StringContent(JsonConvert.SerializeObject(data));
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        HttpResponseMessage response = await httpClient.PutAsync(uri, content);

        await HandleResponse(response);
        string serialized = await response.Content.ReadAsStringAsync();

        TResult result = await Task.Run(() =>
            JsonConvert.DeserializeObject<TResult>(serialized, _serializerSettings));

        return result;
    }
}