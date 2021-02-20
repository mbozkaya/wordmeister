using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using wordmeister_api.Dtos.Word;

namespace wordmeister_api.Services
{
    public class WordAPIService
    {
        HttpClient _client;
        private HttpRequestMessage _request;
        public WordAPIService()
        {
            _client = new HttpClient();

            _request = new HttpRequestMessage
            {
                Headers =
                        {
                            { "x-rapidapi-key", "fcc2d442b7msh0ba3fe97c76c156p18e6c2jsnf43fc9bf2698" },
                            { "x-rapidapi-host", "wordsapiv1.p.rapidapi.com" },
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
