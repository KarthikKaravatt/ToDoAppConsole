using Terminal.Gui;
using ToDoApplicationConsole.ToDoModel;
using ToDoApplicationConsole.ToDoController;
namespace ToDoApplicationConsole
{
    internal static class Program
    {
        private static int Main()
        {
            Application.Init();
            ApplicationController controller = new ApplicationController();
            Application.Run();
            return 0;
        }
    }
}