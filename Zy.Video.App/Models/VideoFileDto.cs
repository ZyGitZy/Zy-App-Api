using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.Video.Enum;

namespace Zy.Video.App.Models
{
    public class VideoFileDto
    {
        public string Name { get; set; } = string.Empty;

        public FileType FileType { get; set; }

        public string Path { get; set; } = string.Empty;

        public bool IsCompressed { get; set; }

        public bool IsCrypted { get; set; }

        public int FileCount { get; set; }

        public FileExtendName ExtendName { get; set; }
    }

    public class VideoFileInfoDto
    {
        public string PrePath { get; set; } = string.Empty;

        public string PreDirName { get; set; } = string.Empty;

        public List<VideoFileDto> FileInfos { get; set; } = new List<VideoFileDto>();
    }
}
