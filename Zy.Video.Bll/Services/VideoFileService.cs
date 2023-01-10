using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.AppExtensions;
using Zy.App.Common.Units;
using Zy.Video.Bll.Interfaces;
using Zy.Video.Bll.Models;
using Zy.Video.Enum;
using SharpCompress.Common;
using SharpCompress.Readers;
using ICSharpCode.SharpZipLib.Zip;

namespace Zy.Video.Bll.Services
{
    public class VideoFileService : IVideoFileService
    {
        public async Task<ServiceResult<VideoFileInfoBo>> DecompressionFile(string path, string password = "")
        {
            if (!File.Exists(path))
            {
                return VideoErrorCode.Common.FileNotFound.As<VideoFileInfoBo>();
            }

            try
            {
                using Stream stream = File.OpenRead(path);

                var option = new ReaderOptions
                {
                    ArchiveEncoding = new ArchiveEncoding
                    {
                        Default = Encoding.UTF8
                    }

                };

                if (!string.IsNullOrWhiteSpace(password))
                {
                    option.Password = password;
                }

                var reader = ReaderFactory.Open(stream, option);
                string dirPath = Directory.GetParent(path)!.FullName;
                string savePath = Path.Combine(dirPath, Path.GetFileName(path).Replace(Path.GetExtension(path), ""));
                if (Directory.Exists(savePath))
                {
                    return await this.GetFileList(savePath);
                }

                Directory.CreateDirectory(savePath);

                while (reader.MoveToNextEntry())
                {
                    if (reader.Entry.IsDirectory)
                    {
                        Directory.CreateDirectory(Path.Combine(savePath, reader.Entry.Key));
                    }
                    else
                    {
                        var file = Path.Combine(savePath, reader.Entry.Key);
                        reader.WriteEntryToFile(file);
                    }
                }

                var result = await this.GetFileList(savePath);
                return result;
            }
            catch (Exception ex)
            {
                return this.Error<VideoFileInfoBo>("error", ex.Message);
            }

        }


        public async Task<ServiceResult<VideoFileInfoBo>> GetFileList(string path)
        {
            var result = new VideoFileInfoBo();
            var videos = new List<VideoFileBo>();

            GetParentInfo(path, result);

            if (string.IsNullOrWhiteSpace(path))
            {
                await this.AddComputerDrives(ComputerInfo.GetComputerDrives(), videos);
                result.FileInfos = videos;
                return this.Ok(result);
            }

            if (!Directory.Exists(path) && !File.Exists(path))
            {
                return VideoErrorCode.Common.FileNotFound.As<VideoFileInfoBo>();
            }

            DirectoryInfo directoryInfo = new(path);

            await this.AddDirectoryInfos(directoryInfo.GetDirectories(), videos, FileType.Directory);
            await this.AddFileInfos(directoryInfo.GetFiles(), videos, FileType.File);

            result.FileInfos = videos;

            return this.Ok(result);
        }

        public async Task<ServiceResult<FileStream>> DownLoadFile(string path)
        {
            await Task.FromResult(0);

            if (!File.Exists(path))
            {
                return VideoErrorCode.Common.FileNotFound.As<FileStream>();
            }

            var stream = File.OpenRead(path);

            return this.Ok(stream);
        }

        private async Task<List<VideoFileBo>> AddComputerDrives(string[] path, List<VideoFileBo> videos)
        {
            await Task.FromResult(0);
            foreach (var item in path)
            {
                DirectoryInfo directoryInfo = new(item);
                videos.Add(new VideoFileBo
                {
                    Name = directoryInfo.Name,
                    Path = item,
                    ExtendName = null,
                    FileCount = directoryInfo.GetDirectories().Length + directoryInfo.GetFiles().Length,
                    IsCompressed = false,
                    FileType = FileType.Disk,
                    IsCrypted = false
                });
            }

            return videos;
        }


        private async Task<List<VideoFileBo>> AddFileInfos(FileInfo[] fileList, List<VideoFileBo> videos, FileType fileType)
        {
            await Task.FromResult(0);
            foreach (var file in fileList)
            {
                FileAttributes attributes = file.Attributes;

                if (attributes.HasFlag(FileAttributes.Hidden) || !IsFileOpen(file))
                {
                    continue;
                }

                string path = file.FullName;

                bool isCompressed = attributes.HasFlag(FileAttributes.Compressed);

                bool IsCrypted = await this.FileIsCrypted(isCompressed, path);

                FileExtendName fileExtendName = FileExtendName.other;

                fileExtendName = this.GetFileExtendName(path);


                videos.Add(new VideoFileBo
                {
                    Name = file.Name,
                    Path = path,
                    ExtendName = fileExtendName,
                    FileCount = 0,
                    IsCompressed = isCompressed,
                    FileType = fileType,
                    IsCrypted = IsCrypted
                });
            }

            return videos;
        }

