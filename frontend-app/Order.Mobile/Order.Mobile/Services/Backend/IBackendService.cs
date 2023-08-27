using IdentityModel.OidcClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Mobile.Services.Backend;

public interface IBackendService
{
    Task<TResult> GetAsync<TResult>(string uri);
    //Task<TResult> GetAsync<TResult>(string uri, string token = ""); token niet nodig denk ik maar toch  nog nie verwijderen voor de zekerheid

    //Task<TResult> PostAsync<TResult>(string uri, TResult data, string token = "");
    Task<TResult> PostAsync<TResult>(string uri, TResult data);
    Task<TResult> PostAsyncWithGuid<TResult>(string uri, Guid data);

    Task<TResult> PostAsyncWithString<TResult>(string uri, string data);

    Task<TResult> PostAsync<TResult>(string uri, string data, string clientId = "", string clientSecret = "");

    Task<TResult> PutAsync<TResult>(string uri, TResult data, string token = "");
    Task<TResult> PutAsyncWithGuid<TResult>(string uri, Guid data);

    Task DeleteAsync(string uri, string token = "");
    Task<TResult> DeleteAsyncWithResult<TResult>(string uri);
    Task<TResult> PutAsyncWithString<TResult>(string uri, string data);
}