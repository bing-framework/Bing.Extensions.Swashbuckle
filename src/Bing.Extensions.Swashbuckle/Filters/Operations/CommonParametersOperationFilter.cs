using System.Collections.Generic;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bing.Extensions.Swashbuckle.Filters.Operations
{
    /// <summary>
    /// 添加 生成通用参数(query/form/header) 操作过滤器
    /// </summary>
    public class CommonParametersOperationFilter:IOperationFilter
    {
        /// <summary>
        /// 默认通用参数列表
        /// </summary>
        private readonly List<IParameter> _defaultParameters=new List<IParameter>()
        {
            new NonBodyParameter()
            {
                Name = "Authorization",
                In = "header",
                Default = "",
                Type = "string"
            },
            new NonBodyParameter()
            {
                Name = "X-Requested-With",
                In = "header",
                Default = "XMLHttpRequest",
                Type = "string"
            }
        };

        /// <summary>
        /// 初始化一个<see cref="CommonParametersOperationFilter"/>类型的实例
        /// </summary>
        public CommonParametersOperationFilter() { }

        /// <summary>
        /// 初始化一个<see cref="CommonParametersOperationFilter"/>类型的实例
        /// </summary>
        /// <param name="parameters">追加参数列表</param>
        public CommonParametersOperationFilter(List<IParameter> parameters)
        {
            _defaultParameters.AddRange(parameters);
        }

        /// <summary>
        /// 重写操作处理
        /// </summary>
        /// <param name="operation">当前操作</param>
        /// <param name="context">操作过滤器上下文</param>
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation == null || context == null)
            {
                return;
            }

            if (operation.Parameters == null)
            {
                operation.Parameters = new List<IParameter>();
            }

            foreach (var item in _defaultParameters)
            {
                operation.Parameters.Add(item);
            }
        }
    }
}
