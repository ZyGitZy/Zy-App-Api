using AutoMapper;
using IdentityServer4.Models;
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
    [Route("[controller]")]
    [AllowAnonymous]
    public class ClientController : ApiController
    {
        private readonly IMapper mapper;

        private readonly IClientService clientService;

        public ClientController(IMapper mapper, IClientService clientService)
        {
            this.mapper = mapper;
            this.clientService = clientService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ClientDto clientDto)
        {
            var bo = this.mapper.Map<ClientDto, ClientBo>(clientDto);
            bo.ClientUri = $"{this.Request.Scheme}://{this.Request.Host}/.well-known/openid-configuration";
            var result = await this.clientService.PostAsync(bo);

            return this.Result(result);
        }

        [HttpDelete("{id:long}")]

        public async Task<IActionResult> Delete(long id, [FromBody] ClientDto clientDto)
        {
            var bo = this.mapper.Map<ClientDto, ClientBo>(clientDto);

            var result = await this.clientService.DeleteAsync(id, bo);

            return this.Ok(result);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> Get(long id)
        {
            var result = await this.clientService.GetAsync(id);

            return this.Result(result);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Put(long id, [FromBody] ClientDto clientDto)
        {
            var bo = this.mapper.Map<ClientDto, ClientBo>(clientDto);

            var result = await this.clientService.PutAsync(id, bo);

            return this.Result(result);
        }

    }
}
