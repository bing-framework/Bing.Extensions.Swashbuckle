using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Bing.Extensions.Swashbuckle.Extensions
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
        /// <returns></returns>
        public static List<string> GetAreaName(this ApiDescription description)
        {
            var areaName = description.ActionDescriptor.RouteValues["area"];
            var controllerName = description.ActionDescriptor.RouteValues["controller"];
            var areaList = new List<string>();
            areaList.Add(controllerName);
            if (!string.IsNullOrWhiteSpace(areaName))
            {
                description.RelativePath = $"{areaName}/{controllerName}/{description.RelativePath}";
            }

            return areaList;
        }
    }
}
