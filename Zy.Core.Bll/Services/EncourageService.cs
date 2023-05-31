using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.Core.Bll.Interfaces;

namespace Zy.Core.Bll.Services
{
    public class EncourageService : IEncourageService
    {

        public EncourageService() 
        {
        
        }

        public Task DrawALotteryOrRaffleAsync()
        {
            throw new NotImplementedException();
        }



    }
}
