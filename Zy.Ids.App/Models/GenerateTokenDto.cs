using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zy.Ids.App.Models
{
    public class GenerateTokenDto
    {
        public string Access_token { get; set; } = string.Empty;

        public string Expires_in { get; set; } = string.Empty;

        public string Refresh_token { get; set; } = string.Empty;

        public string Scope { get; set; } = string.Empty;

        public string Token_type { get; set; } = string.Empty;
    }
}
