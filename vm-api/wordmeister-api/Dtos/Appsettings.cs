using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wordmeister_api.Dtos
{
    public class Appsettings
    {
        public string Secret { get; set; }
        public string AESSecret { get; set; }
        public SlackSettings Slack { get; set; }
    }

    public class SlackSettings
    {
        public string WebHookUrl { get; set; }
    }
}
