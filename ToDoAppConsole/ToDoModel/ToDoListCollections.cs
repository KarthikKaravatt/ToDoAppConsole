using System.Collections;

namespace ToDoAppConsole.ToDoModel
{
    /// <summary>
    /// A collection of ToDoList objects organized by groups
    /// </summary>
    internal class ToDoListCollections
    {
        private readonly Dictionary<string, ToDoList> _listGroups;

        /// <summary>
        /// Initializes a new instance of the <see cref="ToDoListCollections"/> class.
        /// </summary>
        /// <param name="listGroups">Contains the ToDoLists.</param>
        public ToDoListCollections(Dictionary<string, ToDoList> listGroups)
        {
            _listGroups = listGroups;
            // Default Group
            listGroups.Add(ToDoModelConstants.GlobalGroupName, new ToDoList(new SortedSet<ToDoTask>()));
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
            _listGroups.Add(name, new ToDoList(new SortedSet<ToDoTask>()));
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
            return (true, ToDoModelConstants.GroupRemoved);
        }

        /// <summary>
        /// Adds the task to the global list.
        /// </summary>
        /// <param name="name">The name of the task.</param>
        /// <returns>Tuple with boolean indicating success and a description message</returns>
        public (bool success, string message) AddTask(string name)
        {
            // This should never happen, but you never know
            if (!_listGroups.ContainsKey(ToDoModelConstants.GlobalGroupName)) return (false, ToDoModelConstants.GroupDoesNotExist);
            ToDoList list = _listGroups[ToDoModelConstants.GlobalGroupName];
            return list.Add(new ToDoTask(name));
        }

        /// <summary>
        /// Adds the task to the global list with a time of completion.
        /// </summary>
        /// <param name="name">The name of the task.</param>
        /// <param name="completionDateTime">The completion date time.</param>
        /// <returns>Tuple with boolean indicating success and a description message</returns>
        public (bool success, string message) AddTask(string name, DateTime completionDateTime)
        {
            if (!_listGroups.ContainsKey(ToDoModelConstants.GlobalGroupName)) return (false, ToDoModelConstants.GroupDoesNotExist);
            ToDoList list = _listGroups[ToDoModelConstants.GlobalGroupName];
            return list.Add(new ToDoTask(name, completionDateTime));
        }

        /// <summary>
        /// Adds the task to a group.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="group">The group.</param>
        /// <returns>
        /// A tuple containing a boolean indicating success if true and failure if false
        /// and a string indicating reason of failure, or an indication of success
        /// </returns>
        public (bool success, string message) AddTask(string name, string group)
        {
            if (!_listGroups.ContainsKey(name)) return (false, ToDoModelConstants.GroupDoesNotExist);
            ToDoList list = _listGroups[name];
            return list.Add(new ToDoTask(name));
        }

        /// <summary>
        /// Adds the task to a group with a completion date.
        /// </summary>
        /// <param name="name">The name of a task.</param>
        /// <param name="completionDateTime">The completion date time.</param>
        /// <param name="group">The group.</param>
        /// <returns> A tuple with a boolean to indicate success and a description</returns>
        public (bool success, string message) AddTask(string name,DateTime completionDateTime, string group)
        {
            if (!_listGroups.ContainsKey(name)) return (false, ToDoModelConstants.GroupDoesNotExist);
            ToDoList list = _listGroups[name];
            return list.Add(new ToDoTask(name, completionDateTime));
        }

        //TODO: Delete this, for testing only
        public void PrintGroups()
        {
            foreach (var keyValuePair in _listGroups)
            {
                Console.WriteLine(keyValuePair.Key);
            }
        }
    }
}
