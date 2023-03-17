using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Core.AppAbstractions.IAppAbstractionsOptions;
using Zy.App.Common.Core.HealthCheckExtensions.HealthCheckExtensionOption;

namespace Zy.App.Common.Core.DbContextExtension.ZyDbContextOptions
{
    public class ZyDbContextOption
    {
        /// <summary>
        /// 服务器
        /// </summary>
        public string HostName { get; set; } = string.Empty;

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; } = 0;

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// 数据库名
        /// </summary>
        public string DatabaseName { get; set; } = string.Empty;

        /// <summary>
        /// 连接字符串其它设置
        /// 参考 https://www.connectionstrings.com/mysql/
        /// </summary>
        public string ConnectionParam { get; set; } = string.Empty;

        /// <summary>
        /// Migrations Assembly
        /// </summary>
        public string? MigrationsAssembly { get; set; }

        /// <summary>
        /// 是否使用数据库连接池
        /// </summary>
        public bool? UseConnectionPool { get; set; }

        /// <summary>
        /// 数据库连接池的连接数量
        /// </summary>
        public int? ConnectionPoolSize { get; set; }

        public ZyHealthCheckOptions HealthCheck { get; set; } = new ZyHealthCheckOptions();

        public string GetConnectionString()
        {
            return this.GetConnectionString(this.DatabaseName);
        }

        public string GetConnectionString(string database)
        {
            var connectionBuilder = new StringBuilder();

            connectionBuilder.Append($"server={this.HostName};userid={this.UserName};password={this.Password};");

            if (!string.IsNullOrWhiteSpace(database))
            {
                connectionBuilder.Append($"database={database};");
            }

            if (this.Port > 0)
            {
                connectionBuilder.Append($"port={this.Port};");
            }

            if (!string.IsNullOrWhiteSpace(this.ConnectionParam))
            {
                connectionBuilder.Append(this.ConnectionParam);
            }

            return connectionBuilder.ToString();
        }

        public void Apply(ZyDbContextOption source)
        {
            if (source == null)
            {
                return;
            }

            this.HostName = source.HostName;
            this.Port = source.Port;
            this.UserName = source.UserName;
            this.Password = source.Password;
            this.DatabaseName = source.DatabaseName;
            this.ConnectionParam = source.ConnectionParam;
            this.ConnectionPoolSize = source.ConnectionPoolSize;
            this.UseConnectionPool = source.UseConnectionPool;
            this.UseConnectionPool = source.UseConnectionPool;
            this.MigrationsAssembly = source.MigrationsAssembly;
            this.HealthCheck.Apply(source.HealthCheck);
        }
    }
}
