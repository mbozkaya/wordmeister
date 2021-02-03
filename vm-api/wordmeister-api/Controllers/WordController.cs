using Microsoft.AspNetCore.Mvc;
using wordmeister_api.Dtos.General;
using wordmeister_api.Dtos.Word;
using wordmeister_api.Helpers;
using wordmeister_api.Interfaces;

namespace wordmeister_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    //TODO: swaggerden auth kontrol edilemedigi icin simdilik yorum satırında kalacak.
    [Authorize]
    public class WordController : ControllerBase
    {
        private IWordService _wordService;
        public WordController(IWordService wordService)
        {
            _wordService = wordService;
        }

        [HttpPost("AddWord")]
        public IActionResult AddWord(WordRequest model)
        {
            var response = _wordService.AddWord(model, User.GetUserId());
            var result = new General.ResponseResult() { Data = response };
            return Ok(result);
        }

        [HttpGet("GetWord")]
        public IActionResult GetWord(IdDto model)
        {
            var response = _wordService.GetWord(model.Id, User.GetUserId());

            var result = new General.ResponseResult() { Data = response };

            return Ok(result);
        }

        [HttpGet("GetWords")]
        public IActionResult GetWords(PagingDto.Request model)
        {
            var response = _wordService.GetWords(model.PageCount, model.PageSize, User.GetUserId());

            var result = new General.ResponseResult() { Data = response };

            return Ok(result);
        }

        [HttpPost("DeleteWord")]
        public IActionResult DeleteWord(IdDto model)
        {
            _wordService.DeleteWord(model.Id, User.GetUserId());

            return Ok(new General.ResponseResult());
        }

        [HttpPost("UpdateWord")]
        public IActionResult UpdateWord(WordRequest model)
        {
            _wordService.UpdateWord(model, User.GetUserId());

            return Ok(new General.ResponseResult());
        }

    }
}
