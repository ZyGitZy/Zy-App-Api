using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.AppExtensions;
using Zy.User.Bll.Interfaces;
using Zy.User.Bll.Models;
using Zy.User.Dal;
using Zy.User.DAL.Entitys;

namespace Zy.User.Bll.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IMapper mapper;

        private readonly UserEntityStore<UserRoleEntity> userEntityStore;
        private readonly UserEntityStore<UserEntity> userStore;

        public UserRoleService(IMapper mapper, UserEntityStore<UserRoleEntity> userEntityStore, UserEntityStore<UserEntity> userStore)
        {
            this.mapper = mapper;
            this.userStore = userStore;
            this.userEntityStore = userEntityStore;
        }

        public async Task<ServiceResult<List<UserRoleBo>>> GetUserRoleByUserId(long id)
        {
            var linq = await (
              from user in this.userStore.Query(q => q.Id == id)
              join userRole in this.userEntityStore.Query()
              on user.Id equals userRole.UserId
              select new UserRoleBo
              {
                  Id = userRole.Id,
                  UserId = userRole.UserId,
                  RoleId = userRole.RoleId
              }).ToListAsync();

            return this.Ok(linq);
        }
    }
}
