using System.Runtime.CompilerServices;
using Terminal.Gui;
using ToDoApplicationConsole.ToDoModel;

namespace ToDoApplicationConsole.ToDoView
{
    internal class ApplicationUi
    {
        private ScrollView _mainWindow;
        private readonly List<FrameView> _frames = new List<FrameView>();
        private ToDoListCollections _listCollections;
        public ApplicationUi(ToDoListCollections toDoListCollections)
        {
            _listCollections = toDoListCollections;
            _mainWindow = InitializeMainWindow();
            SetKeyBinds();
        }

        public ScrollView GetUi()
        {
            return _mainWindow;
        }

        private static ScrollView InitializeMainWindow()
        {
            ScrollView scroll = new ScrollView()
            {
                X = 0,
                Y = 0,
                Width = Dim.Fill(),
                Height = Dim.Fill(),
                ContentSize = new Size(Application.Top.Frame.Width, Application.Top.Frame.Height),
                AutoHideScrollBars = true,
            };
            Application.Resized += (Application.ResizedEventArgs args) =>
            {
                scroll.ContentSize = new Size(Application.Top.Frame.Width + 500, Application.Top.Frame.Height);
            };
            scroll.ContentSize = new Size(Application.Top.Frame.Width + 500, Application.Top.Frame.Height);
            return scroll;

        }

        private void SetKeyBinds()
        {
            _mainWindow.KeyPress += (e) =>
            {

                switch (e.KeyEvent.Key)
                {
                    case Key.G:
                        AddToDoGroup();
                        break;
                    case Key.S:
                        Application.RequestStop();
                        break;

                }

            };
        }

        private void AddToDoGroup()
        {
            Button okButton = new Button("Ok");
            Button cancelButton = new Button("Cancel");
            Dialog addToDoGroupDialog = new Dialog("Quit", okButton, cancelButton);
        }


    }
}
