using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Core.Controller;

namespace Zy.Ids.App.Controllers
{
    [Route(".well-known/openid-configuration")]
    [AllowAnonymous]
    public class JwtConfigController : ApiController
    {
        [HttpGet]
        public object GetConfig()
        {
            return new { token_endpoint = $"{this.Request.Scheme}://{this.Request.Host}/connect/token" };
        }
    }
}
