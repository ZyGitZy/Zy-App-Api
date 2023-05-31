using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Core.Controller;
using Zy.Core.Bll.Interfaces;

namespace Zy.Core.App.Controllers
{
    public class EncourageController: ApiController
    {
        private readonly IEncourageService service;

        public EncourageController(IEncourageService service) 
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> DrawALotteryOrRaffle()
        {
            await this.service.DrawALotteryOrRaffleAsync();
            return this.Ok();
        }
    }
}
