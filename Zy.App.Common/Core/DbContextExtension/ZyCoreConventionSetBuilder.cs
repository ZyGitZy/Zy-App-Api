using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zy.App.Common.Core.DbContextExtension
{
    public class ZyCoreConventionSetBuilder : IConventionSetPlugin
    {
        private readonly ProviderConventionSetBuilderDependencies _dependencies;

        public ZyCoreConventionSetBuilder(ProviderConventionSetBuilderDependencies dependencies)
        {
            this._dependencies = dependencies;
        }

        public ConventionSet ModifyConventions(ConventionSet conventionSet)
        {
            var defaultValueConvention = new DefaultValueAttributeConvention(this._dependencies);
            conventionSet.PropertyAddedConventions.Add(defaultValueConvention);
            conventionSet.PropertyFieldChangedConventions.Add(defaultValueConvention);
            return conventionSet;
        }
    }
}
