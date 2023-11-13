using Terminal.Gui;
using ToDoApplicationConsole.ToDoModel;
namespace ToDoApplicationConsole
{
    internal class Program
    {
        private static int Main()
        {
            Application.Init();
            Colors.Base.Normal = Application.Driver.MakeAttribute(Color.Green, Color.Black);
            var colorScheme = new ColorScheme()
            {
                Normal = Application.Driver.MakeAttribute(Color.White, Color.Blue) // White on Blue
            };

            var top = Application.Top;

            var frameCount = 15;
            // Creates the top-level window to show
            var win = new Window("Hello")
            {
                X = 0,
                Y = Pos.Center(), // Leave one row for the toplevel menu
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };
            var scroll = new ScrollView()
            {
                X = 0,
                Y = 0,
                Width = Dim.Fill(),
                Height = Dim.Fill(),
                ContentSize = new Size((int)(Application.Top.Frame.Width), Application.Top.Frame.Height),
            };
            Application.Resized += (Application.ResizedEventArgs args) =>
            {
                scroll.ContentSize = new Size((int)(Application.Top.Frame.Width+1000), Application.Top.Frame.Height);
            };
            scroll.ContentSize = new Size((int)(Application.Top.Frame.Width+1000), Application.Top.Frame.Height);

            win.Add(scroll);
            top.Add(win);
            var frames = new List<FrameView>();
            for (int i = 0; i < frameCount; i++)
            {
                var frame = new FrameView()
                {
                    X = (i == 0) ? 0 : Pos.Right(frames[i - 1]),
                    Y = 0,
                    Width = Dim.Percent((1f / frameCount) * 100),
                    Height = Dim.Fill()
                };
                var text = new TextView()
                {
                    X = 0,
                    Y = 0,
                    Width = Dim.Fill(),
                    Height = Dim.Fill(),
                    Text = i.ToString(),
                    ColorScheme = colorScheme

                };
                frame.Add(text);
                frames.Add(frame);
            }

            foreach (var frame in frames)
            {
                scroll.Add(frame);
            }

            Application.Run();



            //ToDoListCollections lists = ToDoListCollections.ReadListsFromFile();
            //lists.PrintGroups();
            // First, add the groups
            //lists.AddListGroup("Bruh");
            //lists.AddListGroup("Epic");

            //// Then, add tasks without a specific group or completion date
            //lists.AddTask("Walk dog");
            //lists.AddTask("Buy groceries");
            //lists.AddTask("Read a book");
            //lists.AddTask("Clean the house");

            //// Next, add tasks with a completion date but without a specific group
            //lists.AddTask("Make cake", completionDateTime:DateTime.Now);
            //lists.AddTask("Finish report", completionDateTime:DateTime.Now.AddDays(2));
            //lists.AddTask("Attend yoga class", completionDateTime: DateTime.Now.AddDays(3));
            //lists.AddTask("Prepare for the meeting", completionDateTime: DateTime.Now.AddDays(1));

            //// After that, add tasks to a specific group without a completion date
            //lists.AddTask("Call mom", "Epic");
            //lists.AddTask("Water the plants", "Bruh");
            //lists.AddTask("Renew library membership", "Epic");

            //// Finally, add tasks to a specific group with a completion date
            //lists.AddTask("Take out trash", "Epic", new DateTime(2028, 12, 3));
            //lists.AddTask("Pay bills", "Bruh", new DateTime(2023, 11, 30));
            //lists.AddTask("Schedule dentist appointment", "Epic", new DateTime(2023, 12, 15));
            //lists.AddTask("Plan a vacation", "Bruh", new DateTime(2024, 1, 1));

            //lists.CompleteTask("Walk dog");
            //lists.CompleteTask("Walk dog", "Epic");
            //lists.CompleteTask("Take out trash", "Epic");

            //lists.SaveListsToFile();
            // Render two items on separate columns to Console
            // Create a list of Items
            // Create a list of Items, apply separate styles to each
            // Create the layout
            // Create a table
            return 0;
        }
    }
}