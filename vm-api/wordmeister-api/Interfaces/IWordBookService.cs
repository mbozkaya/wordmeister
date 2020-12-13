using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wordmeister_api.Dtos.WordBook;

namespace wordmeister_api.Interfaces
{
    public interface IWordBookService
    {
        void CreateRegister(WordBookDto.Create model, int userId);
        public void CreateAnswer(WordBookDto.CreateAnswer model);
        void RemoveRegister(int id);
        void RemoveAnswer(int id);
        void UpdateRegister(WordBookDto.Update model);
        void UpdateAnswer(WordBookDto.Update model);
        List<WordBookDto.Keyword> GetKeywords(int skip = 1, int take = 50);
        List<WordBookDto.KeywordAnswer> GetKeywordAnswers(int keywordId);
        public string CheckAnswer(WordBookDto.CheckAnswer model);
    }
}
