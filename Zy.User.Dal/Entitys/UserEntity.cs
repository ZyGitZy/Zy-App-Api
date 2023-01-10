using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Interfaces;
using Zy.App.Common.Models;
using Zy.User;

namespace Zy.User.DAL.Entitys
{
    public class UserEntity : IdentityUser<long>, IEntityAdditionColumns, IEntity<long>
    {
        [DefaultValue((ActiveStatus)0)]
        [Required]
        public ActiveStatus Status { get; set; }

        [Required]
        [DefaultValue("")]
        [Column(TypeName = ColumnTypes.No)]
        public string No { get; set; } = string.Empty;

        [Required]
        [DefaultValue(0L)]
        public long CreateByUserId { get; set; }

        [Required]
        [DefaultValue(typeof(DateTime), "0001-01-01")]
        public DateTime CreateDateTime { get; set; }

        [Required]
        [DefaultValue(0L)]
        public long LastUpdateByUserId { get; set; }

        [Required]
        [DefaultValue(typeof(DateTime), "0001-01-01")]
        public DateTime LastUpdateDateTime { get; set; }
    }
}
