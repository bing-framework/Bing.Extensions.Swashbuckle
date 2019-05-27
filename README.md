# Bing.Extensions.Swashbuckle
[![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg)](https://mit-license.org/)

Bing.Extensions.Swashbuckle是扩展Swashbuckle.AspNetCore一些常用操作，便于使用以及过滤。

## Nuget
|Nuget|版本号|说明|
|---|---|---|
|Bing.Extensions.Swashbuckle|[![NuGet Badge](https://buildstats.info/nuget/Bing.Extensions.Swashbuckle?includePreReleases=true)](https://www.nuget.org/packages/Bing.Extensions.Swashbuckle)|

## 功能
- 添加文件参数`FileParameterOperationFilter`
- 添加请求头`RequestHeaderOperationFilter`
- 添加响应头`ResponseHeaderOperationFilter`
- 添加安全请求`SecurityRequirementsOperationFilter`
- 添加通用参数`CommonParametersOperationFilter`
- 添加追加授权信息到注释`AppendAuthorizeToSummaryOperationFilter`
- 添加Api接口版本默认值`ApiVersionDefaultValueOperationFilter`
- Url首字母小写`FirstLowerUrlDocumentFilter`
- Url小写`LowerCaseUrlDocumentFilter`
- 显示枚举注释`AddEnumDescriptionsDocumentFilter`

## 依赖类库
- [Swashbuckle.AspNetCore](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)

## 使用方式
```c#

```

## 作者

简玄冰

## 贡献与反馈

> 如果你在阅读或使用Bing中任意一个代码片断时发现Bug，或有更佳实现方式，请通知我们。

> 为了保持代码简单，目前很多功能只建立了基本结构，细节特性未进行迁移，在后续需要时进行添加，如果你发现某个类无法满足你的需求，请通知我们。

> 你可以通过github的Issue或Pull Request向我们提交问题和代码，如果你更喜欢使用QQ进行交流，请加入我们的交流QQ群。

> 对于你提交的代码，如果我们决定采纳，可能会进行相应重构，以统一代码风格。

> 对于热心的同学，将会把你的名字放到**贡献者**名单中。

## 免责声明
- 虽然我们对代码已经进行高度审查，并用于自己的项目中，但依然可能存在某些未知的BUG，如果你的生产系统蒙受损失，Bing 团队不会对此负责。
- 出于成本的考虑，我们不会对已发布的API保持兼容，每当更新代码时，请注意该问题。

## 开源地址
[https://github.com/bing-framework/Bing.Extensions.Swashbuckle](https://github.com/bing-framework/Bing.Extensions.Swashbuckle)

## License

**MIT**

> 这意味着你可以在任意场景下使用 Bing 应用框架而不会有人找你要钱。

> Bing 会尽量引入开源免费的第三方技术框架，如有意外，还请自行了解。
