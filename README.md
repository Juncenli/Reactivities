- .NET Core is a cross-platform, `open-source framework` developed by Microsoft that can be used to develop a variety of application types, including web applications (`using ASP.NET Core`), desktop applications, cloud services, and more. Advantages of developing with .NET Core include good performance, cross-platform capabilities (it can run on Windows, Linux, and MacOS), and rich library support.

- Node.js is an open-source, cross-platform `JavaScript runtime environment based on Chrome's V8 JavaScript engine`. It allows JavaScript to be run outside the browser on the server side. Node.js has an extremely rich package manager, npm, and a vibrant community with a large number of open-source libraries available for use.

As for Express.js and ASP.NET, they are web development frameworks built on these platforms:

- Express.js is a minimalistic and flexible web application development framework based on the Node.js platform. It provides a series of powerful features to help you create a variety of web and mobile device applications.

- ASP.NET Core is a framework for building web applications that's built on .NET Core. It uses a Model-View-Controller (MVC) design pattern and is easy to test, has powerful routing features, and offers good support for RESTful services, among other features.



# .NET Core CLI

## dotnet restore

The `dotnet restore` is a command within the .NET Core Command Line Interface (CLI) used for restoring the dependencies of a project. In .NET Core, dependencies for a project are typically defined in the project file or a `*.deps.json` file.

When you run the `dotnet restore` command, the .NET CLI reads the project file, identifies the NuGet packages that the project depends on, and downloads them to your machine. This process is known as "restoring".

If any packages can't be downloaded, or there's a mismatch between the versions of the packages defined in the project file and the `*.deps.json` file, the `dotnet restore` command will return an error message.

This command is useful in scenarios like when you've just checked out a project from a version control system (like git) or you've added a new NuGet package dependency to your project. In these cases, you would need to run `dotnet restore` to ensure that your project can locate all its necessary dependencies properly.

As of .NET Core 2.0, this command is not necessary in most cases, as commands such as `dotnet build` and `dotnet run` will automatically perform restore operations. However, if you need to perform a restore operation separately, or you wish to explicitly perform a restore operation in a Continuous Integration/Continuous Deployment (CI/CD) environment, you can still use the `dotnet restore` command.

## dotnet watch --no-hot-reload

The `dotnet watch` command is a tool that runs a .NET Core CLI command when source files change. This command is particularly useful when you want to test, build, or run code every time you make changes in your .NET project. 

The `--no-hot-reload` option is used to disable hot reload for `dotnet watch`. Hot Reload is a feature that was introduced in .NET 6.0, which allows developers to modify their application's source code while it's running, without requiring them to manually pause or hit a breakpoint.

Hot Reload can be very helpful for speeding up development because it eliminates the need to manually stop and restart your application every time you make a change. But, if for some reason, you do not want to use this feature, or it is causing issues with your specific application, you can disable it by using the `--no-hot-reload` option.

In summary, the command `dotnet watch --no-hot-reload` will watch your files for changes and automatically re-run the project every time a file is changed, without using the Hot Reload feature.


# Architecture


In a typical .NET full-stack application, you might encounter the following parts:

1. **Client-App:** This layer is usually the client-side application, the part that a user interacts with directly. It could be a Web application (utilizing HTML, CSS, JavaScript, etc.), a desktop application (using WPF, WinForms, etc.), or a mobile application (using Xamarin, MAUI, etc.). The client application is responsible for taking user input, sending it to the server-side (API), and presenting data received from the server to the user.

2. **API:** This layer serves as the entry point on the server-side, handling all incoming requests from the client. The API layer routes HTTP requests to appropriate handlers (like controllers) and formats data into HTTP responses. It may also handle cross-cutting concerns such as authentication, authorization, logging, exception handling, etc.

3. **Application:** This layer contains the core part of the business logic, such as services, commands, queries, etc. It deals with business rules and workflows and orchestrates interactions between the Domain layer and the Infrastructure layer.

4. **Domain:** The Domain layer includes the heart of the business model. It contains all the concepts of the business domain, such as entities, value objects, domain events, domain services, etc. The Domain layer is the core of the business logic, but it is ignorant of the existence of the Application and Infrastructure layers, and it does not depend on them.

