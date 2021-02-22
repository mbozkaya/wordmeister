using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
        WordmeisterContext _dbContext;
        ITranslateService _translateService;
        IServiceScopeFactory _serviceScopeFactory;
        IWordAPIService _wordAPIService;

        public WordService(WordmeisterContext dbContext, ITranslateService translateService, IWordAPIService wordAPIService, IServiceScopeFactory serviceScopeFactory)
        {
            _dbContext = dbContext;
            _translateService = translateService;
            _serviceScopeFactory = serviceScopeFactory;
            _wordAPIService = wordAPIService;
        }

        public WordResponse.Word GetWord(long wordId, int userId)
        {
            var exist = _dbContext.UserWords.FirstOrDefault(x => x.WordId == wordId && x.UserId == userId);

            if (exist.IsNullOrDefault())
                return null;


            return new WordResponse.Word
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
            var query = _dbContext.UserWords.Where(x => x.UserId == userId);
            var page = query.OrderByDescending(x => x.CreatedDate)
                .Select(x => new WordResponse.Word
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

        public ResponseResult AddWord(WordRequest.Add model, int userId)
        {
            var existWord = _dbContext.Words.FirstOrDefault(x => x.Text == model.Text);

            var userWord = new UserWord();

            if (existWord.IsNullOrDefault())
            {
                var newWord = _dbContext.Words.Add(new Word
                {
                    Text = model.Text,
                    Sentences = null,
                    CreatedDate = DateTime.Now,
                });

                _dbContext.SaveChanges();

                userWord = new UserWord
                {
                    UserId = userId,
                    WordId = newWord.Entity.Id,
                    Description = model.Description,
                    CreatedDate = DateTime.Now
                };
                _dbContext.UserWords.Add(userWord);
                _dbContext.SaveChanges();

                Task.Run(() => { AddWordModel(newWord.Entity); });
            }
            else
            {
                var exist = _dbContext.UserWords.FirstOrDefault(x => x.UserId == userId && x.WordId == existWord.Id);

                if (!exist.IsNullOrDefault())
                    return new ResponseResult() { Error = true, ErrorMessage = "Such a word has been added before." };

                userWord = new UserWord
                {
                    UserId = userId,
                    WordId = existWord.Id,
                    Description = model.Description,
                    CreatedDate = DateTime.Now
                };
                _dbContext.UserWords.Add(userWord);
                _dbContext.SaveChanges();
            }

            return new ResponseResult();
        }

        public void DeleteWord(long wordId, int userId)
        {
            var exist = _dbContext.UserWords.FirstOrDefault(x => x.WordId == wordId && x.UserId == userId);

            _dbContext.UserWords.Remove(exist);
            _dbContext.SaveChanges();
        }

        public void UpdateWord(WordRequest.Add model, int userId)
        {
            var existWord = _dbContext.UserWords.FirstOrDefault(x => x.WordId == model.Id && x.UserId == userId);

            existWord.Description = model.Description;
            existWord.UpdateDate = DateTime.Now;

            _dbContext.SaveChanges();
        }

        public WordResponse.WordCard GetWordCard(int userId, int currentIndex = 1, bool isRandom = false)
        {

            var words = _dbContext.UserWords
                .Where(w => w.UserId == userId && !w.IsLearned)
                .Include(i => i.Word)
                .ToList();

            if (currentIndex > words.Count)
            {
                return new WordResponse.WordCard
                {
                    IsOver = true,
                };
            }

            var userWord = new UserWord();

            if (isRandom)
            {
                var random = new Random();

                userWord = words[random.Next(words.Count)];
            }
            else
            {
                userWord = words[currentIndex - 1];
            }

            return new WordResponse.WordCard
            {
                Sentences = _dbContext.Sentences.Where(w => w.WordId == userWord.WordId).Select(s => s.Text).ToList(),
                Description = userWord.Description,
                Word = userWord.Word.Text,
                CurrentIndex = currentIndex,
                IsFavorite = userWord.IsFavorite,
                IsOver = currentIndex == words.Count,
                Point = userWord.Point,
                UserWordId = userWord.Id,
                WordCount = words.Count
            };

        }

        public ResponseResult SetWordPoint(WordRequest.WordPoint model)
        {
            var userWord = GetUserWord(model.UserWordId);

            if (userWord.IsNullOrDefault())
            {
                return new ResponseResult() { Error = true, ErrorMessage = "The word not found" };
            }

            userWord.Point = (byte)model.Point;
            _dbContext.SaveChanges();

            return new ResponseResult();
        }

        public ResponseResult SetWordFavorite(WordRequest.WordFavorite model)
        {
            var userWord = GetUserWord(model.UserWordId);

            if (userWord.IsNullOrDefault())
            {
                return new ResponseResult() { Error = true, ErrorMessage = "The word not found" };
            }

            userWord.IsFavorite = model.IsFavorite;
            userWord.UpdateDate = DateTime.Now;
            _dbContext.SaveChanges();

            return new ResponseResult();
        }

        public ResponseResult AddCustomSentence(WordRequest.CustomSentence model)
        {
            var userWord = GetUserWord(model.UserWordId);

            if (userWord.IsNullOrDefault())
            {
                return new ResponseResult() { Error = true, ErrorMessage = "The word not found" };
            }

            _dbContext.Sentences.Add(new Sentence
            {
                CreatedDate = DateTime.Now,
                IsPrivate = model.IsPrivate,
                UserId = userWord.UserId,
                Text = model.Sentence,
            });

            _dbContext.SaveChanges();

            return new ResponseResult();
        }

        public async void GetRandomWord()
        {
            var result = await _wordAPIService.GetRandom();
        }

        private async void AddSentences(Word word)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetService<WordmeisterContext>();

                var isExistSentences = db.Sentences.Where(w => w.WordId == word.Id).Any();

                if (!isExistSentences)
                {
                    var sentences = await _wordAPIService.GetExample(word.Text);

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

        private async void AddWordModel(Word word)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetService<WordmeisterContext>();

                var response = await _wordAPIService.GetWord(word.Text);

                var now = DateTime.Now;
                var executionStrategy = db.Database.CreateExecutionStrategy();

                executionStrategy.Execute(() =>
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            db.WordFrequencies.Add(new WordFrequency
                            {
                                WordId = word.Id,
                                PerMillion = response.Frequency,
                                Zipf = decimal.Zero,
                                Diversity = decimal.Zero,
                                CreatedDate = now,
                            });

                            db.WordPronunciations.Add(new WordPronunciation
                            {
                                WordId = word.Id,
                                All = response.Pronunciation.All,
                                Noun = response.Pronunciation.Noun,
                                Verb = response.Pronunciation.Verb,
                                CreatedDate = now,
                            });

                            response.Syllables.List.ForEach(f =>
                            {
                                db.WordSyllables.Add(new WordSyllable
                                {
                                    Syllable = f,
                                    WordId = word.Id,
                                    CreatedDate = now,
                                });
                            });

                            response.Results.ForEach(result =>
                            {
                                result.Antonyms.ForEach(antonym =>
                                {
                                    db.WordAntonyms.Add(new WordAntonym
                                    {
                                        WordId = word.Id,
                                        Antonym = antonym,
                                        CreatedDate = now,
                                    });
                                });

                                db.WordDefinations.Add(new WordDefinition
                                {
                                    WordId = word.Id,
                                    Definition = result.Definition,
                                    PartOfSpeech = result.PartOfSpeech,
                                    CreatedDate = now
                                });

                                result.Examples.ForEach(example =>
                                {
                                    db.Sentences.Add(new Sentence
                                    {
                                        WordId = word.Id,
                                        Text = example,
                                        CreatedDate = now
                                    });
                                });

                                result.HasTypes.ForEach(hasType =>
                                {
                                    db.WordHasTypes.Add(new WordHasType
                                    {
                                        WordId = word.Id,
                                        Type = hasType,
                                        CreatedDate = now,
                                    });
                                });

                                result.Synonyms.ForEach(synonym =>
                                {
                                    db.WordSynonyms.Add(new WordSynonym
                                    {
                                        WordId = word.Id,
                                        Synonym = synonym,
                                        CreatedDate = now,
                                    });
                                });


                                result.TypeOf.ForEach(typeOf =>
                                {
                                    db.WordTypeOfs.Add(new WordTypeOf
                                    {
                                        WordId = word.Id,
                                        TypeOf = typeOf,
                                        CreatedDate = now
                                    });
                                });

                            });


                            db.SaveChanges();

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            //TODO Log
                            transaction.Rollback();
                        }
                    }
                });
            }
        }

        private UserWord GetUserWord(int userWordId)
        {
            return _dbContext.UserWords
                .Where(w => w.Id == userWordId)
                .FirstOrDefault();
        }
    }
}