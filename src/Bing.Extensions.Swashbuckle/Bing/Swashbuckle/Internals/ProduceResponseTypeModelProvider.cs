using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Bing.Swashbuckle.Internals;

/// <summary>
/// 产生响应类型模型提供程序
/// </summary>
public class ProduceResponseTypeModelProvider : IApplicationModelProvider
{
    /// <summary>
    /// SwaggerEx 选项配置
    /// </summary>
    private readonly SwaggerExOptions _options;

    /// <summary>
    /// 初始化一个<see cref="ProduceResponseTypeModelProvider"/>类型的实例
    /// </summary>
    public ProduceResponseTypeModelProvider()
    {
            _options = BuildContext.Instance.ExOptions;
        }

    /// <inheritdoc />
    public int Order => 3;

    /// <inheritdoc />
    public void OnProvidersExecuting(ApplicationModelProviderContext context)
    {
            foreach (var controller in context.Result.Controllers)
            {
                foreach (var action in controller.Actions)
                {
                    // 假设所有操作类型都是 Task<ActionResult<ReturnType>>
                    //Type returnType = null;

                    var actionMethodReturnType = action.ActionMethod.ReturnType;
                    var actualReturnType = default(Type);
                    if (actionMethodReturnType == typeof(Task) || actionMethodReturnType.BaseType == typeof(Task)) // 异步类型处理
                    {
                        if ((actionMethodReturnType.GenericTypeArguments?.Length ?? 0) > 0)
                        {
                            // 返回类型：Task<IActionResult<ReturnType>> 或者 Task<ActionResult<ReturnType>>
                            var firstGenericType = actionMethodReturnType.GenericTypeArguments[0];
                            if (firstGenericType != typeof(IActionResult))
                            {
                                actualReturnType = GetResultDataType(firstGenericType);
                            }
                        }
                        else
                        {
                            // 处理Task
                            actualReturnType = actionMethodReturnType;
                        }
                    }
                    else if (actionMethodReturnType != typeof(IActionResult) && actionMethodReturnType != typeof(ActionResult)) // 同步类型处理
                    {
                        actualReturnType = GetResultDataType(actionMethodReturnType);
                    }

                    if (actualReturnType != default)
                    {
                        var firstGenericType = actualReturnType;
                        _options?.GlobalResponseWrapperAction(action, firstGenericType, IsVoid(firstGenericType));
                    }
                }
            }
        }

    /// <summary>
    /// 获取结果数据类型
    /// </summary>
    /// <param name="returnType">返回类型</param>
    private Type GetResultDataType(Type returnType)
    {
            if (returnType == null)
                throw new ArgumentNullException(nameof(returnType));
            return returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof(ActionResult<>)
                ? returnType.GetGenericArguments()[0]
                : returnType;
        }

    /// <summary>
    /// 是否无结果返回
    /// </summary>
    /// <param name="returnType">返回类型</param>
    private bool IsVoid(Type returnType)
    {
            // 返回 void 类型
            if (returnType == typeof(void))
                return true;
            if (returnType == typeof(Task))
                return true;
            return false;
        }

    /// <inheritdoc />
    public void OnProvidersExecuted(ApplicationModelProviderContext context)
    {
        }
}