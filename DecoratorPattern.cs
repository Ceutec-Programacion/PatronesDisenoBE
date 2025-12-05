using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatronesDisenoEstructurales
{
  //interfaz componente
  public interface IHttp
    {
        Task<string> Get(string url);
    }

    public sealed class FetchDecorator : IHttp
    {
        public async Task<string> Get(string url)
        {
            using var client = new HttpClient();
            return await client.GetStringAsync(url);
        }
    }
    public sealed class LoggingDecorator : IHttp
    {
        private readonly IHttp _http;

        public LoggingDecorator(IHttp http) => _http = http;

        public async Task<string> Get(string url)
        {
            var sw = System.Diagnostics.Stopwatch.StartNew(); 
            var result = await _http.Get(url);
            sw.Stop();
            return result;
        }
    }

    public sealed class CachingHttp : IHttp
    {
        private readonly IHttp _http;

        public async Task<string> Get(string url)
        {
            var res = await _http.Get(url);
            return res;
        }
    }
   
}
