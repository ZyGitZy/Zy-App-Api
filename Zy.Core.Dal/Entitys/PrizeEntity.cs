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
    public class PrizeEntity : EntityBase, IEntityAdditionColumns
    {
        [Required]
        [DefaultValue(0D)]
        public double Price { get; set; }

        [Required]
        [DefaultValue(0L)]
        public long UnitId { get; set; }

        [Required]
        [DefaultValue(0L)]
        [Column(TypeName = ColumnTypes.Name)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [DefaultValue("")]
        [Column(TypeName = ColumnTypes.Name)]
        public string Brand { get; set; } = string.Empty;

        [Required]
        [DefaultValue("")]
        [Column(TypeName = ColumnTypes.No)]
        public string No { get; set; } = string.Empty;


        [DefaultValue(0L)]
        [Required]
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
