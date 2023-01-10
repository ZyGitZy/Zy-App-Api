using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.AppExtensions;

namespace Zy.Video.Bll.Models
{
    public static class VideoErrorCode
    {
        public static class Common
        {
            public static Error FileNotFound = new("NotFount", "文件不存在！");
        }
    }
}
