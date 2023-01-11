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
    public interface IUserService : IService
    {
        Task<ServiceResult<long>> PostAsync(UserBo userBo);

        Task<ServiceResult> PutAsync(long id, UserBo userBo);

        Task<ServiceResult> UpdatePasswordAsync(long id, string originPassword,string newPassword);

        Task<ServiceResult> DeleteAsync(long id);

        Task<ServiceResult<UserBo>> GetAsync(long id);

        Task<ServiceResult<QueryResult<UserBo>>> QueryAsync(UserQueryBo query);
    }
}
