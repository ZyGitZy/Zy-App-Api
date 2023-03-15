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

namespace Zy.Core.Dal.Entitys
{
    public class MenuEntity : EntityBase
    {
        [Required]
        [Column(TypeName = ColumnTypes.Name)]
        [DefaultValue("")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = ColumnTypes.NVarchar2000)]
        [DefaultValue("")]
        public string Path { get; set; } = string.Empty;

        [Required]
        [DefaultValue(0)]
        public int Sort { get; set; }

        [Required]
        [DefaultValue(0)]
        public long ParentId { get; set; }

        [Required]
        [Column(TypeName = ColumnTypes.Remark)]
        [DefaultValue("")]
        public string FullPath { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = ColumnTypes.Name)]
        [DefaultValue("")]
        public string IconName { get; set; } = string.Empty;
    }
}
