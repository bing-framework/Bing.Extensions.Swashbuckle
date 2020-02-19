using System;
using System.Collections.Generic;
using System.Linq;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Bing.Knife4jUI
{
    /// <summary>
    /// Knife4j 配置对象
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class Knife4jUIConfigObject
    {
        /// <summary>
        /// 是否启用标签与操作的深层链接。<br />
        /// If set to true, enables deep linking for tags and operations
        /// </summary>
        public bool DeepLinking { get; set; } = false;

        /// <summary>
        ///是否显示操作标识。<br />
        /// Controls the display of operationId in operations list
        /// </summary>
        public bool DisplayOperationId { get; set; } = false;

        /// <summary>
        /// 模型列表默认展开级别（-1：隐藏模型）。<br />
        /// The default expansion depth for models (set to -1 completely hide the models)
        /// </summary>
        public int DefaultModelsExpandDepth { get; set; } = 1;

        /// <summary>
        /// 模型默认展开级别。<br />
        /// The default expansion depth for the model on the model-example section
        /// </summary>
        public int DefaultModelExpandDepth { get; set; } = 1;

        /// <summary>
        /// 模型默认渲染显示方式。<br />
        /// Controls how the model is shown when the API is first rendered.
        /// (The user can always switch the rendering for a given model by clicking the 'Model' and 'Example Value' links)
        /// </summary>
        public ModelRendering DefaultModelRendering { get; set; } = ModelRendering.Example;

        /// <summary>
        /// 显示请求持续时间（单位：毫秒）。<br />
        /// Controls the display of the request duration (in milliseconds) for Try-It-Out requests
        /// </summary>
        public bool DisplayRequestDuration { get; set; } = false;

        /// <summary>
        /// 文档展开方式。<br />
        /// Controls the default expansion setting for the operations and tags.
        /// It can be 'list' (expands only the tags), 'full' (expands the tags and operations) or 'none' (expands nothing)
        /// </summary>
        public DocExpansion DocExpansion { get; set; } = DocExpansion.List;

        /// <summary>
        /// 是否启用过滤
        /// </summary>
        public bool Filter { get; set; }

        /// <summary>
        /// 操作排序方式
        /// </summary>
        public string OperationsSorter { get; set; } = "alpha";

        /// <summary>
        /// 是否显示扩展值。<br />
        /// Controls the display of vendor extension (x-) fields and values for Operations, Parameters, and Schema
        /// </summary>
        public bool ShowExtensions { get; set; } = false;

        /// <summary>
        /// 标签排序
        /// </summary>
        public string TagsSorter { get; set; } = "alpha";

        /// <summary>
        /// 验证地址。<br />
        /// By default, Swagger-UI attempts to validate specs against swagger.io's online validator.
        /// You can use this parameter to set a different validator URL, for example for locally deployed validators (Validator Badge).
        /// Setting it to null will disable validation
        /// </summary>
        public string ValidatorUrl { get; set; } = "";

        /// <summary>
        /// 是否启用Json编辑器
        /// </summary>
        public bool JsonEditor { get; set; } = false;

        /// <summary>
        /// Api排序
        /// </summary>
        public string ApisSorter { get; set; } = "alpha";

        /// <summary>
        /// 是否显示请求头
        /// </summary>
        public bool ShowRequestHeaders { get; set; } = false;

        /// <summary>
        /// 可支持的提交方法。<br />
        /// List of HTTP methods that have the Try it out feature enabled.
        /// An empty array disables Try it out for all operations. This does not filter the operations from the display
        /// </summary>
        public IEnumerable<SubmitMethod> SupportedSubmitMethods { get; set; } = Enum.GetValues(typeof(SubmitMethod)).Cast<SubmitMethod>();
    }
}
