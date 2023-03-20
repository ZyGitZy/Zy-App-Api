using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.AppExtensions;
using Zy.Core.Bll.Interfaces;
using Zy.Core.Bll.Modelus;
using Zy.Core.Dal;
using Zy.Core.Dal.Entitys;

namespace Zy.Core.Bll.Services
{
    public class RoleMenuService : IRoleMenuService
    {
        private readonly IMapper mapper;

        private readonly string menuErrTitle = "菜单";

        private readonly string roleErrTitle = "角色";

        private IZyCoreEntityStore<MenuEntity> zyCoreEntityStore;

        private IZyCoreEntityStore<RoleMenuEntity> zyRoleMenuStore;

        public RoleMenuService(IMapper mapper, IZyCoreEntityStore<MenuEntity> zyCoreEntityStore,
            IZyCoreEntityStore<RoleMenuEntity> zyRoleMenuStore)
        {
            this.mapper = mapper;
            this.zyCoreEntityStore = zyCoreEntityStore;
            this.zyRoleMenuStore = zyRoleMenuStore;
        }

        public async Task<ServiceResult<long>> CreateAsync(RoleMenuBo roleBo)
        {
            var menuEntity = await this.zyCoreEntityStore.FindAsync(keyValues: roleBo.MenuId);

            if (menuEntity == null)
            {
                return this.NotFound(this.menuErrTitle, roleBo.MenuId);
            }

            var roleEntity = await this.zyCoreEntityStore.FindAsync(keyValues: roleBo.RoleId);

            if (roleEntity == null)
            {
                return this.NotFound(this.roleErrTitle, roleBo.RoleId);
            }

            var roleMenuEntity = await this.zyRoleMenuStore.AnyAsync(a => roleBo.MenuId == a.MenuId && a.RoleId == roleBo.RoleId);

            if (roleMenuEntity)
            {
                return this.Error("Duplicate", "菜单重复", "角色下已存在该菜单");
            }

            var entity = this.mapper.Map<RoleMenuBo, RoleMenuEntity>(roleBo);

            var result = await this.zyRoleMenuStore.CreateSaveAsync(entity);

            return this.Ok(result.Id);
        }

        public async Task<ServiceResult<IEnumerable<MenuBo>>> GetMenuByRoleAsync(long roleId)
        {
            var role = this.zyRoleMenuStore.Query(q => q.RoleId == roleId);

            var menu = role.Join(this.zyCoreEntityStore.Query(), inner => inner.MenuId, on => on.Id, (roleMenu, menu) => menu);

            var list = await menu.ToListAsync();

            var result = this.mapper.Map<IEnumerable<MenuEntity>, IEnumerable<MenuBo>>(list);

            return this.Ok(result);
        }
    }
}
