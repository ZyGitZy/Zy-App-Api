using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Models;

namespace Zy.Core.Bll.Interfaces
{
    public interface IEncourageService : IService
    {
        Task DrawALotteryOrRaffleAsync();
    }
}
