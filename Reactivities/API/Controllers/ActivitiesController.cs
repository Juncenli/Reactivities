
/*
    This code is for an API Controller named `ActivitiesController` in ASP.NET Core. This controller is responsible for handling HTTP requests related to `Activity` objects. Here's a detailed explanation:

    - `public class ActivitiesController : BaseApiController`: This line is defining the `ActivitiesController` which inherits from `BaseApiController`. The `BaseApiController` likely contains some common functionality for all your API controllers.

    - The `ActivitiesController` contains five methods: `GetActivities`, `GetActivity`, `CreateActivity`, `Edit`, and `Delete`. Each of these methods corresponds to a different HTTP request method (`GET`, `POST`, `PUT`, `DELETE`) and is intended to handle a specific operation relating to activities.

    - `Mediator.Send(new List.Query())`: The `Mediator` is an instance of the MediatR service (a library implementing the "mediator" pattern), which is used to send requests. The `List.Query` is a request object that asks for a list of `Activity` objects. This operation is asynchronous, which is why you see the `await` keyword. The same pattern is followed for the other request types (`Details.Query`, `Create.Command`, etc.).

    - `[HttpGet]`, `[HttpGet("{id}")]`, `[HttpPost]`, `[HttpPut("{id}")]`, and `[HttpDelete("{id}")]`: These are called attribute routing in ASP.NET Core. They map the corresponding method to an HTTP request of the same type. For example, `[HttpGet]` would map `GetActivities()` to a `GET` request. The `"{id}"` in the URL means that it's a route parameter, and the method expects a corresponding parameter (in this case, the `id` of the activity).

    - Each method is `async` and returns a `Task`. This means the methods are asynchronous, i.e., they can start a task and then free up the current thread to do other work instead of waiting for the task to finish. This helps to improve the scalability of your application.

    - The `ActionResult` and `IActionResult` types that the methods return are both types that can represent HTTP response messages. `ActionResult<T>` can also contain a value of type `T` which will be serialized into the HTTP response body. `IActionResult` is the non-generic version of `ActionResult<T>`. It's generally recommended to use `ActionResult<T>` when possible, as it provides stronger typing for the response. But here `IActionResult` is used in some methods, likely because those methods don't need to return any specific data, just a response status.

    This controller effectively provides a full CRUD (Create, Read, Update, Delete) interface for `Activity` objects.
*/


using Application.Activities;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ActivitiesController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetActivities()
        {
            return HandleResult(await Mediator.Send(new List.Query()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetActivity(Guid id)
        {
            return HandleResult(await Mediator.Send(new Details.Query { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> CreateActivity(Activity activity)
        {
            return HandleResult(await Mediator.Send(new Create.Command { Activity = activity }));
        }

        [Authorize(Policy = "IsActivityHost")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id, Activity activity)
        {
            activity.Id = id;
            return HandleResult(await Mediator.Send(new Edit.Command { Activity = activity }));
        }

        [Authorize(Policy = "IsActivityHost")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
        }

        [HttpPost("{id}/attend")]
        public async Task<IActionResult> Attend(Guid id)
        {
            return HandleResult(await Mediator.Send(new UpdateAttendance.Command{Id = id}));
        }
    }
}