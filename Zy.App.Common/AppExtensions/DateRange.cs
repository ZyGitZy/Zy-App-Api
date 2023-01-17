using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zy.App.Common.AppExtensions
{
    public struct DateRange
    {

        public DateRange(DateTime value)
        {
            this.From = value;
            this.To = value;
        }

        public DateRange(DateTime from, DateTime to)
        {
            this.From = from;
            this.To = to;
        }

        public DateTime From { get; }

        public DateTime To { get; }

        public static bool operator ==(DateRange left, DateRange right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(DateRange left, DateRange right)
        {
            return !Equals(left, right);
        }

        public override int GetHashCode()
        {
            return this.From.GetHashCode() + this.To.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            return this.Equals((DateRange)obj);
        }

        private bool Equals(DateRange dateRange)
        {
            return dateRange.From.ToUniversalTime() == this.From.ToUniversalTime() && dateRange.To.ToUniversalTime() == this.To.ToUniversalTime();
        }
    }
}
