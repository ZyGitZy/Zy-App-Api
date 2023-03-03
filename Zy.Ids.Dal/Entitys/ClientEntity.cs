using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.PortableExecutable;
using Zy.App.Common.Models;
using Zy.App.Common.Interfaces;

namespace Zy.Ids.DAL.Entitys
{
    [Table("ZyIds.Client")]
    public class ClientEntity : EntityBase, IEntityAdditionColumns
    {

        /// <summary>
        ///
        /// </summary>
        [Required]
        [DefaultValue(0)]
        public int ConsentLifetime { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Required]
        [DefaultValue(0)]
        public int AbsoluteRefreshTokenLifetime { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Required]
        [DefaultValue(0)]
        public int SlidingRefreshTokenLifetime { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Required]
        [DefaultValue(0)]
        public int RefreshTokenUsage { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Required]
        [DefaultValue(false)]
        public bool UpdateAccessTokenClaimsOnRefresh { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Required]
        [DefaultValue(0)]
        public int RefreshTokenExpiration { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Required]
        [DefaultValue(0)]
        public int AccessTokenType { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Required]
        [DefaultValue(false)]
        public bool EnableLocalLogin { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Required]
        [DefaultValue(false)]
        public bool IncludeJwtId { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Required]
        [DefaultValue(0)]
        public int AuthorizationCodeLifetime { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Required]
        [DefaultValue(0)]
        public int IdentityTokenLifetime { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Required]
        [DefaultValue(0)]
        public int AccessTokenLifetime { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Column(TypeName = ColumnTypes.NVarchar2000)]
        [Required]
        [DefaultValue("")]
        public string ClientUri { get; set; } = string.Empty;

        /// <summary>
        ///
        /// </summary>
        [Required]
        [DefaultValue(false)]
        public bool RequireClientSecret { get; set; }

        /// <summary>
        /// 是否允许刷新token
        /// </summary>
        [Required]
        [DefaultValue(true)]
        public bool AllowOfflineAccess { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool AllowAccessTokensViaBrowser { get; set; }


        [Required]
        [DefaultValue(false)]
        public bool RequireConsent { get; set; }

        [Required]
        [DefaultValue("")]
        [Column(TypeName = ColumnTypes.NVarchar200)]
        public string ClientClaimsPrefix { get; set; } = string.Empty;


        /// <summary>
        ///
        /// </summary>
        [Column(TypeName = ColumnTypes.NVarchar200)]
        [Required]
        [DefaultValue("")]
        public string ClientName { get; set; } = string.Empty;

        /// <summary>
        /// 客户端编号
        /// </summary>
        [Column(TypeName = ColumnTypes.NVarchar200)]
        [Required]
        [DefaultValue("")]
        public string ClientId { get; set; } = string.Empty;

        public long CreateByUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public long LastUpdateByUserId { get; set; }
        public DateTime LastUpdateDateTime { get; set; }
    }
}
