namespace ToDoAppConsole.ToDoModel
{
    /// <summary>
    /// Represents a  collection of ToDoTasks
    /// </summary>
    internal class ToDoList
    {
        private readonly SortedSet<ToDoTask> _tasks;

        /// <summary>
        /// Initializes a new instance of the <see cref="ToDoList"/> class.
        /// </summary>
        /// <param name="tasks">The tasks.</param>
        public ToDoList(SortedSet<ToDoTask> tasks)
        {
            _tasks = tasks;
        }

        /// <summary>
        /// Adds the specified task.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <returns>True if task was added successfully or false if addition failed.</returns>
        public (bool success, string message) Add(ToDoTask task)
        {
            if (_tasks.Contains(task))
            {
                return (false, ToDoModelConstants.TaskDoesNotExist);
            }
            _tasks.Add(task);
            return (true, ToDoModelConstants.TaskAdded);
        }

        /// <summary>
        /// Removes the specified task name.
        /// </summary>
        /// <param name="taskName">Name of the task.</param>
        /// <returns>True if removal was successful, false if removal failed</returns>
        public (bool success, string message) Remove(string taskName)
        {
            ToDoTask? taskToRemove = _tasks.FirstOrDefault(task => task.Name == taskName);

            if (taskToRemove == null)
            {
                return (false, ToDoModelConstants.TaskDoesNotExist);
            }
            _tasks.Remove(taskToRemove);
            return (true, ToDoModelConstants.TaskAdded);
        }


        // TODO: Delete this later, only for testing
        public void PrintList()
        {
            foreach (var item in _tasks)
            {
                Console.WriteLine(item.Name);
            }
        }

    }
}
