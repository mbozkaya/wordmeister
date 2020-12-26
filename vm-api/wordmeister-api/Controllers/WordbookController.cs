using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using wordmeister_api.Dtos.General;
using wordmeister_api.Dtos.WordBook;
using wordmeister_api.Interfaces;

namespace wordmeister_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
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
                return Ok(new General.ResponseResult { Error = false });
            }
            catch
            {
                return Ok(new General.ResponseResult { Error = true });
            }
        }

        [HttpPost("CreateAnswer")]
        public IActionResult CreateAnswer(WordBookDto.CreateAnswer model)
        {
            try
            {
                _wordbookService.CreateAnswer(model);
                return Ok(new General.ResponseResult { Error = false });
            }
            catch
            {
                return Ok(new General.ResponseResult { Error = true });
            }
        }

        [HttpPost("UpdateRegister")]
        public IActionResult UpdateRegister(WordBookDto.Update model)
        {
            try
            {
                _wordbookService.UpdateRegister(model);
                return Ok(new General.ResponseResult { Error = false });
            }
            catch
            {
                return Ok(new General.ResponseResult { Error = true });
            }
        }

        [HttpPost("UpdateAnswer")]
        public IActionResult UpdateAnswer(WordBookDto.Update model)
        {
            try
            {
                _wordbookService.UpdateAnswer(model);
                return Ok(new General.ResponseResult { Error = false });
            }
            catch
            {
                return Ok(new General.ResponseResult { Error = true });
            }
        }

        [HttpGet("GetRegister")]
        public IActionResult GetRegister()
        {
            try
            {
                return Ok(new General.ResponseResult { Data = _wordbookService.GetKeywords() });
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

        [HttpDelete("RemoveRegister")]
        public IActionResult RemoveRegister(List<int> Ids)
        {
            try
            {
                foreach (var id in Ids)
                {
                    _wordbookService.RemoveRegister(id);
                }

                return Ok(new General.ResponseResult());
            }
            catch (Exception)
            {
                return Ok(new General.ResponseResult { Error = true });
            }
        }

        [HttpDelete("RemoveAnswer")]
        public IActionResult RemoveAnswer(WordBookDto.Delete model)
        {
            try
            {
                foreach (var id in model.Id)
                {
                    _wordbookService.RemoveAnswer(id);
                }

                return Ok(new General.ResponseResult());
            }
            catch (Exception)
            {
                return Ok(new General.ResponseResult { Error = true });
            }
        }

    }
}
