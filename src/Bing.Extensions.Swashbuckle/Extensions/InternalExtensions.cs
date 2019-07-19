using System.ComponentModel;

namespace Bing.Extensions.Swashbuckle.Extensions
{
    /// <summary>
    /// 内部公共扩展
    /// </summary>
    internal static partial class InternalExtensions
    {
        #region Description(获取枚举描述)

        /// <summary>
        /// 获取枚举描述，使用<see cref="DescriptionAttribute"/>特性设置描述
        /// </summary>
        /// <param name="instance">枚举实例</param>
        public static string Description(this System.Enum instance)
        {
            return Internal.Enum.GetDescription(instance.GetType(), instance);
        }

        #endregion
    }
}
