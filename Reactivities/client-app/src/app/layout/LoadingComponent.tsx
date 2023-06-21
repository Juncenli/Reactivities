import React from 'react';
import {Dimmer, Loader} from "semantic-ui-react";

interface Props {
    inverted?: boolean;
    content: string;
}

export default function LoadingComponent({inverted = true, content = 'Loading...'}: Props) {
    return (
        <Dimmer active={true} inverted={inverted}>
            <Loader content={content} />
        </Dimmer>
    )
}

// This module is a React component named `LoadingComponent`. Its purpose is to display a loading state within your application.

// This component accepts two props:

// 1. `inverted`: An optional boolean that determines the color of the loading animation. If `inverted` is `true`, the loader will be light-colored; otherwise, it will be dark. If this prop is not provided, its default value will be `true`.

// 2. `content`: A string that indicates the text displayed underneath the loading animation. If this prop is not provided, its default value will be 'Loading...'

// In the component's return value, it renders a `Dimmer` component from the Semantic UI React library, which darkens the area behind it, often used to indicate loading status or to emphasize a particular part of the UI. Inside the `Dimmer` component, it also renders a `Loader` component, which is used to display a loading animation.

// The `Dimmer` component accepts two props:

// 1. `active`: A boolean that determines whether the `Dimmer` component should be displayed. In this case, it is always `true`, indicating that the `Dimmer` component should always be shown.

// 2. `inverted`: A boolean passed in from the outer props, determining the color of the loading animation.

// The `Loader` component accepts a `content` prop, which sets the text displayed underneath the loading animation.

// Overall, `LoadingComponent` is a component used to display a loading state, providing a configurable way to change the color of the loading animation and the text displayed underneath.