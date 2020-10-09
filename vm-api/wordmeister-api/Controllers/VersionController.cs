using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace wordmeister_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VersionController : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet]
        public string Index() => $"Welcome to WordMeister Api " +
            $"Version:{GetType().Assembly.GetName().Version.ToString()} " +
            $"© 2020 - {DateTime.Now.Year}";
    }
}