5. **Persistence:** Also referred to as the data access layer, this layer mainly interacts with the database, including database connections, CRUD (Create, Read, Update, Delete) operations of data, etc. It typically uses an Entity Framework or another ORM tool to simplify data operations.

6. **Infrastructure:** The Infrastructure layer provides support for other layers to interact with external systems or services, like the file system, email service, message queues, network requests, etc. This layer provides services to other layers but doesn't partake directly in the processing of business logic.

These parts work together to deliver full-stack functionality. Each has its responsibilities, and by working together, the code becomes easier to manage and maintain, and the application becomes more scalable.



Consider drawing a diagram with the following layers, from bottom to top:

1. **Persistence / Database:** This is the lowest layer and represents the data storage.

2. **Infrastructure:** This layer interacts directly with the Persistence layer and might have outgoing arrows to external systems (like email servers, file servers, etc.).

3. **Domain:** This layer sits on top of the Infrastructure layer and contains business logic and business rules.

4. **Application:** This layer is above the Domain layer and orchestrates the workflow of business operations.

5. **API:** This is the entry point on the server-side and should be depicted as interfacing between the Application layer and the Client-App layer.

6. **Client-App:** This is the topmost layer that interacts directly with the user. 

Arrows can be used to denote the interaction between the layers: 

- The Client-App sends requests to the API.
- The API utilizes services in the Application layer.
- The Application layer leverages the Domain for business rules and Infrastructure for interactions with external systems.
- The Infrastructure interacts with the Persistence layer for data operations.



与.NET项目结构相比，Spring Boot（以及更广泛的Java生态系统）通常具有以下结构：

1. **Model/Domain**：这一层和.NET的Domain层基本对应，包含业务领域内的基本实体或对象。例如，对于一个银行应用，这里可能有Account、User等类。

2. **Repository/DAO（Data Access Object）**：这一层对应.NET中的Persistence和部分Infrastructure层。这里包含所有与数据库交互的代码，例如CRUD操作。在Spring中，通常使用Spring Data JPA的Repository接口来简化开发。

3. **Service**：Service层对应.NET的部分Domain和Application层。这里包含主要的业务逻辑，所有的事务管理都在这一层处理。

4. **Controller**：Controller层对应.NET的API层。这是接收和处理来自用户的HTTP请求，并调用Service层进行处理，最后返回响应的地方。

5. **DTO (Data Transfer Object)**：DTO不是必需的，但在很多应用中都会用到，尤其是在Web服务中。DTOs用于在Controller和Service层之间，以及Service层和Repository/DAO层之间传输数据。它们对应.NET中的ViewModels或DTOs。

6. **Client-App**：这一部分在Spring Boot应用中不常见，因为Spring Boot主要关注后端服务。然而，在一个完整的应用中，这部分对应.NET的Client-App层，可能包括Web前端（HTML/CSS/JavaScript）、桌面应用、移动应用等，这些应用通过API与后端服务通信。

以上就是Spring Boot和.NET在架构上的主要差异。需要注意的是，这两种技术在很大程度上是互补的，它们都可以用于构建复杂的企业级应用。你应该根据具体的项目需求和团队技能来选择最适合的技术。




# API


1. **Program.cs** file:

   - The method `WebApplication.CreateBuilder(args)` is used to initiate a new web application builder. This is a new feature in .NET 6 that simplifies the setting and launching of an application.
   
   - Various services are added to the dependency injection container, including controllers, API explorers, Swagger generators, and the `DataContext` database context. These services will be used in subsequent processing.
   
   - The `UseSqlite()` method is used to configure EF Core to use the SQLite database, and the `GetConnectionString()` method is used to obtain the database connection string from the configuration files.
   
   - If the application environment is in development mode, Swagger is enabled. Swagger is a tool that aids in the building, documenting, and testing of RESTful APIs.
   
   - During the startup of the application, database migration and data seeding are performed. If an error occurs during the migration, the error is logged using the logger service.

