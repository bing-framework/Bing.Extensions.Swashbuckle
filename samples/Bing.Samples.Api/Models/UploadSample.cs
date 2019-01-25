using Microsoft.AspNetCore.Http;

namespace Bing.Samples.Api.Models
{
    /// <summary>
    /// 上传文件
    /// </summary>
    public class UploadSample
    {
        /// <summary>
        /// 文件
        /// </summary>
        public IFormFile File { get; set; }

        /// <summary>
        /// 第二个文件
        /// </summary>
        public IFormFile TwoFile { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 尺寸大小
        /// </summary>
        public int Size { get; set; }
    }
}
