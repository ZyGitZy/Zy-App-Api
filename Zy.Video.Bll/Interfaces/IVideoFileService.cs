using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.AppExtensions;
using Zy.App.Common.Models;
using Zy.Video.Bll.Models;

namespace Zy.Video.Bll.Interfaces
{
    public interface IVideoFileService : IService
    {
        Task<ServiceResult<VideoFileInfoBo>> GetFileList(string path);

        Task<ServiceResult<FileStream>> DownLoadFile(string path);

        Task<ServiceResult<VideoFileInfoBo>> DecompressionFile(string path, string password = "");
    }
}
