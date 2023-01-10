using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Interfaces;

namespace Zy.User.DAL.Entitys
{
    public class UserClaimEntity : IdentityUserClaim<long>, IEntity
    {
    }
}
