using System.Collections.Generic;

namespace Bing.Extensions.Swashbuckle.Internal
{
    /// <summary>
    /// 构建上下文
    /// </summary>
    internal class BuildContext
    {
        /// <summary>
        /// Swagger扩展选项配置
        /// </summary>
        public SwaggerExtensionOptions Options { get; set; } = new SwaggerExtensionOptions();

        /// <summary>
        /// 对象
        /// </summary>
        /// <param name="key">键</param>
        public object this[string key]
        {
            get => Items[key];
            set => Items[key] = value;
        }

        /// <summary>
        /// 对象字典
        /// </summary>
        public IDictionary<string, object> Items { get; set; } = new Dictionary<string, object>();

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <typeparam name="TItem">类型</typeparam>
        /// <param name="key">键</param>
        public TItem GetItem<TItem>(string key) => (TItem) Items[key];

        /// <summary>
        /// 设置对象
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="item">项</param>
        public void SetItem(string key, object item) => Items[key] = item;

        /// <summary>
        /// 实例
        /// </summary>
        public static BuildContext Instance = new BuildContext();
    }
}
