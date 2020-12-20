using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wordmeister_api.Dtos.WordBook;
using wordmeister_api.Entities;
using wordmeister_api.Helpers;
using wordmeister_api.Interfaces;

namespace wordmeister_api.Services
{
    public class WordBookSevice : IWordBookService
    {
        WordMeisterDbContext _wordMeisterDbContext;
        public WordBookSevice(WordMeisterDbContext wordMeisterDbContext)
        {
            _wordMeisterDbContext = wordMeisterDbContext;
        }

        public void CreateRegister(WordBookDto.Create model, int userId)
        {
            KeywordRegister newRegister = new KeywordRegister
            {
                Title = model.Title,
                UserId = userId,
            };

            _wordMeisterDbContext.KeywordRegister.Add(newRegister);
            _wordMeisterDbContext.SaveChanges();

            foreach (string key in model.Keys)
            {
                _wordMeisterDbContext.KeywordAnswer.Add(new KeywordAnswer
                {
                    Key = key.Trim().TurkishCharReplace().Replace(" ", "").ToLower(),
                    OriginalKey = key,
                    KeywordRegisterId = newRegister.Id,
                });
            }

            _wordMeisterDbContext.SaveChanges();
        }

        public void CreateAnswer(WordBookDto.CreateAnswer model)
        {
            _wordMeisterDbContext.KeywordAnswer.Add(new KeywordAnswer
            {
                Key = model.Title.Trim().TurkishCharReplace().Replace(" ", "").ToLower(),
                KeywordRegisterId = model.Id,
                OriginalKey = model.Title,
            });

            _wordMeisterDbContext.SaveChanges();
        }

        public void RemoveRegister(int id)
        {
            var register = _wordMeisterDbContext
                .KeywordRegister
                .Where(w => w.Id == id)
                .FirstOrDefault();

            register.Status = false;
            register.UpdateDate = DateTime.Now;
            _wordMeisterDbContext.SaveChanges();
        }

        public void RemoveAnswer(int id)
        {
            var answer = _wordMeisterDbContext
                .KeywordAnswer
                .Where(w => w.Id == id)
                .FirstOrDefault();

            _wordMeisterDbContext.KeywordAnswer.Remove(answer);
            _wordMeisterDbContext.SaveChanges();
        }

        public void UpdateRegister(WordBookDto.Update model)
        {
            KeywordRegister update = _wordMeisterDbContext
                .KeywordRegister
                .Where(w => w.Id == model.Id)
                .FirstOrDefault();

            update.Title = model.Title;
            _wordMeisterDbContext.SaveChanges();
        }

        public void UpdateAnswer(WordBookDto.Update model)
        {
            KeywordAnswer update = _wordMeisterDbContext
                .KeywordAnswer
                .Where(w => w.Id == model.Id)
                .FirstOrDefault();

            update.Key = model.Title;
            _wordMeisterDbContext.SaveChanges();
        }

        public List<WordBookDto.Keyword> GetKeywords(int skip = 0, int take = 50)
        {
            var registers = _wordMeisterDbContext
                .KeywordRegister
                .Where(w => w.Status.Value)
                //.AsEnumerable()
                //.Skip(skip)
                //.Take(take)
                .Select(s => new WordBookDto.Keyword
                {
                    CreatedDate = s.CreatedDate,
                    Id = s.Id,
                    Title = s.Title,
                    CreatedUserName = string.Concat(s.User.FirstName, " ", s.User.LastName),
                    UserId = s.UserId,
                })
                .ToList();
            return registers;
        }
        public List<WordBookDto.KeywordAnswer> GetKeywordAnswers(int keywordId)
        {
            var answers = _wordMeisterDbContext
            .KeywordAnswer
            .Where(w => w.KeywordRegisterId == keywordId)
            .Select(s => new WordBookDto.KeywordAnswer
            {
                Id = s.Id,
                Text = s.Key
            }).ToList();

            return answers;
        }

        public string CheckAnswer(WordBookDto.CheckAnswer model)
        {
            var answers = _wordMeisterDbContext
                .KeywordAnswer
                .Where(w => w.KeywordRegisterId == model.Id)
                .ToList();

            string mutuatedText = model.Title.Trim().TurkishCharReplace().Replace(" ", "").ToLower();

            foreach (var answer in answers)
            {
                float acceptence = (float)answer.Key.Length * 80 / 100;

                if (GetSimilarity(answer.Key, mutuatedText) > acceptence)
                {
                    return answer.OriginalKey;
                }
            }

            return string.Empty;
        }

        private float GetCorrectnessPercentage(string soure, string target)
        {
            int n = soure.Length;
            int m = target.Length;
            int[,] d = new int[n + 1, m + 1];

            // Step 1
            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            // Step 2
            for (int i = 0; i <= n; d[i, 0] = i++)
            {
            }

            for (int j = 0; j <= m; d[0, j] = j++)
            {
            }

            // Step 3
            for (int i = 1; i <= n; i++)
            {
                //Step 4
                for (int j = 1; j <= m; j++)
                {
                    // Step 5
                    int cost = (target[j - 1] == soure[i - 1]) ? 0 : 1;

                    // Step 6
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            // Step 7
            return d[n, m];
        }

        private float GetSimilarity(string string1, string string2)
        {
            float dis = GetCorrectnessPercentage(string1, string2);
            float maxLen = string1.Length;
            if (maxLen < string2.Length)
                maxLen = string2.Length;
            if (maxLen == 0.0F)
                return 1.0F;
            else
                return 1.0F - dis / maxLen;
        }
    }
}
