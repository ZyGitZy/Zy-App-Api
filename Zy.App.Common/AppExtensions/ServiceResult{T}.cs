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
    public class ServiceResult<T> : IServiceResult
    {
        private readonly JsonSerializerSettings jsonOptions = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        };

        [DataMember(Order = 1)]
        public T Data { get; set; } = default!;

        [DataMember(Order = 2)]
        public bool Success { get; set; }

        public IServiceProblemDetails? ProblemDetails { get; set; }

        [DataMember(Order = 3)]
        [JsonIgnore]
        public string? ProblemDetailsString
        {
            get => JsonConvert.SerializeObject(this.ProblemDetails, this.jsonOptions);

            set => this.ProblemDetails =
                JsonConvert.DeserializeObject<IServiceProblemDetails>(value ?? string.Empty, this.jsonOptions);
        }

        object? IServiceResult.Data
        {
            get => this.Data;
            set => this.Data = (T)(value ?? default!);
        }

        public static ServiceResult<T> Error(Error error, Exception? exception = null)
        {
            return Error(error.Type, error.Title, error.Detail, exception);
        }

        public static ServiceResult<T> Error(string type, string? title = "", string? detail = "", Exception? exception = null)
        {
            return new ServiceResult<T>
            {
                Success = false,
                ProblemDetails = new ServiceProblemDetails(type, title, detail, exception)
            };
        }

        public static ServiceResult<T> Ok(T data)
        {
            return new ServiceResult<T> { Success = true, Data = data };
        }

        public static ServiceResult<TData> Ok<TData>(TData data)
        {
            return new ServiceResult<TData> { Success = true, Data = data };
        }
    }
}
