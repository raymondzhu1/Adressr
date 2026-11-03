import type { DashboardTaskMiniResponse } from "../types/Task/task.types";

function StatusLine({ done, text }: {done: boolean; text: string; }) {
    return(
        <>
            <span className={done ? "mark mark--done" : "mark"} aria-hidden="true" />
            <span>
                <span className="visually-hidden">{done ? "Done: " : "Not done: "}</span>
                {text}
            </span>
        </>
    );
}

function TaskArea({ task }: { task: DashboardTaskMiniResponse }) {
    return(
        <li className={task.isDone ? "task-area task-area--complete" : "task-area"}>
            <div className="task-area__row">
                <StatusLine done={task.isDone} text={task.description} />
            </div>

            {task.subTasks.length > 0 && (
                <ul className="subtasks">
                    {task.subTasks.map((subTask) => (
                        <li key={subTask.workTaskID} className="subtask">
                            <StatusLine done={subTask.isDone} text={subTask.description} />
                        </li>
                    ))}
                </ul>
            )}
        </li>
    );
}

export function TaskAreaList({ tasks }: { tasks: DashboardTaskMiniResponse[] }) {
    if(tasks.length === 0) {
        return null;
    }
    
    return(
        <ul className="task-areas">
            {tasks.map((task) => (
                <TaskArea key={task.workTaskID} task={task} />
            ))}
        </ul>
    );
}