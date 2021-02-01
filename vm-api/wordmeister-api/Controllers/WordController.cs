using Microsoft.AspNetCore.Mvc;
using wordmeister_api.Dtos.General;
using wordmeister_api.Dtos.Word;
using wordmeister_api.Helpers;
using wordmeister_api.Interfaces;

namespace wordmeister_api.Controllers
{
    [Route("wordApi/[controller]")]
    [ApiController]
    //TODO: swaggerden auth kontrol edilemedigi icin simdilik yorum satırında kalacak.
    //[Authorize]
    public class WordController : ControllerBase
    {
        private IWordService _wordService;
        public WordController(IWordService wordService)
        {
            _wordService = wordService;
        }

        [HttpPost("addWord")]
        public IActionResult AddWord(WordRequest model)
        {
            var response = _wordService.AddWord(model);

            if (response == null)
                return BadRequest(new { message = "Word Insertion successful!" });

            var result = new General.ResponseResult() { Data = response };
            return Ok(result);
        }

        [HttpGet("getWord")]
        public IActionResult GetWord(long? wordId)
        {
            if (wordId.IsNullOrDefault())
                return BadRequest(new {message = "Word is invalid or you don't have permission." });

            var response = _wordService.GetWord((long)wordId);

            if (response == null)
                return BadRequest(new { message = "Word not found." });

            var result = new General.ResponseResult() { Data = response };

            return Ok(result);
        }

        [HttpGet("getWords")]
        public IActionResult GetWords(int skipRows, int pageSize)
        {
            if (pageSize.IsNullOrDefault())
                return BadRequest(new { message = "Pagination parameter(s) error, try again." });

            var response = _wordService.GetWords(skipRows, pageSize);

            if (response == null)
                return BadRequest(new { message = "Words not found." });

            var result = new General.ResponseResult() { Data = response };

            return Ok(result);
        }

        [HttpPost("deleteWord")]
        public IActionResult DeleteWord(long? wordId)
        {
            if (wordId.IsNullOrDefault())
                return BadRequest(new { message = "Word is invalid or you don't have permission." });

            var response = _wordService.DeleteWord((long)wordId);

            if (response.IsNullOrDefault())
                return BadRequest(new { message = "Word not found." });

            var result = new General.ResponseResult() { Data = response };

            return Ok(result);
        }

        [HttpPost("updateWord")]
        public IActionResult UpdateWord(WordRequest model)
        {
            if (model.IsNullOrDefault() || model.Id.IsNullOrDefault())
                return BadRequest(new { message = "Word is invalid." });

            var response = _wordService.UpdateWord(model);

            if (response.IsNullOrDefault())
                return BadRequest(new { message = "Word not found." });

            var result = new General.ResponseResult() { Data = response };

            return Ok(result);
        }

    }
}
