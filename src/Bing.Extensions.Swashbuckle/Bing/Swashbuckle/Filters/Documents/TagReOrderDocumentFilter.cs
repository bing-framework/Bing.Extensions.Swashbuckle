using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bing.Swashbuckle.Filters.Documents
{
    /// <summary>
    /// 标识重新排序 文档过滤器。用于对控制器进行排序
    /// </summary>
    internal class TagReOrderDocumentFilter : IDocumentFilter
    {
        /// <summary>
        /// 是否降序排序
        /// </summary>
        private readonly bool _orderByDesc;

        /// <summary>
        /// 初始化一个<see cref="TagReOrderDocumentFilter"/>类型的实例
        /// </summary>
        /// <param name="orderByDesc">是否降序排序</param>
        public TagReOrderDocumentFilter(bool orderByDesc = false) => _orderByDesc = orderByDesc;

        /// <summary>
        /// 重写操作处理
        /// </summary>
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Tags = _orderByDesc
                ? swaggerDoc.Tags.OrderByDescending(tag => tag.Name).ToList()
                : swaggerDoc.Tags.OrderBy(tag => tag.Name).ToList();
        }
    }
}
