using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Bing.Knife4jUI
{
    /// <summary>
    /// Knife4jUI 中间件
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class Knife4jUIMiddleware
    {
        /// <summary>
        /// 操作
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// 选项配置
        /// </summary>
        private readonly Knife4jUIOptions _options;

        /// <summary>
        /// 静态文件中间件
        /// </summary>
        private readonly StaticFileMiddleware _staticFileMiddleware;

        /// <summary>
        /// Json序列化设置
        /// </summary>
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public Knife4jUIMiddleware(
            RequestDelegate next,
            IHostingEnvironment hostingEnvironment,
            ILoggerFactory loggerFactory,
            Knife4jUIOptions options)
        {
            _next = next;
            _options = options ?? new Knife4jUIOptions();
            _jsonSerializerOptions = new JsonSerializerOptions();
            _jsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            _jsonSerializerOptions.IgnoreNullValues = true;
            _jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, false));
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var httpMethod = httpContext.Request.Method;
            var path = httpContext.Request.Path.Value;

            if (httpMethod == "GET" && path==$"/{_options.RoutePrefix}/configuration/ui")
            {

            }
        }
    }
}
