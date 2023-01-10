using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.Video.Enum;

namespace Zy.Video.Bll.Models
{
    public class VideoFileBo
    {
        public string Name { get; set; } = string.Empty;

        public FileType FileType { get; set; }

        public string Path { get; set; } = string.Empty;

        public bool IsCompressed { get; set; }

        public bool IsCrypted { get; set; }

        public int FileCount { get; set; }

        public FileExtendName? ExtendName { get; set; }
    }

    public class VideoFileInfoBo
    {
        public string PrePath { get; set; } = string.Empty;

        public string PreDirName { get; set; } = string.Empty;

        public List<VideoFileBo> FileInfos { get; set; } = new List<VideoFileBo>();
    }
}
