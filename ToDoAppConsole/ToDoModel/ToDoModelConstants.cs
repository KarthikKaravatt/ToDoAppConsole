namespace ToDoApplicationConsole.ToDoModel
{
    internal static class ToDoModelConstants
    {
        public const string GlobalGroupName = "GlobalGroup";
        public const string SaveDataFilePath = "./data/ToDoCollections.txt";
        public const string ToDoFileGroupSeparator = "---";
        public const string ToDoCompletedSeparator = "+++";
        public const string ToDoStringSeparator = "|";

        //Error messages
        public const string GroupDoesNotExist = "Error: Group does not exist";
        public const string TaskDoesNotExist = "Error: Task does not exist";
        public const string TaskAlreadyExists = "Error: Task already exisits";
        public const string GroupAlreadyExists = "Error: Group already exisits";
        public const string InvalidTaskName = "Error: Invalid task name";
        public const string InvalidGroupName = "Error: Invalid group name";

        //Success messages
        public const string TaskAdded = "Sucess: Task added";
        public const string GroupRemoved = "Success: Group removed";
        public const string GroupAdded = "Success: Group added";
        public const string TaskRemoved = "Success: Task removed";
        public const string TaskCompleted = "Success: Task completed";
        public const string TaskCompletionUnDone = "Success: Task un-completed";
        public const string CompletedTaskAdded = "Success: Completed task added";
    }
}
