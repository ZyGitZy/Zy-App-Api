using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zy.App.Common.Core.IdGenerate
{
    public class SnowflakeId
    {
        private readonly object locker = new object();

        private readonly SnowflakeOptions options;

        private long _lastTimestamp = -1L;

        private long _sequence;

        public SnowflakeId()
        {
            this.options = SnowflakeOptions.Default;
        }

        public SnowflakeId(SnowflakeOptions options)
        {
            this.options = options;
        }

        /// <summary>
        ///     获取新的ID
        /// </summary>
        /// <returns>新的ID</returns>
        public long NewId()
        {
            lock (this.locker)
            {
                var timestamp = TimeGen();
                if (this._lastTimestamp == timestamp)
                {
                    // 同一微妙中生成ID
                    this._sequence = (this._sequence + 1) & this.options.SequenceMask; // 用&运算计算该微秒内产生的计数是否已经到达上限
                    if (this._sequence == 0)
                    {
                        timestamp = TillNextMillis(this._lastTimestamp);
                    }
                }
                else
                {
                    // 不同微秒生成ID 计数清0
                    this._sequence = 0;
                }

                if (timestamp < this._lastTimestamp)
                {
                    throw new Exception(
                        string.Format(
                            "Clock moved backwards.  Refusing to generate id for {0} milliseconds",
                            this._lastTimestamp - timestamp));
                }

                // 把当前时间戳保存为最后生成ID的时间戳
                this._lastTimestamp = timestamp;
#pragma warning disable CS0675 // Bitwise-or operator used on a sign-extended operand
                return ((timestamp - this.options.BaseTimestamp) << this.options.TimestampLeftShift)
                    | (this.options.WorkerId << this.options.WorkerIdShift)
                    | this._sequence;
#pragma warning restore CS0675 // Bitwise-or operator used on a sign-extended operand
            }
        }


        /// <summary>
        ///     获取下一微秒时间戳
        /// </summary>
        /// <param name="lastTimestamp">时间戳</param>
        /// <returns>下一微秒时间戳</returns>
        private static long TillNextMillis(long lastTimestamp)
        {
            var timestamp = TimeGen();
            while (timestamp <= lastTimestamp)
            {
                timestamp = TimeGen();
            }

            return timestamp;
        }

        /// <summary>
        ///     获取当前时间戳
        /// </summary>
        /// <returns>当前时间戳</returns>
        private static long TimeGen()
        {
            return (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
        }
    }
}
