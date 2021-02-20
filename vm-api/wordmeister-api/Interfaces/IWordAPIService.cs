using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wordmeister_api.Dtos.Word;

namespace wordmeister_api.Interfaces
{
    public interface IWordAPIService
    {
        void GetWord(string word);
        Task<WordApiResponse.ExampleDto> GetExample(string word);
    }
}
