namespace ToDoAppConsole.ToDoModel
{
    internal class ToDoTask
    {
        private string _name;
        private DateTime _completionDate;

        public ToDoTask(string name)
        {
            _name = name;
            // Give max value to tasks wihout completionDate
            _completionDate = DateTime.MaxValue;
        }

        public ToDoTask(string name, DateTime completionDate) : this(name)
        {
            _completionDate = completionDate;
        }

        public string GetName { get { return _name; } }
        public void SetName(string name) { _name = name; }

        public void SetCompletionDate(DateTime completionDate) { _completionDate = completionDate; }

        public DateTime GetCompletionDate() { return _completionDate; }


    }
    internal class TaskCompletionDateComparer : IComparer<ToDoTask>
    {
        public int Compare(ToDoTask? x, ToDoTask? y)
        {
            if (x == null || y == null) return 0;

            return x.GetCompletionDate().CompareTo(y.GetCompletionDate());
        }
    }
}
