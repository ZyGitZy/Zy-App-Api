using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.AppExtensions;
using Zy.App.Common.Models;
using Zy.Ids.Bll.Models;

namespace Zy.Ids.Bll.Interfaces
{
    public interface IJwtService : IService
    {
        Task<ServiceResult<GenerateTokenBo>> GenerateToken(GenerateTokenQueryBo queryBo);
    }
}
