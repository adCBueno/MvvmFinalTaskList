using System.Collections.Generic;

namespace Core
{
    public interface ITaskItemService
    {
        List<TaskItem> GetTaskItems();

        TaskItem AddTaskItem(TaskItem taskItem);

        void DeleteTaskItem(TaskItem taskItem);

        void UpdateTaskItem(TaskItem taskItem);
    }
}
