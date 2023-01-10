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
    [Table("ZyIds.ClientScope")]
    public class ClientScopeEntity : EntityBase
    {
        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = ColumnTypes.NVarchar200)]
        [Required]
        [DefaultValue("")]
        public string Scope { get; set; }

        /// <summary>
        /// 客户端id
        /// </summary>
        [Required]
        [DefaultValue(0L)]
        public long ClientId { get; set; }

    }
}
