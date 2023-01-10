using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Core.Controller;
using Zy.Video.App.Models;
using Zy.Video.Bll.Interfaces;
using Zy.Video.Bll.Models;

namespace Zy.Video.App.Controllers
{
    public class VideoFileController : ApiController
    {
        IVideoFileService service;
        IMapper mapper;

        public VideoFileController(IVideoFileService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetFileList(string path)
        {
            var result = await this.service.GetFileList(path);
            return this.Result<VideoFileInfoBo, VideoFileInfoDto>(result, mapper);
        }

        [HttpGet("DecompressionFile")]
        public async Task<IActionResult> DecompressionFile(string path, string password)
        {
            var result = await this.service.DecompressionFile(path, password);

            return this.Result<VideoFileInfoBo, VideoFileInfoDto>(result, mapper);
        }

        [HttpGet("DownloadFile")]
        public async Task<IActionResult> DownloadFile(string path)
        {
            var result = await this.service.DownLoadFile(path);
            if (result.Success && result.TData != null)
            {
                string fileName = result.TData.Name;
                if (!string.IsNullOrWhiteSpace(fileName))
                {
                    fileName = fileName.Substring(fileName.LastIndexOf("\\") + 1);
                }
                return this.File(result.TData, "application/octet-stream", fileName);
            }

            return this.Fail(result.ErrDetail?.Title ?? "", result.ErrDetail?.Message ?? "");
        }
    }
}
