using System.Collections;

namespace ToDoApplicationConsole.ToDoModel
{
    /// <summary>
    /// Represents a  collection of ToDoTasks
    /// </summary>
    internal class ToDoList
    {
        // Tasks are separated
        private readonly SortedSet<ToDoTask> _tasks;

        private readonly SortedSet<ToDoTask> _completedTasks;


        /// <summary>
        /// Initializes a new instance of the <see cref="ToDoList"/> class.
        /// </summary>
        /// <param name="tasks">The tasks.</param>
        /// <param name="completedTasks">The completed tasks.</param>
        internal ToDoList(SortedSet<ToDoTask> tasks, SortedSet<ToDoTask> completedTasks)
        {
            _tasks = tasks;
            _completedTasks = completedTasks;
        }

        /// <summary>
        /// Adds the specified task.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <returns>True if task was added successfully or false if addition failed.</returns>
        internal (bool success, string message) Add(ToDoTask task)
        {
            if (task.Name == ToDoModelConstants.ToDoFileGroupSeparator) return (false, ToDoModelConstants.InvalidTaskName);
            if (_tasks.Contains(task)) return (false, ToDoModelConstants.TaskAlreadyExists);
            // If Task was previously completed then un-complete it
            if (_completedTasks.Contains(task)) _completedTasks.Remove(task);
            _tasks.Add(task);
            return (true, ToDoModelConstants.TaskAdded);
        }

        /// <summary>
        /// Removes the specified task.
        /// </summary>
        /// <param name="taskName">Name of the task.</param>
        /// <returns>True if removal was successful, false if removal failed</returns>
        internal (bool success, string message) Remove(string taskName)
        {
            ToDoTask? taskToRemove = _tasks.FirstOrDefault(task => task.Name == taskName);

            if (taskToRemove == null)
            {
                return (false, ToDoModelConstants.TaskDoesNotExist);
            }
            _tasks.Remove(taskToRemove);
            return (true, ToDoModelConstants.TaskRemoved);
        }

        /// <summary>
        /// Completes the specified task.
        /// </summary>
        /// <param name="taskName">Name of the task.</param>
        /// <returns>A tuple with a boolean indicating success and a string message description.</returns>
        internal (bool success, string message) Complete(string taskName)
        {
            ToDoTask? taskToRemove = _tasks.FirstOrDefault(task => task.Name == taskName);

            if (taskToRemove == null)
            {
                return (false, ToDoModelConstants.TaskDoesNotExist);
            }
            _completedTasks.Add(taskToRemove);
            _tasks.Remove(taskToRemove);
            return (true, ToDoModelConstants.TaskCompleted);
        }

        /// <summary>
        /// Adds the task to the completed task, used when reading the file.
        /// </summary>
        /// <param name="completeTask">The complete task.</param>
        /// <returns></returns>
        internal (bool success, string message) AddCompletedTask(ToDoTask completeTask)
        {
            if (_tasks.FirstOrDefault(task => task.Name == completeTask.Name) != null) return (false, ToDoModelConstants.TaskAlreadyExists);
            if (_completedTasks.FirstOrDefault(task => task.Name == completeTask.Name) != null) return (false, ToDoModelConstants.TaskAlreadyExists);
            _completedTasks.Add(completeTask);
            return (true, ToDoModelConstants.CompletedTaskAdded);

        }

        /// <summary>
        /// Removes the completed task.
        /// </summary>
        /// <param name="taskName">Name of the task.</param>
        /// <returns>A tuple with a boolean that indicates success and a string message description.</returns>
        internal (bool success, string message) RemoveCompleted(string taskName)
        {

            ToDoTask? taskToComplete = _completedTasks.FirstOrDefault(task => task.Name == taskName);

            if (taskToComplete == null)
            {
                return (false, ToDoModelConstants.TaskDoesNotExist);
            }
            _completedTasks.Remove(taskToComplete);
            return (true, ToDoModelConstants.TaskRemoved);
        }
        /// <summary>
        /// Undoes a completed task.
        /// </summary>
        /// <param name="taskName">Name of the task.</param>
        /// <returns>A tuple containing a boolean indicating success and string message description.</returns>
        internal (bool success, string message) UndoCompleted(string taskName)
        {
            ToDoTask? taskToUndo = _completedTasks.FirstOrDefault(task => task.Name == taskName);
            if (taskToUndo == null) return (false, ToDoModelConstants.TaskDoesNotExist);
            _completedTasks.Remove(taskToUndo);
            _tasks.Add(taskToUndo);
            return (true, ToDoModelConstants.TaskCompletionUnDone);
        }

        internal List<string> GetTaskData()
        {
            List<string> tasks = new List<string>();
            foreach (var item in _tasks)
            {
                tasks.Add(item.ToString());
            }
            return tasks;
        }


        // TODO: Delete this later, only for testing
        internal void PrintList()
        {
            foreach (var item in _tasks)
            {
                Console.WriteLine(item.Name);
            }
        }

       internal void PrintCompleted()
        {
            foreach (var toDoTask in _completedTasks)
            {
                Console.WriteLine(toDoTask.Name);
            }
        }

    }
}
