

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

        public object? TData { get; set; }

        public IErrorDetail? ErrDetail { get; set; }

        public bool Success { get; set; }

        [DataMember(Order = 2)]
        [JsonIgnore]
        public string ErrDetailString
        {
            get => JsonConvert.SerializeObject(this.ErrDetail, this.jsonOptions);

            set => this.ErrDetail =
                JsonConvert.DeserializeObject<IErrorDetail>(value, this.jsonOptions) ?? default!;
        }

        public static implicit operator ServiceResult<long>(ServiceResult serviceResult) => serviceResult.As<long>();

        public static ServiceResult Error(string type, string title = "", string detail = "")
        {
            return new ServiceResult
            {
                Success = false,
                ErrDetail = new ErrorDetail(type, title, detail)
            };
        }

        public static ServiceResult Error(Error error)
        {
            return Error(error.Type, error.Title, error.Detail);
        }

        public static ServiceResult Ok(object? data = null)
        {
            return new ServiceResult { Success = true, TData = data ?? default! };
        }

        public ServiceResult<T> As<T>()
        {
            var r = new ServiceResult<T>()
            {
                Success = this.Success,
                ErrDetail = this.ErrDetail,
                TData = default!
            };

            if (this.TData != null && this.TData is T)
            {
                r.TData = (T)this.TData;
            }

            return r;
        }
    }
}
