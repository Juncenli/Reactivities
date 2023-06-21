// This is going to contain all of our requests that go to our API

import axios, { AxiosResponse } from 'axios';
import { Activity } from '../models/activity';

const sleep = (delay: number) => {
    return new Promise((resolve) => {
        setTimeout(resolve, delay);
    })
}

axios.defaults.baseURL = 'http://localhost:5000/api';

const responseBody = <T>(response: AxiosResponse<T>) => response.data;

axios.interceptors.response.use(async response => {
    try {
        await sleep(1000);
        return response;
    } catch (error) {
        console.log(error);
        return await Promise.reject(error)
    }
})

const requests = {
    get: <T>(url: string) => axios.get<T>(url).then(responseBody),
    post: <T>(url: string, body: {}) => axios.post<T>(url, body).then(responseBody),
    put: <T>(url: string, body: {}) => axios.put<T>(url, body).then(responseBody),
    del: <T>(url: string) => axios.delete<T>(url).then(responseBody)
}

const Activities = {
    list: () => requests.get<Activity[]>(`/activities`),
    details: (id: string) => requests.get<Activity>(`/activities/${id}`),
    create: (activity: Activity) => requests.post<void>(`/activities`, activity),
    update: (activity: Activity) => requests.put<void>(`/activities/${activity.id}`, activity),
    delete: (id: string) => requests.del<void>(`/activities/${id}`)
}

const agent = {
    Activities
}

export default agent;


// 这个模块是一个封装了axios HTTP请求的服务模块，其目的是对服务端的API进行交互。

// 首先，你定义了一个名为`sleep`的函数，该函数返回一个Promise，会在给定的延迟（以毫秒为单位）后被解析。这可以用来模拟网络延迟。

// 然后，你为axios设置了一个默认的基本URL，该URL用于所有的HTTP请求。

// 接下来，你创建了一个`responseBody`函数，该函数从axios的响应中提取出数据。

// 你还为axios的响应添加了一个拦截器，该拦截器在每个请求返回后暂停1秒（模拟网络延迟），然后返回响应。

// 你为HTTP GET, POST, PUT, DELETE请求创建了一个通用的`requests`对象，这个对象对axios的各个方法进行了封装，以便于在后续的代码中重复使用。

// 在`Activities`对象中，你创建了五个函数，分别用于获取活动列表、获取单个活动详情、创建新的活动、更新现有的活动和删除活动。这些函数都使用了前面创建的`requests`对象中的方法，并向对应的API endpoint发送请求。

// 最后，你创建了一个`agent`对象，包含了`Activities`对象，并将这个`agent`对象导出，以便在应用的其他部分使用这些函数。

// 这种模式的主要好处是，它将所有的HTTP请求逻辑集中在一个地方，使得代码更加清晰，易于维护。同时，它提供了一种统一的方式来处理网络延迟和错误处理。