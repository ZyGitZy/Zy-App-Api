using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.Ids.App.Models;
using Zy.Ids.Bll.Models;

namespace Zy.Ids.App.Profiles
{
    public class ClientAppProfile : Profile
    {
        public ClientAppProfile()
        {
            this.CreateMap<ClientDto, ClientBo>().ReverseMap();
        }
    }
}
