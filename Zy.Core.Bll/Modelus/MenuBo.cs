using Zy.App.Common.Models;

namespace Zy.Core.Bll.Modelus
{
    public class MenuBo : ResourceBo
    {
        public string Name { get; set; } = string.Empty;

        public string Path { get; set; } = string.Empty;

        public int Sort { get; set; }

        public long ParentId { get; set; }

        public List<long> FullPath { get; set; } = new List<long>();

        public string IconName { get; set; } = string.Empty;
    }
}
