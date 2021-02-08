using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wordmeister_api.Dtos.Account
{
    public class AccountRequest
    {
        
    }

    public class AccountResponse
    {
        public class Information
        {
            public string Firstname { get; set; }
            public string Lastname { get; set; }
            public string Email { get; set; }
            public string PictureUri { get; set; }
        }
    }
}
