using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.AppExtensions;
using Zy.App.Common.Models;
using Zy.Core.Bll.Modelus;

namespace Zy.Core.Bll.Interfaces
{
    public interface IRoleMenuService : IService
    {
        Task<ServiceResult<IEnumerable<MenuBo>>> GetMenuByRoleAsync(long roleId);

        Task<ServiceResult<long>> CreateAsync(RoleMenuBo roleBo);
    }
}
