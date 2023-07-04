/*
    这段代码定义了一个“编辑活动”的命令和处理器，使用了MediatR库以及AutoMapper库。我来详细解释下：

    - `Command` 类定义了一个请求类型，它携带了要更新的 `Activity` 对象。

    - `Handler` 类实现了 `IRequestHandler<Command>` 接口，处理 `Command` 请求。

    - 在处理器（`Handler`）类中，它包含两个私有字段：`_context` 和 `_mapper`。`_context` 是 `DataContext` 类型，它表示数据库上下文，用于执行数据库操作；`_mapper` 是 `IMapper` 类型，它是AutoMapper库的主要接口，用于执行对象之间的映射。

    - `Handler` 类的构造函数接收 `DataContext` 和 `IMapper` 两个参数，并赋值给对应的字段。这是通过依赖注入实现的。

    - `Handle` 方法是 `IRequestHandler` 接口的实现，它在这里的功能是查找到要修改的 `Activity` 对象，然后用 `_mapper.Map` 方法将请求中的 `Activity` 对象的值映射到找到的 `Activity` 对象上，然后保存到数据库中。

    - `Handle` 方法返回 `Unit.Value`，这是MediatR库中表示无返回值的特殊方式。

    总的来说，这段代码是在处理一个“编辑活动”的请求，它首先从数据库中找到要编辑的活动，然后把新的活动数据映射到找到的活动上，最后保存到数据库。这就是CQRS中的Command部分（即写操作）。
*/
using Application.Core;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Edit
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Activity Activity { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Activity).SetValidator(new ActivityValidator());
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await _context.Activities.FindAsync(request.Activity.Id);

                if (activity == null) return null;

                _mapper.Map(request.Activity, activity);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to update activity");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}