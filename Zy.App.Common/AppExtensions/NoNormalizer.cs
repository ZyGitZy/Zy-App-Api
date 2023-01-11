using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Interfaces;

namespace Zy.App.Common.AppExtensions
{
    public class NoNormalizer : INoNormalizer
    {
        public string Normalize(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }

            return value.ToUpper().Trim().Replace(" ", "");
        }
    }
}
