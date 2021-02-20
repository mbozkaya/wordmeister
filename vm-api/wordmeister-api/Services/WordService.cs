using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wordmeister_api.Dtos.General;
using wordmeister_api.Dtos.Word;
using wordmeister_api.Entity;
using wordmeister_api.Helpers;
using wordmeister_api.Interfaces;
using wordmeister_api.Model;
using static wordmeister_api.Dtos.General.General;

namespace wordmeister_api.Services
{
    public class WordService : IWordService
    {
        WordmeisterContext _wordMeisterDbContext;
        ITranslateService _translateService;
        IServiceScopeFactory _serviceScopeFactory;

        public WordService(WordmeisterContext wordMeisterDbContext, ITranslateService translateService, IServiceScopeFactory serviceScopeFactory)
        {
            _wordMeisterDbContext = wordMeisterDbContext;
            _translateService = translateService;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public WordResponse GetWord(long wordId, int userId)
        {
            var exist = _wordMeisterDbContext.UserWords.FirstOrDefault(x => x.WordId == wordId && x.UserId == userId);

            if (exist.IsNullOrDefault())
                return null;


            return new WordResponse
            {
                Id = exist.WordId,
                Text = exist.Word.Text,
                Description = exist.Description,
                Sentences = exist.Word.Sentences.Select(s => new SentenceDto
                {
                    Id = s.Id,
                    Text = s.Text
                }).ToList()
            };
        }

        public PageResponse GetWords(int pageNumber, int pageSize, int userId)
        {
            var query = _wordMeisterDbContext.UserWords.Where(x => x.UserId == userId);
            var page = query.OrderByDescending(x => x.CreatedDate)
                .Select(x => new WordResponse
                {
                    Id = x.WordId,
                    Description = x.Description,
                    Text = x.Word.Text,
                    Sentences = x.Word.Sentences.Select(s => new SentenceDto
                    {
                        Id = s.Id,
                        Text = s.Text
                    }).ToList(),
                    CreatedDate = x.CreatedDate,
                })
            .Skip((pageNumber) * pageSize).Take(pageSize).ToList();

            int total = query.Count();
            var words = page.Select(x => x);

            return new PageResponse { Data = words, Total = total };
        }

        public ResponseResult AddWord(WordRequest model, int userId)
        {
            var existWord = _wordMeisterDbContext.Words.FirstOrDefault(x => x.Text == model.Text);

            var userWord = new UserWord();

            if (existWord.IsNullOrDefault())
            {
                var newWord = _wordMeisterDbContext.Words.Add(new Word
                {
                    Text = model.Text,
                    Sentences = null,
                    CreatedDate = DateTime.Now,
                });

                //_translateService.TranslateText(model.Text);
                _wordMeisterDbContext.SaveChanges();

                userWord = new UserWord
                {
                    UserId = userId,
                    WordId = newWord.Entity.Id,
                    Description = model.Description,
                    CreatedDate = DateTime.Now
                };
                _wordMeisterDbContext.UserWords.Add(userWord);
                _wordMeisterDbContext.SaveChanges();

                Task.Run(() => { AddSentences(newWord.Entity); });
            }
            else
            {
                var exist = _wordMeisterDbContext.UserWords.FirstOrDefault(x => x.UserId == userId && x.WordId == existWord.Id);

                if (!exist.IsNullOrDefault())
                    return new ResponseResult() { Error = true, ErrorMessage = "Such a word has been added before." };

                userWord = new UserWord
                {
                    UserId = userId,
                    WordId = existWord.Id,
                    Description = model.Description,
                    CreatedDate = DateTime.Now
                };
                _wordMeisterDbContext.UserWords.Add(userWord);
                _wordMeisterDbContext.SaveChanges();
            }

            return new ResponseResult();
        }

        public void DeleteWord(long wordId, int userId)
        {
            var exist = _wordMeisterDbContext.UserWords.FirstOrDefault(x => x.WordId == wordId && x.UserId == userId);

            _wordMeisterDbContext.UserWords.Remove(exist);
            _wordMeisterDbContext.SaveChanges();
        }

        public void UpdateWord(WordRequest model, int userId)
        {
            var existWord = _wordMeisterDbContext.UserWords.FirstOrDefault(x => x.WordId == model.Id && x.UserId == userId);

            existWord.Description = model.Description;
            existWord.UpdateDate = DateTime.Now;

            _wordMeisterDbContext.SaveChanges();
        }

        private async void AddSentences(Word word)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetService<WordmeisterContext>();

                var isExistSentences = db.Sentences.Where(w => w.WordId == word.Id).Any();

                if (!isExistSentences)
                {
                    WordAPIService wordAPIService = new WordAPIService();
                    var sentences = await wordAPIService.GetExample(word.Text);

                    foreach (var example in sentences.Examples)
                    {
                        db.Sentences.Add(new Sentence
                        {
                            CreatedDate = DateTime.Now,
                            Text = example,
                            WordId = word.Id
                        });
                    }

                    db.SaveChanges();
                }
            }
            
        }
    }
}