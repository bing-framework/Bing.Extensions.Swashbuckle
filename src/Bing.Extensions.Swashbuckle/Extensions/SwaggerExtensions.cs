using System.Reflection;
using Bing.Extensions.Swashbuckle.Configs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Bing.Extensions.Swashbuckle.Extensions
{
    /// <summary>
    /// Swagger扩展
    /// </summary>
    public static partial class SwaggerExtensions
    {
        /// <summary>
        /// 使用默认SwggerUI
        /// </summary>
        /// <param name="options">SwaggerUI配置</param>
        // ReSharper disable once InconsistentNaming
        public static void UseDefaultSwaggerUI(this SwaggerUIOptions options)
        {
            options.DefaultModelExpandDepth(2);// 接口列表折叠配置
            options.DefaultModelRendering(ModelRendering.Example);// 控制首次呈现API时模型的显示方式（模型|示例）。
            options.ShowExtensions();// 显示扩展信息
            options.DefaultModelsExpandDepth(-1);// 隐藏model
            options.DisplayOperationId();// 显示控制器接口方法名
            options.DisplayRequestDuration();// 显示请求持续时间（以毫秒为单位）
            options.DocExpansion(DocExpansion.None);// 文档显示方式：显示控制器
            options.EnableDeepLinking();// 启用深层连接，用于指定Url自动跳转到相应标签
            options.EnableFilter();// 启用过滤文本框
        }

        /// <summary>
        /// 使用自定义首页
        /// </summary>
        /// <param name="options">SwaggerUI选项</param>
        public static void UseCustomSwaggerIndex(this SwaggerUIOptions options)
        {
            var currentAssembly = typeof(CustomSwaggerOptions).GetTypeInfo().Assembly;
            options.IndexStream = () =>
                currentAssembly.GetManifestResourceStream($"{currentAssembly.GetName().Name}.Resources.index.html");
        }

        /// <summary>
        /// 写入Swagger页面
        /// </summary>
        /// <param name="response">Http响应</param>
        /// <param name="page">页面名称</param>
        public static void WriteSwaggerPage(this HttpResponse response, string page)
        {
            var currentAssembly = typeof(CustomSwaggerOptions).GetTypeInfo().Assembly;
            var stream = currentAssembly.GetManifestResourceStream($"{currentAssembly.GetName().Name}.Resources.{page}.html");
            if (stream == null)
            {
                return;
            }
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            response.ContentType = "text/html;charset=utf-8";
            response.StatusCode = StatusCodes.Status200OK;
            response.Body.Write(buffer, 0, buffer.Length);
        }
    }
}
