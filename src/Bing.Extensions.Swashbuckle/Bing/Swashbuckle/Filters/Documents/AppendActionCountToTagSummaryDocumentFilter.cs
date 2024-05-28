using System.Collections.Generic;
using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bing.Swashbuckle.Filters.Documents;

/// <summary>
/// 控制器方法计数 文档过滤
/// </summary>
internal class AppendActionCountToTagSummaryDocumentFilter: IDocumentFilter
{
    /// <summary>
    /// 重写操作处理
    /// </summary>
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        if (swaggerDoc.Tags == null)
            return;
        var tagActionCount = new Dictionary<string, int>();
        foreach (var path in swaggerDoc.Paths)
        {
            var possibleParameterizedOperations = path.Value.Operations.Select(x => x.Value);
            possibleParameterizedOperations.Where(o=>o?.Tags!=null).ToList().ForEach(x =>
            {
                foreach (var tag in x.Tags)
                {
                    if (!tagActionCount.ContainsKey(tag.Name))
                        tagActionCount.Add(tag.Name, 1);
                    else
                        tagActionCount[tag.Name]++;
                }
            });
        }

        foreach (var tagActionCountKey in tagActionCount.Keys)
        {
            foreach (var tag in swaggerDoc.Tags)
            {
                if (tag.Name == tagActionCountKey)
                    tag.Description += $"({tagActionCount[tagActionCountKey]})";
            }
        }
    }
}