using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zy.App.Common.Interfaces
{
    public interface IBusinessObject<TKey>: IBusinessObject
    {
        TKey Id { get; set; }
    }
}
