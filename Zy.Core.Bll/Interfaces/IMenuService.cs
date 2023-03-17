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
    public interface IMenuService : IService
    {
        Task<ServiceResult<long>> CreateAsync(MenuBo menuBo);

        Task<ServiceResult> UpdateAsync(MenuBo menuBo, long id);

        Task<ServiceResult> DeleteAsync(long id);

        Task<ServiceResult<MenuBo>> GetAsync(long id);

        Task<ServiceResult<QueryResult<MenuBo>>> QueryAsync(MenuQueryBo MenuQueryBo);

        Task<ServiceResult<IEnumerable<MenuBo>>> FindByIdsAsync(long[] ids);
    }
}
