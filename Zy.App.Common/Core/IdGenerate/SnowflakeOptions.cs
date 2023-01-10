using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zy.App.Common.Core.IdGenerate
{
    public class SnowflakeOptions
    {
        private ushort _workerId;

        public SnowflakeOptions(ushort workerId, int workerIdBits = 10, int sequenceBits = 12, int baseTimeStamp = 0)
        {
            this.WorkerIdBits = workerIdBits;
            this.SequenceBits = sequenceBits;
            this.BaseTimestamp = baseTimeStamp <= 0 ? GetTimestamp(new DateTime(DateTime.Now.Year, 1, 1, 0, 0, 0)) : baseTimeStamp;
            this.MaxWorkerId = (ushort)(-1L ^ (-1L << this.WorkerIdBits));
            this.SequenceMask = -1L ^ (-1L << this.SequenceBits);
            this.WorkerId = workerId > 0
                                ? workerId
                                : (ushort)new Random(DateTime.Now.Millisecond).Next(1, this.MaxWorkerId);

            this.TimestampLeftShift = this.SequenceBits + this.WorkerIdBits;
            this.WorkerIdShift = this.SequenceBits;
        }

        public static SnowflakeOptions Default = new SnowflakeOptions(1);

        /// <summary>
        ///     初始基准时间戳，小于当前时间点即可
        ///     分布式项目请保持此时间戳一致
        /// </summary>
        public long BaseTimestamp { get; }

        /// <summary>
        ///     最大机器ID所占的位数
        /// </summary>
        public ushort MaxWorkerId { get; }

        /// <summary>
        ///     计数器字节数，10个字节用来保存计数码
        /// </summary>
        public int SequenceBits { get; } = 12;

        /// <summary>
        ///     一微秒内可以产生计数，如果达到该值则等到下一微妙在进行生成
        /// </summary>
        public long SequenceMask { get; }

        /// <summary>
        ///     时间戳左移动位数就是机器码和计数器总字节数
        /// </summary>
        public int TimestampLeftShift { get; }

        /// <summary>
        ///     机器编号
        /// </summary>
        public ushort WorkerId
        {
            get => this._workerId;
            set
            {
                if (value > 0 && value < this.MaxWorkerId)
                {
                    this._workerId = value;
                }
                else
                {
                    throw new Exception("Workerid must be greater than 0 or less than " + this.MaxWorkerId);
                }
            }
        }

        /// <summary>
        ///     机器码字节数。10个字节用来保存机器码(定义为Long类型会出现，最大偏移64位，所以左移64位没有意义)
        /// </summary>
        public int WorkerIdBits { get; }

        /// <summary>
        ///     机器码数据左移位数，就是后面计数器占用的位数
        /// </summary>
        public int WorkerIdShift { get; }

        /// <summary>
        ///     获取当前时间戳
        /// </summary>
        /// <param name="dateTime">指定时间</param>
        /// <returns>当前时间戳</returns>
        public static long GetTimestamp(DateTime dateTime)
        {
            return (long)(dateTime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))
                .TotalMilliseconds;
        }
    }
}
