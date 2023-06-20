import { Grid } from "semantic-ui-react";
import { Activity } from "../../../app/models/activity";
import ActivityDetails from "../details/ActivityDetails";
import ActivityForm from "../form/ActivityForm";
import ActivityList from './ActivityList';


/*

这个 `ActivityDashboard` 组件是一个功能丰富的组件，它包含了一些用于处理和展示 "活动"（Activities）的相关操作。

这个组件接收一些属性（Props），包括：

- `activities`：一个Activity数组，这些Activity可能会在页面上以列表的形式显示。
- `selectedActivity`：被选中的Activity。
- `selectActivity`、`cancelSelectActivity`：用于选中和取消选中Activity的函数。
- `openForm`、`closeForm`：用于打开和关闭表单的函数。
- `editMode`：一个布尔值，表示是否处于编辑模式。
- `createOrEdit`：用于创建或编辑Activity的函数。
- `deleteActivity`：用于删除Activity的函数。

在组件的渲染方法（return部分）中，它创建了一个由Semantic UI React库提供的Grid组件。在这个Grid组件中，它有两个列（Column）：

- 左边的列宽为10，主要展示一个 `ActivityList` 组件，显示所有的活动并且允许用户选择或删除特定的活动。
- 右边的列宽为6，这个列根据 `selectedActivity` 和 `editMode` 的状态，可能会显示一个 `ActivityDetails` 组件（用于展示选中活动的详细信息）或者一个 `ActivityForm` 组件（用于编辑选中的活动）。

简单来说，这个 `ActivityDashboard` 组件是一个活动管理的仪表板，它允许用户查看活动列表，查看选中活动的详情，编辑选中的活动，以及删除活动。
*/
interface Props {
    activities: Activity[];
    selectedActivity: Activity | undefined;
    selectActivity: (id: string) => void;
    cancelSelectActivity: () => void;
    openForm: (id: string) => void;
    closeForm: () => void;
    editMode: boolean;
    createOrEdit: (activity: Activity) => void;
    deleteActivity: (id: string) => void;
}

export default function ActivityDashboard({ activities, selectedActivity, selectActivity,
    cancelSelectActivity, openForm, closeForm, editMode, createOrEdit, deleteActivity }: Props) {
    return (
        <Grid>
            <Grid.Column width='10'>
                <ActivityList 
                    activities={activities} 
                    selectActivity={selectActivity}
                    deleteActivity={deleteActivity} 
                />
            </Grid.Column>
            <Grid.Column width='6'>
                {selectedActivity && !editMode &&
                    <ActivityDetails
                        activity={selectedActivity}
                        cancelSelectActivity={cancelSelectActivity}
                        openForm={openForm}
                    />}
                {editMode &&
                    <ActivityForm 
                        closeForm={closeForm} 
                        activity={selectedActivity} 
                        createOrEdit={createOrEdit} />}
            </Grid.Column>

        </Grid>
    )
}