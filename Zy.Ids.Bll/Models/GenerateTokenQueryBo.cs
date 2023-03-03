using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zy.Ids.Bll.Models
{
    public class GenerateTokenQueryBo
    {
        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public bool Offline_access { get; set; } = true;
    }
}
