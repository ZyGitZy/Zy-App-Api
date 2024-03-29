﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.User.App.Models;
using Zy.User.Bll.Models;

namespace Zy.User.App.Profiles
{
    public class UserRoleDtoProfile : Profile
    {
        public UserRoleDtoProfile()
        {
            this.CreateMap<UserRoleBo, UserRoleDto>().ReverseMap();
        }
    }
}
