using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoAppConsole.ToDoModel
{
    internal static class ToDoModelConstants
    {
        public const string GlobalGroupName = "GlobalGroup";

        //Error messages
        public const string GroupDoesNotExist = "Error: Group does not exist";
        public const string TaskDoesNotExist = "Error: Task does note exist";
        public const string GroupAlreadyExists = "Error: Group already exisits";

        //Success messages
        public const string TaskAdded = "Sucess: Task added";
        public const string GroupRemoved = "Success: Group removed";
        public const string GroupAdded = "Success: Group added";
    }
}
