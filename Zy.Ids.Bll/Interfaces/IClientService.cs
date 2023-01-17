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
    public interface IClientService : IService
    {
        Task<ServiceResult<long>> PostAsync(ClientBo clientBo);

        Task<ServiceResult> PutAsync(long id, ClientBo clientBo);

        Task<ServiceResult> DeleteAsync(long id, ClientBo clientBo);

        Task<ServiceResult<ClientBo>> GetAsync(long id);
    }
}
