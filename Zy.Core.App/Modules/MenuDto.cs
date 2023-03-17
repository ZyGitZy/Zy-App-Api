using System.ComponentModel.DataAnnotations;
using Zy.App.Common.Models;

namespace Zy.Core.App.Modules
{
    public class MenuDto : ResourceDto
    {
        [Required(ErrorMessage = AppErrorCodes.Reqired)]
        public string Name { get; set; } = string.Empty;

        public string Path { get; set; } = string.Empty;

        public int Sort { get; set; }

        public long ParentId { get; set; }

        public List<long> FullPath { get; set; } = new List<long>();

        public string IconName { get; set; } = string.Empty;
    }
}
