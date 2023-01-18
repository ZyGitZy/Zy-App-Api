using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Interfaces;
using Zy.App.Common.Models;

namespace Zy.App.Common.AppExtensions
{
    [DataContract]
    public class ValidationProblemDetails : ServiceProblemDetails, IServiceProblemDetails
    {
        public ValidationProblemDetails()
        {
        }

        public ValidationProblemDetails(ModelStateDictionary modelState)
            : this()
        {
            if (modelState == null)
            {
                throw new ArgumentNullException(nameof(modelState));
            }

            var errorList = new List<(string Key, Error Error)>();

            foreach (var modelStateItem in modelState)
            {
                if (modelState.ValidationState != ModelValidationState.Invalid)
                {
                    continue;
                }
                var key = modelStateItem.Key;
                var errors = modelStateItem.Value.Errors;
                if (errors.Count == 0)
                {
                    continue;
                }

                var errorMessages = new string[errors.Count];

                for (var i = 0; i < errors.Count; i++)
                {
                    var modelError = errors[i];

                    Error apiError = AppErrorCodes.ModelUnknownError;

                    var modelErrorMessage = modelError.ErrorMessage;
                    if (!string.IsNullOrWhiteSpace(modelErrorMessage))
                    {
                        apiError = this.ParseToError(modelErrorMessage);
                    }
                    else if (modelError.Exception != null)
                    {
                        if (modelError.Exception is JsonReaderException)
                        {
                            apiError = AppErrorCodes.ModelJsonDeserializeException;
                        }
                        else
                        {
                            apiError = AppErrorCodes.ModelUnhandledException;
                        }
                    }

                    if (apiError.Type == null)
                    {
                        apiError.Type = AppErrorCodes.ModelUnknownError.Type;
                    }

                    errorMessages[i] = string.IsNullOrWhiteSpace(apiError.Detail) ? apiError.Title : apiError.Detail;
                    errorList.Add((key, apiError));
                }
                this.Errors.Add(key, errorMessages);
            }

            if (errorList.Count == 0)
            {
                errorList.Add((string.Empty, AppErrorCodes.ModelUnknownError));
            }

            this.Type = AppErrorCodes.ModelInvalidType;
            this.Title = AppErrorCodes.ModelInvalidTitle;
            if (errorList.Count == 1)
            {
                var firstError = errorList[0];
                if (!string.IsNullOrWhiteSpace(firstError.Error.Type) && firstError.Error.Type != AppErrorCodes.ModelInvalidType)
                {
                    this.Type = $"{AppErrorCodes.ModelInvalidType}.{firstError.Error.Type}";
                }

                if (!string.IsNullOrWhiteSpace(firstError.Error.Title))
                {
                    this.Title = firstError.Error.Title;
                }
                this.Detail = string.IsNullOrWhiteSpace(firstError.Error.Detail) ? firstError.Error.Title : firstError.Error.Detail;
            }
            else
            {
                var detail = string.Join("\r\n", this.Errors.Values.SelectMany(m => m));
                this.Type = $"{AppErrorCodes.ModelInvalidType}.Multiple";
                this.Detail = detail;
            }
        }

        public IDictionary<string, string[]> Errors { get; } = new Dictionary<string, string[]>(StringComparer.Ordinal);

        private Error ParseToError(string errorMessage)
        {
            var items = errorMessage.Split(Error.Split);
            string type = AppErrorCodes.ModelInvalidType;
            string title = AppErrorCodes.ModelInvalidTitle;
            string detail;
            if (items.Length == 1)
            {
                detail = items[0];
            }
            else if (items.Length == 2)
            {
                type = items[0];
                detail = items[1];
            }
            else
            {
                type = items[0];
                title = items[1];
                detail = items[2];
            }
            return new Error(type, title, detail);
        }

    }
}
