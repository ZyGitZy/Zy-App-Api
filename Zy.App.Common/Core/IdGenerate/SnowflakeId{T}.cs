using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zy.App.Common.Core.IdGenerate
{
    public class SnowflakeId<T>
    {
        private static readonly SnowflakeId SnowflakeIdInstance;

        static SnowflakeId()
        {
            SnowflakeIdInstance = new SnowflakeId();
        }

        public static long NewId()
        {
            return SnowflakeIdInstance.NewId();
        }
    }
}
