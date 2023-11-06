namespace ToDoApplicationConsole.ToDoModel
{
    /// <summary>
    /// A collection of ToDoList objects organized by groups
    /// </summary>
    internal class ToDoListCollections
    {
        private readonly Dictionary<string, ToDoList> _listGroups;

        private readonly Dictionary<string, ToDoList> _completedTasks;

        /// <summary>
        /// Initializes a new instance of the <see cref="ToDoListCollections"/> class.
        /// </summary>
        /// <param name="listGroups">The list groups.</param>
        /// <param name="completedTasks">The completed tasks.</param>
        public ToDoListCollections(Dictionary<string, ToDoList> listGroups, Dictionary<string, ToDoList> completedTasks)
        {
            _listGroups = listGroups;
            _completedTasks = completedTasks;
            // Default Group
            _listGroups.Add(ToDoModelConstants.GlobalGroupName,
                new ToDoList(new SortedSet<ToDoTask>(new TaskCompletionDateComparer())));
            _completedTasks.Add(ToDoModelConstants.GlobalGroupName,
                new ToDoList(new SortedSet<ToDoTask>(new TaskCompletionDateComparer())));
        }

        /// <summary>
        /// Adds the list group.
        /// </summary>
        /// <param name="name">The name of the group.</param>
        /// <returns> A tuple containing a boolean indicating success and a message description.</returns>
        public (bool success, string message) AddListGroup(string name)
        {
            if (_listGroups.ContainsKey(name))
            {
                return (false, ToDoModelConstants.GroupAlreadyExists);
            }
            _listGroups.Add(name, new ToDoList(new SortedSet<ToDoTask>(new TaskCompletionDateComparer())));
            return (true, ToDoModelConstants.GroupAdded);
        }

        /// <summary>
        /// Removes the list group.
        /// </summary>
        /// <param name="name">The name of the group.</param>
        /// <returns>Tuple containing boolean indicating success and string description.</returns>
        public (bool success, string message) RemoveListGroup(string name)
        {
            if (!_listGroups.ContainsKey(name)) return (false, ToDoModelConstants.GroupDoesNotExist);
            _listGroups.Remove(name);
            // Do not allow the deletion of the GlobalGroup
            if (name == ToDoModelConstants.GlobalGroupName)
            {
                _listGroups.Add(ToDoModelConstants.GlobalGroupName,
                    new ToDoList(new SortedSet<ToDoTask>(new TaskCompletionDateComparer())));
            }
            return (true, ToDoModelConstants.GroupRemoved);
        }


        /// <summary>
        /// Adds the task to a group with a completion date.
        /// </summary>
        /// <param name="name">The name of a task.</param>
        /// <param name="completionDateTime">The completion date time.</param>
        /// <param name="group">The group.</param>
        /// <returns> A tuple with a boolean to indicate success and a description</returns>
        public (bool success, string message) AddTask(string name, string group = ToDoModelConstants.GlobalGroupName, DateTime completionDateTime = default)
        {
            if(completionDateTime == default) completionDateTime = DateTime.MaxValue;
            if (!_listGroups.ContainsKey(group)) return (false, ToDoModelConstants.GroupDoesNotExist);
            ToDoList list = _listGroups[group];
            return list.Add(new ToDoTask(name, completionDateTime));
        }

        /// <summary>
        /// Removes the task from a task group.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="group">The group.</param>
        /// <returns>A tuple with a boolean indicating success and a description string</returns>
        public (bool success, string message) RemoveTask(string name, string group = ToDoModelConstants.GlobalGroupName)
        {
            return !_listGroups.ContainsKey(group)
                ? (false, ToDoModelConstants.GroupDoesNotExist)
                : _listGroups[group]
                    .Remove(name);
        }

        //TODO: Delete this, for testing only
        public void PrintGroups()
        {
            foreach (var keyValuePair in _listGroups)
            {
                Console.WriteLine(keyValuePair.Key);
                Console.WriteLine("-------------");
                keyValuePair.Value.PrintList();
                Console.WriteLine("\n");
            }
        }
    }
}