using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    [AllowAnonymous]
    public class UserController : ApiController
    {
        private readonly IMapper mapper;
        private readonly IUserService userService;

        public UserController(IMapper mapper, IUserService userService)
        {
            this.mapper = mapper;
            this.userService = userService;
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await this.userService.DeleteAsync(id);

            return this.Result(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserDto userDto)
        {
            var bo = this.mapper.Map<UserDto, UserBo>(userDto);

            var result = await this.userService.PostAsync(bo);

            return this.Result(result);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Put(long id, [FromBody] UserDto userDto)
        {
            var bo = this.mapper.Map<UserDto, UserBo>(userDto);

            var result = await this.userService.PutAsync(id, bo);

            return this.Result(result);
        }

        [HttpPut("{id:long}/ChangePassword")]
        public async Task<IActionResult> ChangePassword(long id, [FromBody] ChangePasswordDto dto)
        {
            var result = await this.userService.UpdatePasswordAsync(id, dto.OriginPassword, dto.NewPassword);

            return this.Result(result);
        }

        [HttpGet]
        public async Task<IActionResult> Query([FromQuery] UserQueryDto queryDto)
        {
            var queryBo = this.mapper.Map<UserQueryDto, UserQueryBo>(queryDto);

            var result = await this.userService.QueryAsync(queryBo);

            return this.Result<UserBo, UserDto>(result, this.mapper);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> Get(long id)
        {
            var result = await this.userService.GetAsync(id);

            return this.Result(result);
        }

    }
}
