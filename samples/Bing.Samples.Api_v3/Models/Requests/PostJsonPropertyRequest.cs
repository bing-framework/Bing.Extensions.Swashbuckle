using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Bing.Samples.Api.Models.Requests
{
    /// <summary>
    /// Post Json属性请求
    /// </summary>
    public class PostJsonPropertyRequest
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [JsonProperty("nickname")]
        public string OtherName { get; set; }

        /// <summary>
        /// Json属性名
        /// </summary>
        [JsonPropertyName("jsonPropertyName")]
        public string SerializeName { get; set; }

        /// <summary>
        /// 子属性
        /// </summary>
        public PostJsonPropertyChildRequest Child { get; set; }
    }

    /// <summary>
    /// Post Json子属性请求
    /// </summary>
    public class PostJsonPropertyChildRequest
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [JsonProperty("nickname")]
        public string OtherName { get; set; }

        /// <summary>
        /// Json属性名
        /// </summary>
        [JsonPropertyName("jsonPropertyName")]
        public string SerializeName { get; set; }
    }
}
