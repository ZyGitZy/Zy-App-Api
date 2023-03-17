using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Core.Controller;

namespace Zy.Core.App.Controllers
{
    public class MenuController : ApiController
    {
        public MenuController()
        {

        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync()
        {

        }

        [HttpGet]
        public async Task<IActionResult> QueryAsync() 
        {
        
        }
    }
}
