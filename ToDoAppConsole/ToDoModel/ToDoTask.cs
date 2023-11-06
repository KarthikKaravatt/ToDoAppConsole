namespace ToDoApplicationConsole.ToDoModel
{
    /// <summary>
    /// Represents a task in a to-do list.
    /// </summary>
    internal class ToDoTask
    {
        private string _name;

        /// <summary>
        /// Gets or sets the name of the task.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when trying to set the value to null.</exception>
        public string Name
        {
            get => _name;
            set => _name = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Gets or sets the completion date of the task.
        /// </summary>
        public DateTime CompletionDate { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ToDoTask"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="completionDate">The completion date.</param>
        /// <exception cref="System.ArgumentNullException">name</exception>
        public ToDoTask(string name, DateTime completionDate = default)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
            if (completionDate == default)
            {
                CompletionDate = DateTime.MaxValue;
            }
            CompletionDate = completionDate;
        }
    }

    /// <summary>
    /// Compares two <see cref="ToDoTask"/> objects based on their completion dates.
    /// </summary>
    internal class TaskCompletionDateComparer : IComparer<ToDoTask>
    {
        /// <summary>
        /// Compares two <see cref="ToDoTask"/> objects and returns a value indicating whether one is less than, equal to, or greater than the other.
        /// </summary>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// <returns>A signed integer that indicates the relative values of x and y.</returns>
        public int Compare(ToDoTask? x, ToDoTask? y)
        {
            if (x == null) return -1;
            if (y == null) return 1;

            int dateComparison = x.CompletionDate.CompareTo(y.CompletionDate);
            if (dateComparison == 0)
            {
                // If dates are equal, compare by task name
                return String.Compare(x.Name, y.Name, StringComparison.Ordinal);
            }
            else
            {
                return dateComparison;
            }
        }

    }

}

