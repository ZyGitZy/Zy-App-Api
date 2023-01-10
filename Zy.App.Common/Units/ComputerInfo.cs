using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zy.App.Common.Units
{
    public static class ComputerInfo
    {
        /// <summary>
        /// 获取电脑的磁盘列表
        /// </summary>
        /// <returns></returns>
        public static string[] GetComputerDrives()
        {
            return Environment.GetLogicalDrives();
        }
    }
}