2. **BaseApiController.cs** file:

   - This file defines a base controller class named `BaseApiController`. The class is adorned with the `[ApiController]` and `[Route]` attributes, and all controller classes derived from this base class will inherit these attributes. The `[ApiController]` attribute marks the class as a controller class, and the `[Route]` attribute defines the routing template, where `[controller]` gets replaced by the derived controller's class name minus "Controller".

3. **ActivitiesController.cs** file:

   - This file defines a controller that inherits from `BaseApiController`, so it automatically acquires the `[ApiController]` and `[Route]` attributes.
   
   - A `DataContext` instance `_context` is injected into this controller, which will be used for database access in subsequent operations.
   
   - Two action methods are provided: one for getting a list of all activities, and the other for getting a single activity by a specified ID. Both methods are marked with the HTTP method attribute `HttpGet`, meaning they will handle HTTP GET requests.


# Client

```
npx create-react-app client-app --use-npm --template typescript
```

![components](README-pics/react-components1.png)

![components](README-pics/react-components2.png)

![virtualDOM](README-pics/virtual-DOM.png)


## In React, `useState` and `useEffect` 

1. **useState**: This hook allows you to add state to functional components. It is a direct replacement for `this.state` and `this.setState` in class components. It returns an array containing the current state value and a function to update it.

Here's an example of `useState`:

```jsx
import React, { useState } from 'react';

function Counter() {
  const [count, setCount] = useState(0);

  return (
    <div>
      <p>You clicked {count} times</p>
      <button onClick={() => setCount(count + 1)}>
        Click me
      </button>
    </div>
  );
}
```

In the example above, `count` is the state variable, and `setCount` is the function that updates the state variable. The `useState` hook initializes `count` to zero.

2. **useEffect**: This hook allows you to perform side effects in your components. It is a direct replacement for lifecycle methods like `componentDidMount`, `componentDidUpdate`, and `componentWillUnmount` in class components.

Here's an example of `useEffect`:

```jsx
import React, { useState, useEffect } from 'react';

function Counter() {
  const [count, setCount] = useState(0);

  // Similar to componentDidMount and componentDidUpdate:
  useEffect(() => {
    // Update the document title using the browser API
    document.title = `You clicked ${count} times`;
  });

  return (
    <div>
      <p>You clicked {count} times</p>
      <button onClick={() => setCount(count + 1)}>
        Click me
      </button>
    </div>
  );
}
```

In the example above, the `useEffect` hook runs after every render when the `count` state changes, updating the document title.




`useEffect` is a hook in React that allows you to perform side effect operations within function components. Side effects are operations that have an impact outside of the function, such as data fetching, subscriptions, or manually changing the React component's DOM.

The basic syntax of `useEffect` is as follows:

```jsx
useEffect(() => {
  // Perform your side effect code here
}, [/* dependencies list */]);
```

## Talk About useEffect() Deeper
`useEffect` hook takes two parameters: the first is a function where you can perform the side effect operations; the second is an array known as the dependencies list. React will track all the variables listed in this array and re-run the side effect function whenever any of these variables change.

If you don't provide a second parameter, `useEffect` will run the side effect function after every render (i.e., after the initial render and each update render).

If you provide an empty array `[]` as the second parameter, `useEffect` will only run the side effect function after the initial render, and not on subsequent update renders, similar to the `componentDidMount` lifecycle method in class components.

If you list some variables in the array, `useEffect` will re-run the side effect function whenever any of these variables change.

Within the side effect function, you can optionally return a function. This returned function will be executed before the component unmounts, or before the next side effect function runs. It's similar to the `componentWillUnmount` lifecycle method in class components and can be used to clean up side effects, such as canceling subscriptions.

Here's an example of `useEffect` for fetching user data:

```jsx
import React, { useState, useEffect } from 'react';

function UserProfile({ userId }) {
  const [user, setUser] = useState(null);

  useEffect(() => {
    const fetchUser = async () => {
      const response = await fetch(`/api/users/${userId}`);
      const data = await response.json();
      setUser(data);
    };
    fetchUser();
  }, [userId]);  // When userId changes, re-fetch user data

  if (user) {
    return <div>{user.name}</div>;
  } else {
    return <div>Loading...</div>;
  }
}
```

