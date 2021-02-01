using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private WordmeisterContext context;
        public User currentUser;
        private readonly IHttpContextAccessor _httpContext;

        public WordService(WordmeisterContext wordMeisterDbContext)
        {
            context = wordMeisterDbContext;

            currentUser = context.Users.FirstOrDefault();
            //TODO: swaggerden auth kontrol edilemedigi icin simdilik yorum satırında kalacak.
            //currentUser = (User)_httpContext.HttpContext.Items["User"];
        }

        public WordResponse GetWord(long wordId)
        {
            var exist = context.UserWords.FirstOrDefault(x => x.WordId == wordId && x.UserId == currentUser.Id);

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

        public PageResponse GetWords(int pageNumber, int pageSize)
        {
            var query = context.UserWords.Where(x => x.UserId == currentUser.Id);
            var page = query.OrderBy(x => x.WordId)
                .Select(x => new WordResponse
                {
                    Id = x.WordId,
                    Description = x.Description,
                    Text = x.Word.Text,
                    Sentences = x.Word.Sentences.Select(s => new SentenceDto
                    {
                        Id = s.Id,
                        Text = s.Text
                    }).ToList()
                })
            .Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            int total = query.Count();
            var words = page.Select(x => x);

            if (total < 1)
                return null;

            return new PageResponse { Data = words, Total = total };
        }

        public ResponseResult AddWord(WordRequest model)
        {
            var existWord = context.Words.FirstOrDefault(x => x.Text == model.Text);

            var userWord = new UserWord();

            if (!existWord.IsNullOrDefault())
            {
                var exist = context.UserWords.FirstOrDefault(x => x.UserId == currentUser.Id && x.WordId == existWord.Id);

                if (!exist.IsNullOrDefault())
                    return new ResponseResult() { Error = true, ErrorMessage = "Such a word has been added before." };

                userWord = new UserWord
                {
                    User = currentUser,
                    Word = existWord,
                    Description = model.Description,
                    CreatedDate = DateTime.Now
                };
            }
            else
            {
                var word = new Word
                {
                    Text = model.Text,
                    Sentences = null,
                    CreatedDate = DateTime.Now
                };

                userWord = new UserWord
                {
                    User = currentUser,
                    Word = word,
                    Description = model.Description
                };
            }

            context.UserWords.Add(userWord);
            context.SaveChanges();

            return new ResponseResult() { Data = new WordResponse { Text = userWord.Word.Text, Description = userWord.Description } };

        }

        public bool DeleteWord(long wordId)
        {
            var exist = context.UserWords.FirstOrDefault(x => x.WordId == wordId && x.UserId == currentUser.Id);

            if (exist.IsNullOrDefault())
                return false;

            context.UserWords.Remove(exist);
            context.SaveChanges();

            return true;
        }
        
        public bool UpdateWord(WordRequest model)
        {
            var existWord = context.UserWords.FirstOrDefault(x => x.WordId == model.Id && x.UserId == currentUser.Id);

            if (existWord.IsNullOrDefault())
                return false;

            existWord.Description = model.Description;
            existWord.UpdateDate = DateTime.Now;

            context.SaveChanges();

            return true;
        }
    }
}