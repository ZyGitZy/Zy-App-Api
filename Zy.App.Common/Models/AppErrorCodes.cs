using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.AppExtensions;

namespace Zy.App.Common.Models
{
    public class AppErrorCodes
    {
        public const string ModelInvalidTitle = "请求无效";
        public const string ModelInvalidType = "ModelInvalid";

        public const string Length = "Length|请求无效|{0}长度必须是{1}位至{2}位";

        public const string MaxLength = "MaxLength|请求无效|{0}长度不能超过{1}位";

        public const string NotEmpty = "NotEmpty|请求无效|{0}不能为空";

        public const string Reqired = "Reqired|请求无效|{0}必须输入";

        public static readonly Error ModelJsonDeserializeException = new(
            "Model.JsonDeserialize.Exception",
            ModelInvalidTitle,
            "Json反序列化错误");

        public static readonly Error ModelUnhandledException = new("Model.UnhandledException", ModelInvalidTitle, "服务异常");

        public static readonly Error ModelUnknownError = new("Model.UnknownError", ModelInvalidTitle, "未知错误");

        public static readonly Error UnhandledException = new("UnhandledException", "未处理异常");

        public static readonly Error UpdateConcurrency = new("UpdateConcurrency", "修改发生冲突");
    }
}
