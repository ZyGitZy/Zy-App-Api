using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Models;

namespace Zy.App.Common.Interfaces
{
    public interface IEntityLabels
    {
        [Column(TypeName = ColumnTypes.Json)]
        string? Labels { get; set; }
    }
}
