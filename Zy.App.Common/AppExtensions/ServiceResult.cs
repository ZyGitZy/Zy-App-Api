

using Newtonsoft.Json;
using System.Runtime.Serialization;
using Zy.App.Common.Interfaces;
using Zy.App.Common.Models;

namespace Zy.App.Common.AppExtensions
{

    [DataContract]
    public class ServiceResult : IServiceResult
    {
        private readonly JsonSerializerSettings jsonOptions = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        };

        public object? Data { get; set; }

        public IServiceProblemDetails? ProblemDetails { get; set; }

        [DataMember(Order = 2)]
        [JsonIgnore]
        public string ProblemDetailsString
        {
            get => JsonConvert.SerializeObject(this.ProblemDetails, this.jsonOptions);

            set => this.ProblemDetails =
                JsonConvert.DeserializeObject<IServiceProblemDetails>(value, this.jsonOptions) ?? default!;
        }

        [DataMember(Order = 3)]
        public bool Success { get; set; }

        public static implicit operator ServiceResult<long>(ServiceResult serviceResult) => serviceResult.As<long>();

        public static ServiceResult Error(string type, string title = "", string detail = "", Exception? exception = null)
        {
            return new ServiceResult
            {
                Success = false,
                ProblemDetails = new ServiceProblemDetails(type, title, detail, exception)
            };
        }

        public static ServiceResult Error(Error error, Exception? exception = null)
        {
            return Error(error.Type, error.Title, error.Detail, exception);
        }

        public static ServiceResult Ok(object? data = null)
        {
            return new ServiceResult { Success = true, Data = data ?? default! };
        }

        public ServiceResult<T> As<T>()
        {
            var r = new ServiceResult<T>()
            {
                Success = this.Success,
                ProblemDetails = this.ProblemDetails,
                Data = default!
            };

            if (this.Data != null && this.Data is T)
            {
                r.Data = (T)this.Data;
            }

            return r;
        }
    }
}
