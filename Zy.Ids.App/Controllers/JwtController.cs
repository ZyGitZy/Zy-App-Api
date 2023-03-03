using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Core.Controller;
using Zy.Ids.App.Models;
using Zy.Ids.Bll.Interfaces;
using Zy.Ids.Bll.Models;

namespace Zy.Ids.App.Controllers
{
    [Route("connect/token")]
    [AllowAnonymous]
    public class JwtController : ApiController
    {
        private IJwtService jwtService;

        private IMapper mapper;

        public JwtController(IJwtService jwtService, IMapper mapper)
        {
            this.jwtService = jwtService;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> GenerateToken([FromForm] GenerateTokenQueryDto login)
        {
            var bo = this.mapper.Map<GenerateTokenQueryDto, GenerateTokenQueryBo>(login);
            var result = await this.jwtService.GenerateToken(bo);
            return this.Result(result);
        }

    }
}
