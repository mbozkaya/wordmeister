using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using wordmeister_api.Dtos;
using wordmeister_api.Dtos.Word;
using wordmeister_api.Interfaces;

namespace wordmeister_api.Services
{
    public class WordAPIService: IWordAPIService
    {
        HttpClient _client;
        private HttpRequestMessage _request;
        IOptions<Appsettings> _appSettings;
        public WordAPIService(IOptions<Appsettings> appSettings)
        {
            _appSettings = appSettings;
            _client = new HttpClient();

            _request = new HttpRequestMessage
            {
                Headers =
                        {
                            { "x-rapidapi-key", _appSettings.Value.RapidApi.XRapidapiKey },
                            { "x-rapidapi-host", _appSettings.Value.RapidApi.XRapidapiHost },
                            { "useQueryString", "true" },
                        },
            };
        }
        public async void GetWord(string word)
        {

            _request.Method = HttpMethod.Get;
            _request.RequestUri = new Uri("https://wordsapiv1.p.rapidapi.com/words/away");

            var result = await SendRequest();

        }

        public async Task<WordApiResponse.ExampleDto> GetExample(string word)
        {
            //if not exist
            _request.Method = HttpMethod.Get;
            _request.RequestUri = new Uri($"https://wordsapiv1.p.rapidapi.com/words/{word}/examples");

            var response = JsonConvert.DeserializeObject<WordApiResponse.ExampleDto>(await SendRequest());
            //db insert
            //end if

            return response;
        }

        private async Task<string> SendRequest()
        {
            string responseResult = string.Empty;
            using (var response = await _client.SendAsync(_request))
            {
                response.EnsureSuccessStatusCode();
                responseResult = await response.Content.ReadAsStringAsync();
            }

            return responseResult;
        }

    }
}