In this example, when the `userId` changes, `useEffect` triggers the operation to re-fetch the user data.


The `fetchUser();` line of code is simply a function call. Here's a breakdown of the surrounding structure:

1. `fetchUser` is defined as an `async` function inside the `useEffect` hook. It uses the `fetch` API to get data from the `/api/users/${userId}` URL, where `${userId}` is a placeholder for the user ID passed as a prop to the `UserProfile` component.

    ```jsx
    const fetchUser = async () => {
      const response = await fetch(`/api/users/${userId}`);
      const data = await response.json();
      setUser(data);
    };
    ```

    The `async/await` syntax is used here to handle the Promise returned by the `fetch` API in an asynchronous manner, allowing the function to wait for the Promise to resolve before moving on to the next line of code. 

    The `response.json()` line is also asynchronous, as it waits for the entire response body to be read before it's parsed into a JavaScript object.

    Once the data is fetched and parsed, the `setUser` function (from the `useState` hook) is called to update the `user` state with the fetched data.

2. After the `fetchUser` function is defined, it's immediately called with the line `fetchUser();`. This line initiates the data fetching process.

This pattern of defining an `async` function inside a `useEffect` hook and then immediately calling it is quite common in React, because the `useEffect` hook itself cannot be made asynchronous. This pattern allows you to handle asynchronous operations, like data fetching, inside a `useEffect` hook.


## TypeScript

![TypeScript](README-pics/typescript.png)


## Axios & Fetch

`fetch` and `axios` are both JavaScript libraries used for making HTTP requests in the browser. Although they can be used for the same tasks, there are some differences between them. 

Here are some of the main differences between `fetch` and `axios`:

1. **Error Handling**: `fetch` only rejects a promise when a network failure or anything that prevented the request from completing occurs. It won't reject if the HTTP status code indicates a failure, such as 404 or 500. You have to handle these cases yourself. On the other hand, `axios` automatically rejects the promise if the HTTP status code indicates a failure, making error handling easier.

2. **Browser Compatibility**: `fetch` is a newer API, and some older browsers may not support it (e.g., Internet Explorer). However, `axios` is supported in all modern browsers and even some older ones, including Internet Explorer.

3. **Data Parsing**: With `fetch`, you need to manually parse the returned JSON data, like this: `fetch(url).then(response => response.json())`. In contrast, `axios` automatically transforms the response data to JSON, so you can directly receive JSON data like this: `axios(url).then(response => console.log(response.data))`.

4. **Timeouts**: `axios` allows you to set a request timeout, but `fetch` doesn't.

5. **Cancellation**: `axios` provides a simpler way to cancel requests. While `fetch` doesn't support cancellation directly, it can be achieved through the `AbortController` API.

6. **XSRF Protection**: `axios` has a built-in mechanism to defend against cross-site request forgery (XSRF).

## CORS Policy

`Access to XMLHttpRequest at 'http://localhost:5000/api/activities' from origin 'http://localhost:3000' has been blocked by CORS policy: No 'Access-Control-Allow-Origin' header is present on the requested resource.`

CORS, or Cross-Origin Resource Sharing, is a mechanism that allows many resources (e.g., fonts, JavaScript, etc.) on a web page to be requested from another domain outside the domain from which the resource originated.

In the context of web applications, the term "origin" is defined as a combination of URI scheme (http, https), hostname (example.com), and port number (3000). When a document or script from one origin attempts to access resources from another origin, the browser's same-origin policy comes into play. The same-origin policy is a security measure that restricts how a document or script loaded from one origin can interact with a resource from another origin.

To mitigate this, CORS was introduced to allow controlled access to resources located outside of a given origin. In simple terms, CORS handles cross-origin requests by adding specific HTTP headers that tell the browser if it's permissible to access the requested resource.

In your case, the error message indicates that an XMLHttpRequest to 'http://localhost:5000/api/activities' from 'http://localhost:3000' was blocked by the browser due to the server's CORS policy. The server at 'http://localhost:5000' does not include the necessary Access-Control-Allow-Origin header in its response, so the browser denies the request.

