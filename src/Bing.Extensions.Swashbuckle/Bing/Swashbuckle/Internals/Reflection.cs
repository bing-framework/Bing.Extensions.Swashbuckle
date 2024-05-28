using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Bing.Swashbuckle.Internals;

/// <summary>
/// 反射 操作
/// </summary>
internal static class Reflection
{
    #region GetDescription(获取类型描述)

    /// <summary>
    /// 获取类型描述，使用<see cref="DescriptionAttribute"/>设置描述
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    public static string GetDescription<T>() => GetDescription(Common.GetType<T>());

    /// <summary>
    /// 获取类型成员描述，使用<see cref="DescriptionAttribute"/>设置描述
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="memberName">成员名称</param>
    public static string GetDescription<T>(string memberName) => GetDescription(Common.GetType<T>(), memberName);

    /// <summary>
    /// 获取类型成员描述，使用<see cref="DescriptionAttribute"/>设置描述
    /// </summary>
    /// <param name="type">类型</param>
    /// <param name="memberName">成员名称</param>
    public static string GetDescription(Type type, string memberName)
    {
        if (type == null)
            return string.Empty;
        return string.IsNullOrWhiteSpace(memberName)
            ? string.Empty
            : GetDescription(type.GetTypeInfo().GetMember(memberName).FirstOrDefault());
    }

    /// <summary>
    /// 获取类型成员描述，使用<see cref="DescriptionAttribute"/>设置描述
    /// </summary>
    /// <param name="member">成员</param>
    public static string GetDescription(MemberInfo member)
    {
        if (member == null)
            return string.Empty;
        return member.GetCustomAttribute<DescriptionAttribute>() is DescriptionAttribute attribute
            ? attribute.Description
            : member.Name;
    }

    #endregion
}