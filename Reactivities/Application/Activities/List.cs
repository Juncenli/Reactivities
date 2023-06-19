using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;

/*
    这个代码片段是一个 ASP.NET Core 使用 MediatR 库实现的 CQRS（Command Query Responsibility Segregation）模式的例子。在这个模式中，我们把数据的读和写操作分离成两个独立的模型，使得系统的复杂性降低。

    在这段代码中，我们定义了一个 List 类，这个类包含两个嵌套的类：Query 和 Handler。这是 MediatR 中的典型模式，它通过这种方式实现了对命令和查询的解耦。

    1. `Query` 类：这个类代表一个查询请求，这里它没有任何成员因为它只是一个简单的列表查询，不需要传递任何参数。该类实现了 `IRequest<List<Activity>>` 接口，代表这个查询请求的结果应该是一个 Activity 的 List。

    2. `Handler` 类：这个类是实际处理请求的地方。它实现了 `IRequestHandler<Query, List<Activity>>` 接口，这表明它是处理 `Query` 类型请求并返回 `List<Activity>` 的处理器。在 `Handle` 方法中，它从 `DataContext` 获取所有的 `Activities` 并返回。

    这段代码实现的功能是：当接收到一个 `List.Query` 请求时，MediatR 会找到对应的 `List.Handler` 来处理这个请求，并返回一个 Activity 的列表。

    这种方式可以使你的应用更容易维护和测试，因为每个操作都被单独的类处理，这些类之间互相独立，只关注各自的职责。

*/

namespace Application.Activities
{
    public class List
    {
        public class Query : IRequest<List<Activity>> { }

        public class Handler : IRequestHandler<Query, List<Activity>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<Activity>> Handle(Query request, CancellationToken token)
            {
                return await _context.Activities.ToListAsync();
            }
        }
    }
}