```cs
builder.Services.AddCors(opt => 
{
    opt.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:3000");
    });
});



app.UseCors("CorsPolicy");
```

## React.StrictMode

`<React.StrictMode>` 是一个用于帮助开发人员在开发过程中检查潜在问题的React包装组件。这个组件不会对生产环境的构建产生影响，只会在开发模式下运行，帮助你找出应用中的潜在问题。

它主要进行以下检查：

1. **识别过时或不建议使用的生命周期方法：** 如 `componentWillMount`，`componentWillReceiveProps` 和 `componentWillUpdate` 这些在 React 17 之后将会被弃用的生命周期方法。使用了这些方法的组件在严格模式下会在控制台中打印警告。

2. **警告使用了不安全的生命周期钩子：** `<React.StrictMode>` 会帮助找出应用中使用了不安全的生命周期钩子的地方。

3. **检查意外的副作用：** 严格模式下，生命周期方法以及 `render` 方法会被执行两次（这只会在开发模式下发生，不会影响生产环境）。这有助于开发者捕获到在这些方法中可能出现的意外副作用。

4. **检查使用过时的 context API：** 新的 Context API 是 React 16.3 版本新添加的，如果你的项目中还在使用过时的 context API，`<React.StrictMode>` 会在控制台中打印警告。

5. **检测是否有使用废弃的ref API：** 早期的版本中，字符串（legacy string ref）被用作 ref ，现在已经被回调函数和 `createRef` API 取代。如果你的应用中仍然使用了字符串 ref ，`<React.StrictMode>` 会在控制台打印警告。

6. **检查是否在组件中误用了 DOM 属性：** 例如，某些无效的属性值被传递给了 DOM 的元素。

这些检查有助于发现和修正开发过程中可能犯的错误，让应用的代码质量更高、更易于维护。


## Semantic UI


`npm install semantic-ui-react semantic-ui-css`

`import 'semantic-ui-css/semantic.min.css'`

# Creating a CRUD application using the CQRS + Mediator pattern

Creating a CRUD (Create, Read, Update, Delete) application using the CQRS (Command Query Responsibility Segregation) and Mediator pattern involves several steps. The basic idea is to divide your application's data operations into "Commands" (for modifying data) and "Queries" (for reading data), and use a Mediator to decouple the in-process sending of messages from handling messages.

Here's a high-level overview of the steps you might take:

1. **Define your Domain Models:** These are the entities your application will be working with. For a blog application, for example, you might have entities like `Post`, `Author`, etc.

2. **Setup the Database Context:** You would typically use an Object-Relational Mapping (ORM) tool like Entity Framework Core to interact with your database. You define a `DbContext` that includes `DbSet` properties for each of your entities.

3. **Implement the CQRS Pattern:** Create separate models for reads (Queries) and writes (Commands). These models represent the inputs and outputs for each operation your application can perform. For a blog application, you might have commands like `CreatePostCommand`, `UpdatePostCommand`, and `DeletePostCommand`, and queries like `GetPostQuery` and `GetAllPostsQuery`.

4. **Create Command and Query Handlers:** These are classes that handle the execution of commands and queries. They contain the business logic that's executed when a command or query is processed. 

5. **Use a Mediator:** The Mediator pattern is used to decouple the sending of messages (i.e., commands and queries) from the handling of those messages. This makes it easier to maintain and test your code. A popular .NET library that implements the Mediator pattern is MediatR. You can create an instance of `IMediator` in your controller and use it to send commands and queries.

6. **Create a Controller:** The controller is responsible for handling HTTP requests and sending the appropriate commands or queries to the Mediator.

7. **Setup a Client:** Finally, you can create a client application (like a React or Angular app) to interact with your API.

Remember that while the CQRS pattern can be useful, it may not be suitable for every application. It's best suited to complex applications where there is a clear benefit to separating read and write operations and you need to maximize performance, scalability, and security.

![cleanArchitecture](README-pics/cleanArchitecture.png)


![CQRS](README-pics/CRQS1.png)

![CQRS](README-pics/CRQS2.png)

![CQRS](README-pics/CRQS3.png)