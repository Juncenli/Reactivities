/*
    This code defines a `Create` command and its handler in the context of the MediatR library. Here's a more detailed breakdown:

    - `Command` class defines a request type. It contains the `Activity` object that needs to be added to the database.

    - `Handler` class implements `IRequestHandler<Command>` interface to handle `Command` requests.

    - Within the `Handler` class, there's a private field `_context` of type `DataContext`. `DataContext` is essentially the database context and is used for interacting with the database.

    - The `Handler` class constructor takes `DataContext` as a parameter and assigns it to the `_context` field. This is done via dependency injection.

    - `Handle` method is an implementation of the `IRequestHandler` interface. Its functionality here is to add the `Activity` object from the `Command` request to the `DataContext`.

    - After adding the `Activity` to the context, it calls `_context.SaveChangesAsync()` to commit the changes to the database.

    - Finally, it returns `Unit.Value` which signifies to MediatR that the operation has been completed successfully. `Unit` is the equivalent of `void` in a standard C# method, but since `Handle` method must return a `Task`, `Unit.Value` is used instead of `void`.

    In summary, this code handles a "create activity" request. It adds a new `Activity` to the database. This is part of the Command portion in CQRS (i.e., the write operation).
*/
using Application.Core;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Activity Activity { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public class CommandValidator : AbstractValidator<Command>
            {
                public CommandValidator()
                {
                    RuleFor(x => x.Activity).SetValidator(new ActivityValidator());
                }
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                _context.Activities.Add(request.Activity);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to create activity");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}