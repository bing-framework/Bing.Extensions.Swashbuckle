namespace Bing.Samples.ApiGroup.Models
{
    /// <summary>
    /// 结果
    /// </summary>
    public class Result
    {
        /// <summary>
        /// 编码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 返回消息
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 请求成功
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Result Success(object data = null)
        {
            var result = new Result();
            result.Code = 0;
            result.Message = "请求成功";
            result.Data = data;
            return result;
        }

        /// <summary>
        /// 请求失败
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static Result Fail(int code=1, string message="请求失败")
        {
            var result = new Result();
            result.Code = code;
            result.Message = message;
            return result;
        }
    }
}
