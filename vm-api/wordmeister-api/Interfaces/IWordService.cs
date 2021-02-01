using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wordmeister_api.Dtos.Word;
using static wordmeister_api.Dtos.General.General;

namespace wordmeister_api.Interfaces
{
   public interface IWordService
    {
        WordResponse GetWord(long wordId);
        PageResponse GetWords(int skipRows, int pageSize);
        ResponseResult AddWord(WordRequest model);
        bool UpdateWord(WordRequest model);
        bool DeleteWord(long wordId);
    }
}
