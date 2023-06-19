using MediatR;
using Microsoft.AspNetCore.Mvc;

/*
    这段代码定义了一个叫做 `BaseApiController` 的类，该类继承自 `ControllerBase` 类，`ControllerBase` 是 ASP.NET Core MVC 中用于创建 Web API 控制器的基类。以下是详细的解释：

    - `[ApiController]` 和 `[Route("api/[controller]")]` 是用于 ASP.NET Core 的特性（Attributes），它们为该控制器提供了元数据。 `[ApiController]` 特性表明该类是一个 API 控制器类，`[Route("api/[controller]")]` 特性指示了控制器的路由模板，其中 `[controller]` 是控制器名称的占位符。

    - `private IMediator _mediator;` 定义了一个 `IMediator` 类型的私有成员 `_mediator`，`IMediator` 是 MediatR 库中的一个接口，它用于实现“中介者”模式，这可以帮助你实现命令、请求和事件之间的解耦。

    - `protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();` 这一行使用了 C# 的空合并赋值运算符 `??=`，如果 `_mediator` 是 `null`，它将通过 `HttpContext.RequestServices.GetService<IMediator>()` 获取 `IMediator` 服务实例并赋值给 `_mediator`。这样，你可以在继承了 `BaseApiController` 的其他控制器中使用 `Mediator` 属性来发送命令或发布事件。其中 `HttpContext.RequestServices.GetService<IMediator>()` 是获取依赖注入提供的 `IMediator` 实例的方式。

    总的来说，`BaseApiController` 是一个基础控制器类，你可以在你的应用程序中创建的其他控制器继承这个基础控制器，以复用一些常用的功能或设定，例如这里的 `Mediator` 属性。
*/

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= 
            HttpContext.RequestServices.GetService<IMediator>();
    }
}

