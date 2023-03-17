using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Zy.App.Common.Core.DbContextExtension
{
    public class DefaultValueAttributeConvention : PropertyAttributeConventionBase<DefaultValueAttribute>
    {
        public DefaultValueAttributeConvention(ProviderConventionSetBuilderDependencies dependencies)
            : base(dependencies)
        {
        }

        protected override void ProcessPropertyAdded(
            IConventionPropertyBuilder propertyBuilder,
            DefaultValueAttribute attribute,
            MemberInfo clrMember,
            IConventionContext context)
        {
            if (propertyBuilder == null)
            {
                throw new ArgumentNullException(nameof(propertyBuilder));
            }

            if (attribute == null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            if (attribute.Value != null)
            {
                propertyBuilder.HasDefaultValue(attribute.Value ?? DBNull.Value);
            }
        }
    }
}
