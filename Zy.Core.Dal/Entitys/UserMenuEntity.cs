using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Interfaces;
using Zy.App.Common.Models;

namespace Zy.Core.Dal.Entitys
{
    public class UserMenuEntity : EntityBase, IEntityAdditionColumns
    {
        [Required]
        [DefaultValue(0)]
        public long UserId { get; set; }

        [Required]
        [DefaultValue(0)]
        public long MenuId { get; set; }

        public long CreateByUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public long LastUpdateByUserId { get; set; }
        public DateTime LastUpdateDateTime { get; set; }
    }
}
