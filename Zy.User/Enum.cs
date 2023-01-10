using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zy.User
{
    public enum ActiveStatus
    {
        /// <summary>
        /// 新建
        /// </summary>
        New = 1,

        /// <summary>
        /// 启用状态
        /// </summary>
        Active,

        /// <summary>
        /// 停用状态
        /// </summary>
        Inactive,
    }
}
