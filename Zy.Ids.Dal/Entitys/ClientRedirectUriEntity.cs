using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Models;

namespace Zy.Ids.DAL.Entitys
{
    [Table("ZyIds.ClientRedirectUri")]
    public class ClientRedirectUriEntity : EntityBase
    {
        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = ColumnTypes.NVarchar2000)]
        [Required]
        [DefaultValue("")]
        public string RedirectUri { get; set; } = string.Empty;

        /// <summary>
        /// 客户端id
        /// </summary>
        [Required]
        [DefaultValue(0L)]
        public long ClientId { get; set; }
    }
}
