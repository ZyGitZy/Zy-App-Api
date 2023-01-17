using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zy.App.Common.AppExtensions
{
    public static class MySqlDbFunctions
    {
        [DbFunction("JSON_EXTRACT", "")]
        public static string JsonExtract(string column, [NotParameterized] string path)
        {
            throw new NotSupportedException();
        }

        [DbFunction("JSON_CONTAINS", "")]
        public static string JsonContains(string column, [NotParameterized] string path)
        {
            throw new NotSupportedException();
        }

        [DbFunction("JSON_UNQUOTE", "")]
        public static string JsonUnQuote(string column)
        {
            throw new NotSupportedException();
        }

        [DbFunction("JSON_OVERLAPS", "")]
        public static string JsonOverlaps(string column, [NotParameterized] string jsonArrayString)
        {
            throw new NotSupportedException();
        }
    }
}
