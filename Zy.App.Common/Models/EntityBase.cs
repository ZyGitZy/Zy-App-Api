using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Interfaces;

namespace Zy.App.Common.Models
{
    public class EntityBase : IEntity<long>, IEntityLogicDelete, IEntityVersion
    {
        [Timestamp]
        public virtual int? RowVersion { get; set; }

        public virtual bool IsDeleted { get; set; }

        [Key]
        public virtual long Id { get; set; }
    }
}
