using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApplicationConsole.ToDoModel
{
    internal static class ToDoModelConstants
    {
        public const string GlobalGroupName = "GlobalGroup";

        //Error messages
        public const string GroupDoesNotExist = "Error: Group does not exist";
        public const string TaskDoesNotExist = "Error: Task does not exist";
        public const string TaskAlreadyExists = "Error: Task already exisits";
        public const string GroupAlreadyExists = "Error: Group already exisits";

        //Success messages
        public const string TaskAdded = "Sucess: Task added";
        public const string GroupRemoved = "Success: Group removed";
        public const string GroupAdded = "Success: Group added";
        public const string TaskRemoved = "Success: Task removed";
        public const string TaskCompleted = "Success: Task completed";
        public const string TaskCompletionUnDone = "Success: Task un-completed";
    }
}
