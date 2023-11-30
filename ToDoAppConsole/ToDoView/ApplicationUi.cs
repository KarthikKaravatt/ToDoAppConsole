using System.Diagnostics;
using System.Runtime.CompilerServices;
using Terminal.Gui;
using ToDoApplicationConsole.ToDoController;
using ToDoApplicationConsole.ToDoModel;

namespace ToDoApplicationConsole.ToDoView
{
    /// <summary>
    /// The is the front end ui for the application
    /// </summary>
    internal class ApplicationUi
    {
        private readonly View _mainWindow;
        private readonly ScrollView _contentWindow;
        private readonly ApplicationAddTaskGroupDialogUi _applicationAddTaskGroupDialogUi;
        private ApplicationController _applicationController;
        public ApplicationUi(ApplicationController controller)
        {
            _applicationController = controller;
            (_mainWindow, _contentWindow) = InitializeMainWindow();
            _applicationAddTaskGroupDialogUi = new ApplicationAddTaskGroupDialogUi(_contentWindow, controller);
            Application.Top.Add(_mainWindow);
            SetKeyBinds();
        }


        private static (View mainWindow, ScrollView conetnWindow) InitializeMainWindow()
        {
            View view = new View()
            {
                X = 0,
                Y = 0,
                Width = Dim.Fill(),
                Height = Dim.Fill(),
            };
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
                scroll.ContentSize = new Size(Application.Top.Frame.Width + ApplicationUiConstants.ToDoListSize, Application.Top.Frame.Height);
            };
            scroll.ContentSize = new Size(Application.Top.Frame.Width + ApplicationUiConstants.ToDoListSize, Application.Top.Frame.Height);
            view.Add(scroll);
            return (view, scroll);

        }

        private void SetKeyBinds()
        {
            _contentWindow.KeyPress += (e) =>
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
            _applicationAddTaskGroupDialogUi.AddToDoGroupDialog(_mainWindow, _contentWindow);
        }



    }
}
