using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.AppExtensions;
using Zy.App.Common.Core.AppAbstractions.AppAbstractionsOptions;
using Zy.App.Common.Core.Controller;
using Zy.Core.App.Modules;
using Zy.Core.Bll.Interfaces;
using Zy.Core.Bll.Modelus;

namespace Zy.Core.App.Controllers
{
    public class MenuController : ApiController
    {
        private readonly IMapper mapper;

        public readonly IMenuService service;

        public MenuController(IMapper mapper, IMenuService service)
        {
            this.mapper = mapper;
            this.service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] MenuDto menuDto)
        {
            var bo = this.mapper.Map<MenuDto, MenuBo>(menuDto);
            var result = await this.service.CreateAsync(bo);
            return this.Result(result);
        }

        [HttpDelete]
        [Route("{id:long}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var result = await this.service.DeleteAsync(id);
            return this.Result(result);
        }

        [HttpPut]
        [Route("{id:long}")]
        public async Task<IActionResult> PutAsync(long id, [FromBody] MenuDto menuDto)
        {
            var bo = this.mapper.Map<MenuDto, MenuBo>(menuDto);
            var result = await this.service.UpdateAsync(bo, id);
            return this.Result(result);
        }

        [HttpGet]
        public async Task<IActionResult> QueryAsync([FromQuery] MenuQueryDto menuQueryDto)
        {
            var queryBo = this.mapper.Map<MenuQueryDto, MenuQueryBo>(menuQueryDto);

            var result = await this.service.QueryAsync(queryBo);

            return this.Result<MenuBo, MenuDto>(result, this.mapper);
        }

        [HttpGet]
        [Route("{id:long}")]
        public async Task<IActionResult> GetAsync(long id)
        {
            var result = await this.service.GetAsync(id);
            return this.Result(result);
        }

        [HttpGet("{ids}")]
        public async Task<IActionResult> FindByIdsAsync([IdsBinder] long[] ids)
        {
            var result = await this.service.FindByIdsAsync(ids);
            return this.Result(result);
        }
    }
}
