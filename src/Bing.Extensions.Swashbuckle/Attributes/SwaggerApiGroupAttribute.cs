using System;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Bing.Extensions.Swashbuckle.Attributes
{
    /// <summary>
    /// Swagger：Api分组
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class SwaggerApiGroupAttribute : Attribute, IApiDescriptionGroupNameProvider
    {
        /// <summary>
        /// 分组名
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 初始化一个<see cref="SwaggerApiGroupAttribute"/>类型的实例
        /// </summary>
        /// <param name="groupName">分组名</param>
        public SwaggerApiGroupAttribute(string groupName)
        {
            GroupName = groupName;
        }

        /// <summary>
        /// 初始化一个<see cref="SwaggerApiGroupAttribute"/>类型的实例
        /// </summary>
        /// <param name="group">分组</param>
        public SwaggerApiGroupAttribute(object group)
        {
            if (group is Enum groupEnum)
            {
                GroupName = groupEnum.ToString();
                return;
            }

            if (group is string groupStr)
            {
                GroupName = groupStr;
                return;
            }

            GroupName = group.ToString();
        }
    }
}
