using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

// ReSharper disable once CheckNamespace
namespace Bing.Swashbuckle
{
    /// <summary>
    /// Api描述器(<see cref="ApiDescription"/>) 扩展
    /// </summary>
    public static class ApiDescriptionExtensions
    {
        /// <summary>
        /// 获取区域名称列表
        /// </summary>
        /// <param name="description">Api描述器</param>
        public static List<string> GetAreaName(this ApiDescription description)
        {
            var areaName = description.ActionDescriptor.RouteValues["area"];
            var controllerName = description.ActionDescriptor.RouteValues["controller"];
            var areaList = new List<string>();
            areaList.Add(controllerName);
            if (!string.IsNullOrWhiteSpace(areaName))
                description.RelativePath = $"{areaName}/{controllerName}/{description.RelativePath}";
            return areaList;
        }
    }
}
