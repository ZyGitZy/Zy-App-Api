using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.AppExtensions;
using Zy.App.Common.Interfaces;
using Zy.App.Common.Models;
using Zy.App.Common.StoreCore;
using Zy.User.Bll.Interfaces;
using Zy.User.Bll.Models;
using Zy.User.Dal;
using Zy.User.DAL.Entitys;

namespace Zy.User.Bll.Services
{
    public class UserService : IUserService
    {
        private readonly IPasswordHasher<UserEntity> _passwordHasher;
        private readonly UserManager<UserEntity> userManager;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly UserEntityStore<UserEntity> userEntityStore;
        private readonly INoNormalizer noNormalizer;
        private readonly IMapper mapper;
        private readonly string userTemp = "用户";
        private readonly IZyAppContext zyAppContext;

        public UserService(UserEntityStore<UserEntity> userEntityStore, SignInManager<UserEntity> _signInManager, UserManager<UserEntity> userManager, IZyAppContext zyAppContext, INoNormalizer noNormalizer, IMapper mapper, IPasswordHasher<UserEntity> _passwordHasher)
        {
            this._passwordHasher = _passwordHasher;
            this.noNormalizer = noNormalizer;
            this.mapper = mapper;
            this.zyAppContext = zyAppContext;
            this.userManager = userManager;
            this.userEntityStore = userEntityStore;
            this._signInManager = _signInManager;
        }

        public async Task<ServiceResult> DeleteAsync(long id)
        {
            var userEntity = await this.userManager.FindByIdAsync(id.ToString());

            if (userEntity == null)
            {
                return this.NotFound(userTemp, id.ToString());
            }

            if (userEntity.Status != ActiveStatus.New)
            {
                return this.Error("非新建用户不能删除");
            }

            userEntity.LastUpdateDateTime = DateTime.Now;
            userEntity.LastUpdateByUserId = this.zyAppContext.UserId;

            await this.userManager.DeleteAsync(userEntity);
            return this.Ok();
        }

        public async Task<ServiceResult<UserBo>> GetAsync(long id)
        {
            var userEntity = await this.userManager.FindByIdAsync(id.ToString());
            if (userEntity == null)
            {
                return this.NotFound(userTemp, id.ToString()).As<UserBo>();
            }

            var userBo = this.mapper.Map<UserEntity, UserBo>(userEntity);

            return this.Ok(userBo);
        }

        public async Task<ServiceResult<long>> PostAsync(UserBo userBo)
        {
            var userEntity = this.mapper.Map<UserEntity>(userBo);

            userEntity.PasswordHash = this._passwordHasher.HashPassword(userEntity, userBo.Password);

            userEntity.NormalizedUserName = this.noNormalizer.Normalize(userEntity.UserName);

            userEntity.NormalizedEmail = this.noNormalizer.Normalize(userEntity.Email);

            userEntity.Status = ActiveStatus.Active;

            if (string.IsNullOrWhiteSpace(userEntity.LockoutEnd.ToString()))
            {
                userEntity.LockoutEnd = DateTime.Now;
            }

            if (await IsExistsNo(userEntity.No) && await IsExistsNo(userEntity.No.ToUpper()))
            {
                return this.NoDuplicate(userTemp, userEntity.No);
            }

            userEntity.CreateDateTime = DateTime.Now;
            userEntity.CreateByUserId = this.zyAppContext.UserId;
            userEntity.LastUpdateByUserId = this.zyAppContext.UserId;
            userEntity.LastUpdateDateTime = DateTime.Now;

            var result = await this.userManager.CreateAsync(userEntity);

            if (result.Succeeded)
            {
                var resultUser = await this.userManager.FindByNameAsync(userEntity.UserName);
                return resultUser == null ? this.NotFound(userTemp, userEntity.UserName) : this.Ok(resultUser.Id);
            }

            var err = result.Errors.FirstOrDefault();

            if (err != null)
            {
                return this.Error(err.Code, err.Description);
            }

            return this.Error("创建用户发生异常");
        }

        public async Task<ServiceResult<QueryResult<UserBo>>> QueryAsync(UserQueryBo query)
        {
            var linq = this.userEntityStore.Query()
                .WhereLike(w => w.UserName == query.UserName, !string.IsNullOrWhiteSpace(query.UserName))
                .WhereLike(w => w.No == query.No, !string.IsNullOrWhiteSpace(query.No));

            var queryResult = new QueryResult<UserBo>(query);
            if (query.EnablePaging && query.Offset == null)
            {
                int? count = await linq.CountAsync();
                queryResult.Count = count;
            }

            linq = query.IsSortable() ? linq.OrderByCustomer(query.SortExpression) : linq.OrderByDescending(o => o.LastUpdateDateTime);

            var source = await linq.ToListAsync();

            queryResult.Items = this.mapper.Map<IEnumerable<UserEntity>, IEnumerable<UserBo>>(source);

            return this.Ok(queryResult);
        }

        private async Task<bool> IsExistsNo(string no)
        {
            if (!string.IsNullOrWhiteSpace(no))
            {
                return false;
            }
            return await this.userEntityStore.Query(q => q.No == no).AnyAsync();
        }

        public async Task<ServiceResult> PutAsync(long id, UserBo userBo)
        {
            var userEntity = await this.userManager.FindByIdAsync(id.ToString());

            if (userEntity == null)
            {
                return this.NotFound(userTemp, id.ToString());
            }

            var orinalNo = userEntity.No;

            this.mapper.Map(userBo, userEntity);

            if (!string.IsNullOrWhiteSpace(userEntity.No) && orinalNo != userEntity.No)
            {
                if (await IsExistsNo(userEntity.No))
                {
                    return this.NoDuplicate(userTemp, userEntity.No);
                }
            }

            userEntity.LastUpdateByUserId = this.zyAppContext.UserId;
            userEntity.LastUpdateDateTime = DateTime.Now;

            await this.userManager.UpdateAsync(userEntity);

            return this.Ok();
        }

        public async Task<ServiceResult> UpdatePasswordAsync(long id, string originPassword, string newPassword)
        {
            var user = await this.userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                return this.NotFound(userTemp, id.ToString());
            }

            if (this._passwordHasher.VerifyHashedPassword(user, user.PasswordHash, originPassword) == PasswordVerificationResult.Failed)
            {
                return this.Error("用户原密码错误");
            }

            user.PasswordHash = this._passwordHasher.HashPassword(user, newPassword);
            user.LastUpdateByUserId = this.zyAppContext.UserId;
            user.LastUpdateDateTime = DateTime.Now;

            await this.userManager.UpdateAsync(user);
            return this.Ok();
        }
    }
}
