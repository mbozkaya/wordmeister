﻿using Microsoft.AspNetCore.Http;
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
        private WordmeisterContext _wordMeisterDbContext;

        public WordService(WordmeisterContext wordMeisterDbContext)
        {
            _wordMeisterDbContext = wordMeisterDbContext;
        }

        public WordResponse GetWord(long wordId,int userId)
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

        public PageResponse GetWords(int pageNumber, int pageSize,int userId)
        {
            var query = _wordMeisterDbContext.UserWords.Where(x => x.UserId == userId);
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
                    CreatedDate = DateTime.Now
                });

                _wordMeisterDbContext.SaveChanges();

                userWord = new UserWord
                {
                    UserId = userId,
                    WordId = newWord.Entity.Id,
                    Description = model.Description
                };

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
            }
            _wordMeisterDbContext.SaveChanges();

            return new ResponseResult();
        }

        public void DeleteWord(long wordId,int userId)
        {
            var exist = _wordMeisterDbContext.UserWords.FirstOrDefault(x => x.WordId == wordId && x.UserId == userId);

            _wordMeisterDbContext.UserWords.Remove(exist);
            _wordMeisterDbContext.SaveChanges();
        }

        public void UpdateWord(WordRequest model,int userId)
        {
            var existWord = _wordMeisterDbContext.UserWords.FirstOrDefault(x => x.WordId == model.Id && x.UserId == userId);

            existWord.Description = model.Description;
            existWord.UpdateDate = DateTime.Now;

            _wordMeisterDbContext.SaveChanges();
        }
    }
}