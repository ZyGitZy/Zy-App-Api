using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.Core.App.Modules;
using Zy.Core.Bll.Modelus;

namespace Zy.Core.App.Profiles
{
    public class MenuDtoProfile : Profile
    {
        public MenuDtoProfile()
        {
            this.CreateMap<MenuDto, MenuBo>().ReverseMap();
            this.CreateMap<MenuQueryDto, MenuQueryBo>();
        }
    }
}
