using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Bing.Extensions.Swashbuckle.Attributes;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bing.Extensions.Swashbuckle.Filters.Operations
{
    /// <summary>
    /// 添加文件参数 操作过滤器。支持<see cref="SwaggerUploadAttribute"/>特性
    /// </summary>
    public class FileParameterOperationFilter : IOperationFilter
    {
        /// <summary>
        /// 文件参数
        /// </summary>
        private static readonly string[] FileParameters = new[]
            {"ContentType", "ContentDisposition", "Headers", "Length", "Name", "FileName"};

        /// <summary>
        /// 重写操作处理
        /// </summary>
        /// <param name="operation">当前操作</param>
        /// <param name="context">操作过滤器上下文</param>
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (!context.ApiDescription.HttpMethod.Equals("POST", StringComparison.OrdinalIgnoreCase) &&
                !context.ApiDescription.HttpMethod.Equals("PUT", StringComparison.OrdinalIgnoreCase))
                return;
            UploadByAttribute(operation, context);
            UploadByFile<IFormFile>(operation, context);
            UploadByFile<List<IFormFile>>(operation, context);
        }

        /// <summary>
        /// 通过特性进行上传
        /// </summary>
        /// <param name="operation">当前操作</param>
        /// <param name="context">操作过滤器上下文</param>
        private void UploadByAttribute(Operation operation, OperationFilterContext context)
        {
            var swaggerUpload = context.MethodInfo.GetCustomAttributes<SwaggerUploadAttribute>().FirstOrDefault();
            if (swaggerUpload == null)
                return;
            AddMime(operation);
            RemoveExistingFileParameters(operation.Parameters);
            operation.Parameters.Add(new NonBodyParameter()
            {
                Name = swaggerUpload.Name,
                Required = swaggerUpload.Required,
                In = "formData",
                Type = "file",
                Description = swaggerUpload.Descritpion
            });
        }

        /// <summary>
        /// 添加Mime
        /// </summary>
        /// <param name="operation">当前操作</param>
        private void AddMime(Operation operation)
        {
            if (!operation.Consumes.Contains("multipart/form-data"))
                operation.Consumes.Add("multipart/form-data");
        }

        /// <summary>
        /// 移除已存在的文件参数
        /// </summary>
        /// <param name="operationParameters">操作参数列表</param>
        private void RemoveExistingFileParameters(IList<IParameter> operationParameters)
        {
            foreach (var parameter in operationParameters
                .Where(x => x.In == "query" && FileParameters.Contains(x.Name)).ToList())
                operationParameters.Remove(parameter);
        }

        /// <summary>
        /// 通过文件进行上传
        /// </summary>
        /// <param name="operation">当前操作</param>
        /// <param name="context">操作过滤器上下文</param>
        private void UploadByFile<T>(Operation operation, OperationFilterContext context)
        {
            var files = context.ApiDescription.ActionDescriptor.Parameters
                .Where(x => x.ParameterType == typeof(T)).ToList();
            if (files.Count == 0)
                return;
            AddMime(operation);
            foreach (var file in files)
            {
                var parameter = operation.Parameters.Single(x => x.Name == file.Name);
                operation.Parameters.Remove(parameter);
                operation.Parameters.Add(new NonBodyParameter()
                {
                    Name = parameter.Name,
                    In = "formData",
                    Type = "file",
                    Required = parameter.Required,
                    Default = parameter.Description
                });
            }
        }
    }
}
