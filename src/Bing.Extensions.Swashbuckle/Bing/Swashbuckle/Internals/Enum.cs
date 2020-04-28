using System;
using System.ComponentModel;
using System.Reflection;

namespace Bing.Swashbuckle.Internals
{
    /// <summary>
    /// 枚举 操作
    /// </summary>
    internal static class Enum
    {
        #region GetName(获取成员名)

        /// <summary>
        /// 获取成员名
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="member">成员名、值、实例均可，范例：Enum1枚举有成员A=0，则传入Enum1.A或0，获取成员名"A"</param>
        public static string GetName<TEnum>(object member) => GetName(Common.GetType<TEnum>(), member);

        /// <summary>
        /// 获取成员名
        /// </summary>
        /// <param name="type">枚举类型</param>
        /// <param name="member">成员名、值、实例均可，范例：Enum1枚举有成员A=0，则传入Enum1.A或0，获取成员名"A"</param>
        public static string GetName(Type type, object member)
        {
            if (type == null)
                return string.Empty;
            if (member == null)
                return string.Empty;
            if (member is string)
                return member.ToString();
            if (type.GetTypeInfo().IsEnum == false)
                return string.Empty;
            return System.Enum.GetName(type, member);
        }

        #endregion

        #region GetDescription(获取描述)

        /// <summary>
        /// 获取描述，使用<see cref="DescriptionAttribute"/>特性设置描述
        /// </summary>
        /// <typeparam name="T">枚举</typeparam>
        /// <param name="member">成员名、值、实例均可,范例:Enum1枚举有成员A=0,可传入"A"、0、Enum1.A，获取值0</param>
        public static string GetDescription<T>(object member) => Reflection.GetDescription<T>(GetName<T>(member));

        /// <summary>
        /// 获取描述，使用<see cref="DescriptionAttribute"/>特性设置描述
        /// </summary>
        /// <param name="type">枚举类型</param>
        /// <param name="member">成员名、值、实例均可,范例:Enum1枚举有成员A=0,可传入"A"、0、Enum1.A，获取值0</param>
        public static string GetDescription(Type type, object member) => Reflection.GetDescription(type, GetName(type, member));

        #endregion
    }
}
