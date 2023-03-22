using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Core.Controller;
using Zy.Core.App.Modules;
using Zy.Core.Bll.Interfaces;
using Zy.Core.Bll.Modelus;

namespace Zy.Core.App.Controllers
{
    public class MenuRoleController : ApiController
    {
        private readonly IMapper mapper;

        private readonly IRoleMenuService service;

        public MenuRoleController(IMapper mapper, IRoleMenuService service)
        {
            this.mapper = mapper;
            this.service = service;
        }

        [HttpGet]
        [Route("Role/{id:long}")]
        public async Task<IActionResult> GetMenuByRoleId(long id)
        {
            var result = await this.service.GetMenuByRoleAsync(id);

            return this.Result(result, e => this.mapper.Map<IEnumerable<MenuBo>, IEnumerable<MenuDto>>(e));
        }
    }

}
