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
    [Table("ZyIds.PersistedGrant")]
    public class PersistedGrantEntity : EntityBase
    {
        [Column(TypeName = ColumnTypes.NVarchar200)]
        [Required]
        [DefaultValue("")]
        public string Key { get; set; } = string.Empty;

        [Column(TypeName = ColumnTypes.NVarchar100)]
        [Required]
        [DefaultValue("")]
        public string Type { get; set; } = string.Empty;

        [Column(TypeName = ColumnTypes.NVarchar200)]
        [Required]
        [DefaultValue("")]
        public string SubjectId { get; set; } = string.Empty;

        /// <summary>
        /// 客户端id
        /// </summary>
        [Column(TypeName = ColumnTypes.NVarchar200)]
        [Required]
        [DefaultValue("")]
        public string ClientId { get; set; } = string.Empty;

        [Required]
        [DefaultValue(typeof(DateTime), "0001-01-01")]
        public DateTime CreationTime { get; set; }

        [DefaultValue(typeof(DateTime), "0001-01-01")]
        public DateTime? Expiration { get; set; }

        [Column(TypeName = ColumnTypes.Remark)]
        [Required]
        [DefaultValue("")]
        public string Data { get; set; } = string.Empty;
    }
}
