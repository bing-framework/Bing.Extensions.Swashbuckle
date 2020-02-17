using System.Collections.Generic;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bing.Extensions.Swashbuckle.Filters.Operations
{
    /// <summary>
    /// 添加 生成通用参数(query/form/header) 操作过滤器
    /// </summary>
    public class CommonParametersOperationFilter : IOperationFilter
    {
        /// <summary>
        /// 默认通用参数列表
        /// </summary>
        private readonly List<OpenApiParameter> _defaultParameters = new List<OpenApiParameter>()
        {
            new OpenApiParameter()
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Schema = new OpenApiSchema() {Type = "string", Default = new OpenApiString("")},
            },
            new OpenApiParameter()
            {
                Name = "X-Requested-With",
                In = ParameterLocation.Header,
                Schema = new OpenApiSchema() {Type = "string", Default = new OpenApiString("XMLHttpRequest")}
            },
        };

        /// <summary>
        /// 初始化一个<see cref="CommonParametersOperationFilter"/>类型的实例
        /// </summary>
        public CommonParametersOperationFilter() { }

        /// <summary>
        /// 初始化一个<see cref="CommonParametersOperationFilter"/>类型的实例
        /// </summary>
        /// <param name="parameters">追加参数列表</param>
        public CommonParametersOperationFilter(IEnumerable<OpenApiParameter> parameters)
        {
            _defaultParameters.AddRange(parameters);
        }

        /// <summary>
        /// 重写操作处理
        /// </summary>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation == null || context == null)
                return;
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();
            foreach (var item in _defaultParameters)
                operation.Parameters.Add(item);
        }
    }
}
