using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Zy.App.Common.Core.Controller
{
    [Authorize]
    [Route("[Controller]")]
    public class ApiController : ControllerBase
    {
    }
}
