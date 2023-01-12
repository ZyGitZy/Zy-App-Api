using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.User.Bll.Models;
using Zy.User.DAL.Entitys;

namespace Zy.User.Bll.Profiles
{
    public class UserBoProfile : Profile
    {
        public UserBoProfile()
        {
            this.CreateMap<UserBo, UserEntity>().ReverseMap();
        }
    }
}
