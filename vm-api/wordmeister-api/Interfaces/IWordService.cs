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
        WordResponse GetWord(long wordId,int userId);
        PageResponse GetWords(int skipRows, int pageSize, int userId);
        ResponseResult AddWord(WordRequest model, int userId);
        void UpdateWord(WordRequest model, int userId);
        void DeleteWord(long wordId, int userId);
    }
}
