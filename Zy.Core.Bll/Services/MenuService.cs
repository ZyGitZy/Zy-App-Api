using AutoMapper;
using Byzan.Biz.Abstractions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.AppExtensions;
using Zy.App.Common.Models;
using Zy.Core.Bll.Interfaces;
using Zy.Core.Bll.Modelus;
using Zy.Core.Dal;
using Zy.Core.Dal.Entitys;

namespace Zy.Core.Bll.Services
{
    public class MenuService : IMenuService
    {
        private readonly IMapper mapper;

        private readonly IZyCoreEntityStore<MenuEntity> zyCoreEntityStore;

        private readonly string errKey = "菜单";

        public MenuService(IMapper mapper, IZyCoreEntityStore<MenuEntity> zyCoreEntityStore)
        {
            this.mapper = mapper;
            this.zyCoreEntityStore = zyCoreEntityStore;
        }

        public async Task<ServiceResult<long>> CreateAsync(MenuBo menuBo)
        {

            if (menuBo.ParentId != 0)
            {
                if (!await this.zyCoreEntityStore.AnyAsync(a => a.ParentId == menuBo.ParentId))
                {
                    return this.NotFound(errKey + "父", menuBo.ParentId);
                }

                if (!menuBo.FullPath.Any(a => menuBo.ParentId == a))
                {
                    menuBo.FullPath = new List<long>();
                    await GetParentIdTree(menuBo.ParentId, menuBo.FullPath);
                }
            }

            var entity = this.mapper.Map<MenuEntity>(menuBo);

            var result = await this.zyCoreEntityStore.CreateSaveAsync(entity);

            return this.Ok(result.Id);
        }

        public async Task<ServiceResult> DeleteAsync(long id)
        {
            var result = await this.zyCoreEntityStore.FindAsync(default, id);

            if (result == null)
            {
                return this.NotFound(this.errKey, id);
            }

            if (await this.zyCoreEntityStore.AnyAsync(a => a.ParentId == id))
            {
                return this.Error("Error", "Error", "存在子菜单不允许被删除");
            }

            await this.zyCoreEntityStore.DeleteSaveAsync(result);

            return this.Ok();
        }

        public async Task<ServiceResult<IEnumerable<MenuBo>>> FindByIdsAsync(long[] ids)
        {
            var entitys = await this.zyCoreEntityStore.FindByIdsAsync(ids);

            var result = this.mapper.Map<IEnumerable<MenuEntity>, IEnumerable<MenuBo>>(entitys);

            return this.Ok(result);
        }

        public async Task<ServiceResult<MenuBo>> GetAsync(long id)
        {
            var result = await this.zyCoreEntityStore.FindAsync(default, id);

            if (result == null)
            {
                return this.NotFound(this.errKey, id).As<MenuBo>();

            }

            return this.Ok(this.mapper.Map<MenuEntity, MenuBo>(result));
        }

        public async Task<ServiceResult<QueryResult<MenuBo>>> QueryAsync(MenuQueryBo menuQueryBo)
        {
            var linq = this.zyCoreEntityStore.Query().WhereLike(w => w.Name, menuQueryBo.Name);

            var result = new QueryResult<MenuBo>(menuQueryBo);

            if (menuQueryBo.EnablePaging && menuQueryBo.Offset == null)
            {
                int count = await linq.CountAsync();
                result.Count = count;
            }

            linq = menuQueryBo.EnableCustomerSort ? linq.OrderByCustomer(menuQueryBo.SortExpression) : linq.OrderByDescending(o => o.LastUpdateDateTime);

            linq = linq.Paging(menuQueryBo);

            result.Items = this.mapper.Map<IEnumerable<MenuEntity>, IEnumerable<MenuBo>>(await linq.ToListAsync());

            return this.Ok(result);
        }

        public async Task<ServiceResult> UpdateAsync(MenuBo menuBo, long id)
        {
            var entity = await this.zyCoreEntityStore.FindAsync(default, id);

            if (entity == null)
            {
                return this.NotFound(this.errKey, id);
            }

            if (menuBo.ParentId != 0)
            {
                if (!await this.zyCoreEntityStore.AnyAsync(a => a.ParentId == menuBo.ParentId))
                {
                    return this.NotFound(errKey + "父", menuBo.ParentId);
                }

                if (!menuBo.FullPath.Any(a => menuBo.ParentId == a))
                {
                    menuBo.FullPath = new List<long>(); // 每次都重新生成菜单 要优化
                    await GetParentIdTree(menuBo.ParentId, menuBo.FullPath);
                }
            }

            this.mapper.Map(menuBo, entity);

            await this.zyCoreEntityStore.SaveChangesAsync();

            return this.Ok();
        }

        private async Task GetParentIdTree(long parentId, List<long> dataSource)
        {
            dataSource.Add(parentId);
            var menus = await this.zyCoreEntityStore.Query().FirstOrDefaultAsync(q => q.FullPath.Contains(parentId.ToString()));
            if (menus != null)
            {
                dataSource.InsertRange(0, Array.ConvertAll(menus.FullPath.Split(','), c => long.Parse(c)));
            }
        }

    }
}
