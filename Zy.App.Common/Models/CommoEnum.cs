using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zy.App.Common.Models
{
    public enum DeletedDataQueryTypes
    {
        All,
        OnlyDeleted,
        OnlyUnDeleted
    }

    public enum ByzanBetweenOptions
    {
        /// <summary>
        /// 开区间
        /// </summary>
        OpenInterval = 0,

        /// <summary>
        /// 左开右闭
        /// </summary>
        LeftSideOpenInterval,

        /// <summary>
        /// 左闭右开
        /// </summary>
        RightSideOpenInterval,

        /// <summary>
        /// 闭区间
        /// </summary>
        ClosedInterval,
    }
}
