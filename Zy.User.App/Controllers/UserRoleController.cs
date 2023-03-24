using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Core.Controller;
using Zy.User.App.Models;
using Zy.User.Bll.Interfaces;
using Zy.User.Bll.Models;

namespace Zy.User.App.Controllers
{
    public class UserRoleController : ApiController
    {
        private readonly IMapper mapper;

        private readonly IUserRoleService service;

        public UserRoleController(IMapper mapper, IUserRoleService service)
        {
            this.mapper = mapper;
            this.service = service;
        }

        [HttpGet("{id:long}/UserId")]
        public async Task<IActionResult> GetUserRoleByUserId(long id)
        {
            var result = await this.service.GetUserRoleByUserId(id);

            return this.Result(result, e => this.mapper.Map<IEnumerable<UserRoleBo>, IEnumerable<UserRoleDto>>(e));
        }
    }
}
