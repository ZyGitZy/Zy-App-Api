using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zy.App.Common.AppExtensions
{
    public class IdsBinderAttribute : ModelBinderAttribute
    {
        public IdsBinderAttribute() : base(typeof(CommaDelimeterArrayModelBinder))
        {

        }
    }

    public class CommaDelimeterArrayModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var modelName = bindingContext.ModelName; // 字段名
            var val = bindingContext.ValueProvider.GetValue(modelName);

            if (val == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            var elementType = bindingContext.ModelType.GetElementType();

            try
            {
                if (elementType != null)
                {
                    var converter = TypeDescriptor.GetConverter(elementType);

                    var values = Array.ConvertAll(val.ToString().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries),
                        e => converter.ConvertFromString(e != null ? e.Trim() : string.Empty));

                    var distinctValues = values.Distinct().ToArray();

                    var typeValues = Array.CreateInstance(elementType, distinctValues.Length);

                    distinctValues.CopyTo(typeValues, 0);

                    bindingContext.Result = ModelBindingResult.Success(typeValues);
                }
            }
            catch (Exception e)
            {
                bindingContext.ModelState.AddModelError(modelName, e.Message);
            }

            return Task.CompletedTask;
        }
    }
}
