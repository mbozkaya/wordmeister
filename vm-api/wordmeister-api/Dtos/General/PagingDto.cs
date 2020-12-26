using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wordmeister_api.Dtos.General
{
    public class PagingDto
    {
        public PagingDto()
        {
            PageCount = 1;
            PageSize = 50;
            Order = string.Empty;
            OrderBy = string.Empty;
        }
        public int PageCount
        {
            get
            {
                return pageCount;
            }
            set
            {
                pageCount = (value - 1) < 0 ? 0 : value - 1;
            }
        }
        private int pageCount { get; set; }
        public int PageSize { get; set; }
        public string OrderBy { get; set; }
        public string Order { get; set; }
    }
}
