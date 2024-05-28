using System;
using System.Collections.Generic;

namespace Bing.Swashbuckle.Internals;

/// <summary>
/// 内部缓存
/// </summary>
internal static class InternalCache
{
    /// <summary>
    /// 枚举字典
    /// </summary>
    public static Dictionary<Type, List<(int Value, string Name, string Description)>> EnumDict =
        new Dictionary<Type, List<(int Value, string Name, string Description)>>();
}