using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.AppExtensions;
using Zy.App.Common.Models;
using Zy.User.Bll.Models;

namespace Zy.User.Bll.Interfaces
{
    public interface IUserRoleService : IService
    {
        Task<ServiceResult<List<UserRoleBo>>> GetUserRoleByUserId(long id);
    }
}
