using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using wordmeister_api.Dtos.WordBook;
using wordmeister_api.Interfaces;

namespace wordmeister_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WordbookController : ControllerBase
    {
        IWordBookService _wordbookService;
        public WordbookController(IWordBookService wordbookService)
        {
            _wordbookService = wordbookService;
        }

        [HttpPost("CreateRegister")]
        public IActionResult CreateRegister(WordBookDto.Create model)
        {
            try
            {
                _wordbookService.CreateRegister(model, 1);
                return Ok(_wordbookService.GetKeywords());
            }
            catch
            {
                return Problem();
            }
        }

        [HttpPost("CreateAnswer")]
        public IActionResult CreateAnswer(WordBookDto.CreateAnswer model)
        {
            try
            {
                _wordbookService.CreateAnswer(model);
                return Ok(_wordbookService.GetKeywordAnswers(model.Id));
            }
            catch
            {
                return Problem();
            }
        }

        [HttpPost("UpdateRegister")]
        public IActionResult UpdateRegister(WordBookDto.Update model)
        {
            try
            {
                _wordbookService.UpdateRegister(model);
                return Ok(_wordbookService.GetKeywords());
            }
            catch
            {
                return Problem();
            }
        }

        [HttpPost("UpdateAnswer")]
        public IActionResult UpdateAnswer(WordBookDto.Update model)
        {
            try
            {
                _wordbookService.UpdateAnswer(model);
                return Ok(_wordbookService.GetKeywordAnswers(model.Id));
            }
            catch
            {
                return Problem();
            }
        }

        [HttpGet("GetRegister")]
        public IActionResult GetRegister()
        {
            try
            {
                return Ok(_wordbookService.GetKeywords());
            }
            catch
            {
                return Problem();
            }
        }

        [HttpGet("GetAnswer")]
        public IActionResult GetAnswer(int id)
        {
            try
            {
                return Ok(_wordbookService.GetKeywordAnswers(id));
            }
            catch
            {
                return Problem();
            }
        }


        [HttpPost("CheckAnswer")]
        public IActionResult CheckAnswer(WordBookDto.CheckAnswer model)
        {
            try
            {
                return Ok(_wordbookService.CheckAnswer(model));
            }
            catch
            {
                return Problem();
            }
        }


    }
}
