using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Models;

namespace Zy.Ids.DAL.Entitys
{
    [Table("ZyIds.ClientGrantType")]
    public class ClientGrantTypeEntity : EntityBase
    {
        /// <summary>
        /// 认证类型
        /// </summary>
        [Column(TypeName = ColumnTypes.NVarchar500)]
        [Required]
        [DefaultValue("")]
        public string GrantType { get; set; } = string.Empty;

        /// <summary>
        /// 客户端id
        /// </summary>
        [Required]
        [DefaultValue(0L)]
        public long ClientId { get; set; }
    }
}
