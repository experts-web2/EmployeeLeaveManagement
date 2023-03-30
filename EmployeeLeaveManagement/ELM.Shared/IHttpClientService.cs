

namespace ELM.Shared
{
    public interface IHttpClientService
    {
        Task<HttpResponseMessage> PostAsync<T>(T model, string path) where T : class;
        Task<T> PutAsync<T>(T model, string path) where T : class;
        Task<T> PutAsync<T>(string path);
        Task<HttpResponseMessage> DeleteAsync(string path);
        Task<T> GetAsync<T>(string path);
        Task<T> SendAsync<T, TModel>(string path, TModel model, HttpMethod method);
        Task<T> DeserializeAsync<T>(HttpResponseMessage response);
        string Serialize<T>(T model);
        Task<HttpResponseMessage> Post<T>(T model, string path) where T : class;
        Task<returnT> PostAsJsonAsync<returnT, modelT>(modelT model, string path) where modelT : class;
    }
}
