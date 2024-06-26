﻿using System;

namespace Bing.Swashbuckle.Attributes;

/// <summary>
/// Swagger：Api分组
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class SwaggerApiGroupAttribute : Attribute
{
    /// <summary>
    /// 分组名
    /// </summary>
    public string GroupName { get; set; }

    /// <summary>
    /// 初始化一个<see cref="SwaggerApiGroupAttribute"/>类型的实例
    /// </summary>
    /// <param name="groupName">分组名</param>
    public SwaggerApiGroupAttribute(string groupName) => GroupName = groupName;

    /// <summary>
    /// 初始化一个<see cref="SwaggerApiGroupAttribute"/>类型的实例
    /// </summary>
    /// <param name="group">分组</param>
    public SwaggerApiGroupAttribute(object group)
    {
        if (group == null)
            return;
        GroupName = string.Concat(@group);
    }
}