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
    [Table("ZyIds.ClientSecret")]
    public class ClientSecretEntity: EntityBase
    {
        /// <summary>
        /// 备注
        /// </summary>
        [Column(TypeName = ColumnTypes.Description)]
        [Required]
        [DefaultValue("")]
        public string Description { get; set; }

        /// <summary>
        /// 原始值
        /// </summary>
        [Column(TypeName = ColumnTypes.Remark)]
        [Required]
        [DefaultValue("")]
        public string RawValue { get; set; }

        /// <summary>
        /// 加密值
        /// </summary>
        [Column(TypeName = ColumnTypes.Remark)]
        [Required]
        [DefaultValue("")]
        public string Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        [DefaultValue(typeof(DateTime), "0001-01-01")]
        public DateTime Expiration { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = ColumnTypes.NVarchar500)]
        [Required]
        [DefaultValue("")]
        public string Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        [DefaultValue(typeof(DateTime), "0001-01-01")]
        public DateTime Created { get; set; }

        /// <summary>
        /// 客户端id
        /// </summary>
        [Required]
        [DefaultValue(0L)]
        public long ClientId { get; set; }
    }
}
