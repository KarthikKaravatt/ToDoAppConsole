namespace ToDoAppConsole.ToDoModel
{
    internal class ToDoList
    {
        private SortedSet<ToDoTask> _tasks;

        public ToDoList(SortedSet<ToDoTask> tasks)
        {
            _tasks = tasks;
        }

        public void Add(ToDoTask task)
        {
            if (_tasks.Contains(task))
            {
                // Throw an excpetion
            }
            else
            {
                _tasks.Add(task);
            }
        }

        public void Remove(string taskName)
        {
            ToDoTask? taskToRemove = _tasks.FirstOrDefault(task => task.GetName == taskName);

            if (taskToRemove == null)
            {
                // Throw an exception
            }
            else
            {
                _tasks.Remove(taskToRemove);
            }
        }

        // TODO: Delete this later, only for testing
        public void printList()
        {
            foreach (var item in _tasks)
            {
                Console.WriteLine(item.GetName);
            }
        }

    }
}
