using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zy.App.Common.Core.IdGenerate
{
    public class IdGenerator : IIdGenerator
    {
        private static readonly IIdGenerator Default;

        private static IIdGenerator _instance;

        static IdGenerator()
        {
            Default = new IdGenerator();
            _instance = Default;
        }

        public static IIdGenerator Instance
        {
            get => _instance;

            set => _instance = value ?? Default;
        }

        public static long NewId()
        {
            return Instance.NewId();
        }

        public static long NewId<T>()
        {
            return Instance.NewId<T>();
        }

        long IIdGenerator.NewId()
        {
            return SnowflakeId<object>.NewId();
        }

        long IIdGenerator.NewId<T>()
        {
            return SnowflakeId<T>.NewId();
        }
    }
}