        /// <summary>
        /// 获取目录信息
        /// </summary>
        /// <param name="fileList"></param>
        /// <param name="path"></param>
        /// <param name="videos"></param>
        /// <returns></returns>
        private async Task<List<VideoFileBo>> AddDirectoryInfos(DirectoryInfo[] dirInfos, List<VideoFileBo> videos, FileType fileType)
        {
            await Task.FromResult(0);
            foreach (var dirInfo in dirInfos)
            {
                FileAttributes attributes = dirInfo.Attributes;
                if (attributes.HasFlag(FileAttributes.Hidden) || !IsDirOpen(dirInfo))
                {
                    continue;
                }

                var direCount = dirInfo.GetDirectories().Count(w => !w.Attributes.HasFlag(FileAttributes.Hidden) && IsDirOpen(w));
                var fileCount = dirInfo.GetFiles().Count(w => !w.Attributes.HasFlag(FileAttributes.Hidden) && IsFileOpen(w));

                videos.Add(new VideoFileBo
                {
                    Name = dirInfo.Name,
                    Path = dirInfo.FullName,
                    ExtendName = null,
                    FileCount = direCount + fileCount,
                    IsCompressed = false,
                    FileType = fileType,
                    IsCrypted = false
                });
            }

            return videos;
        }


        private void GetParentInfo(string path, VideoFileInfoBo info)
        {
            if (!string.IsNullOrEmpty(path))
            {
                DirectoryInfo directoryInfo = new(path);
                info.PreDirName = directoryInfo.Name;
                info.PrePath = "/";
                if (directoryInfo.Parent != null)
                {
                    info.PrePath = directoryInfo.Parent.FullName;
                }
            }
        }

        /// <summary>
        /// 获取文件扩展名
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>

        private FileExtendName GetFileExtendName(string path)
        {
            try
            {
                using FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
                using BinaryReader reader = new BinaryReader(stream);
                string fileclass = "";
                for (int i = 0; i < 2; i++)
                {
                    fileclass += reader.ReadByte().ToString();
                }
                switch (fileclass)
                {
                    case "255216":
                    case "7173":
                    case "13780":
                        return FileExtendName.jpg;
                    case "8075":
                        if (path.EndsWith("docx"))
                        {
                            return FileExtendName.doc;
                        }
                        else if (path.EndsWith("xlsx"))
                        {
                            return FileExtendName.xls;
                        }
                        else if (path.EndsWith("zip"))
                        {
                            return FileExtendName.zip;
                        }
                        else
                        {
                            return FileExtendName.ppt;
                        }
                    case "208207":
                        if (path.EndsWith("doc") || path.EndsWith("wps"))
                        {
                            return FileExtendName.doc;
                        }
                        else if (path.EndsWith("xls"))
                        {
                            return FileExtendName.xls;
                        }
                        else
                        {
                            return FileExtendName.ppt;
                        }
                    case "4946":
                    case "104116":
                    case "5150":
                    case "239187":
                        return FileExtendName.txt;
                    case "8297":
                        return FileExtendName.zip;
                    case "7790":
                        return FileExtendName.exe;
                    case "3780":
                        return FileExtendName.pdf;
                    default:
                        return FileExtendName.other;
                }
            }
            catch (Exception)
            {
                return FileExtendName.other;
            }
        }

        /// <summary>
        /// 判断是否加密
        /// </summary>
        /// <param name="isCompressed"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        private async Task<bool> FileIsCrypted(bool isCompressed, string path)
        {
            await Task.FromResult(0);
            if (isCompressed)
            {
                using FileStream fileStreamIn = new FileStream(path, FileMode.Open, FileAccess.Read);
                using ZipInputStream zipInStream = new ZipInputStream(fileStreamIn);
                ZipEntry entry = zipInStream.GetNextEntry();
                return entry.IsCrypted;
            }

            return false;
        }

        /// <summary>
        /// 文件是否打开
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private bool IsOccupied(string filePath)
        {
            FileStream? stream = null;
            try
            {
                stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None);
                return false;
            }
            catch
            {
                return true;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
        }

        private bool IsDirOpen(DirectoryInfo directoryInfo)
        {
            try
            {
                directoryInfo.GetDirectories().FirstOrDefault();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool IsFileOpen(FileInfo fileInfo)
        {
            try
            {
                fileInfo.OpenRead();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
