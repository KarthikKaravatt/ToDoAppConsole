using System.Text.Json;
using System.IO;
using System.Security.Permissions;
using System.Security;
namespace ToDoApplicationConsole.ToDoModel
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
        /// <param name="listGroups">The list groups.</param>
        /// <param name="completedTasks">The completed tasks.</param>
        public ToDoListCollections(Dictionary<string, ToDoList> listGroups, Dictionary<string, ToDoList> completedTasks)
        {
            _listGroups = listGroups;
            // Default Group
            _listGroups.Add(ToDoModelConstants.GlobalGroupName,
                new ToDoList(new SortedSet<ToDoTask>(new TaskCompletionDateComparer()),
                    new SortedSet<ToDoTask>(new TaskCompletionDateComparer())));
        }

        public void SaveListsToFile(string path = ToDoModelConstants.SaveDataFilePath)
        {
            string fileString = ToDoModelConstants.ToDoFileGroupSeparator;
            foreach (KeyValuePair<string, ToDoList> listGroup in _listGroups)
            {
                fileString += "\n" + listGroup.Key;
                foreach (string task in listGroup.Value.GetTaskData())
                {
                    fileString += "\n" + task;
                }

                fileString += "\n" + ToDoModelConstants.ToDoFileGroupSeparator;
            }

            Console.WriteLine(fileString);
        }



        /// <summary>
        /// Adds the list group.
        /// </summary>
        /// <param name="name">The name of the group.</param>
        /// <returns> A tuple containing a boolean indicating success and a message description.</returns>
        public (bool success, string message) AddListGroup(string name)
        {
            if (name == ToDoModelConstants.ToDoFileGroupSeparator) return (false, ToDoModelConstants.InvalidGroupName);
            if (_listGroups.ContainsKey(name)) return (false, ToDoModelConstants.GroupAlreadyExists);
            _listGroups.Add(name,
                new ToDoList(new SortedSet<ToDoTask>(new TaskCompletionDateComparer()),
                    new SortedSet<ToDoTask>(new TaskCompletionDateComparer())));
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
                    new ToDoList(new SortedSet<ToDoTask>(new TaskCompletionDateComparer()),
                        new SortedSet<ToDoTask>(new TaskCompletionDateComparer())));
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
            if (completionDateTime == default) completionDateTime = DateTime.MaxValue;
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

        /// <summary>
        /// Removes the completed task.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="group">The group.</param>
        /// <returns>A tuple with a boolean indicating success and a string message as a description</returns>
        public (bool success, string message) RemoveCompletedTask(string name, string group = ToDoModelConstants.GlobalGroupName)
        {
            return !_listGroups.ContainsKey(group)
                ? (false, ToDoModelConstants.GroupDoesNotExist)
                : _listGroups[group]
                    .RemoveCompleted(name);
        }

        /// <summary>
        /// Completes the task.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="group">The group.</param>
        /// <returns>A tuple containing a boolean indication success and a string indication description</returns>
        public (bool success, string message) CompleteTask(string name, string group = ToDoModelConstants.GlobalGroupName)
        {
            return !_listGroups.ContainsKey(group)
                ? (false, ToDoModelConstants.GroupDoesNotExist)
                : _listGroups[group]
                    .Complete(name);
        }

        /// <summary>
        /// Uns the do completed.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="groupName">Name of the group.</param>
        /// <returns>A tuple with a boolean indicating success and a string as a description</returns>
        public (bool success, string message) UnDoCompleted(string name, string groupName = ToDoModelConstants.GlobalGroupName)
        {
            return !_listGroups.ContainsKey(groupName)
                ? (false, ToDoModelConstants.GroupDoesNotExist)
                : _listGroups[groupName]
                    .UndoCompleted(name);
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
                Console.WriteLine("Completed:");
                keyValuePair.Value.PrintCompleted();
                Console.WriteLine("\n");
            }
        }
    }
}