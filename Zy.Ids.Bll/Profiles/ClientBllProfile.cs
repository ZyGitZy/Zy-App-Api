using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.Ids.Bll.Models;
using Zy.Ids.DAL.Entitys;

namespace Zy.Ids.Bll.Profiles
{
    public class ClientBllProfile : Profile
    {
        public ClientBllProfile()
        {
            this.CreateMap<ClientBo, ClientEntity>();
        }
    }
}
