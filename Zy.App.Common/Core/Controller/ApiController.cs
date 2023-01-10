using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Zy.App.Common.Core.Controller
{
    [Route("[Controller]")]
    [Authorize]
    public class ApiController : ControllerBase
    {
    }
}
