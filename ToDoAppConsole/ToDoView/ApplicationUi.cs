﻿using System.Diagnostics;
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
        private readonly ApplicationController _applicationController;
        public ApplicationUi(ApplicationController controller)
        {
            _applicationController = controller;
            (_mainWindow, _contentWindow) = InitializeMainWindow();
            _applicationAddTaskGroupDialogUi = new ApplicationAddTaskGroupDialogUi(_mainWindow, _contentWindow, controller);
            Application.Top.Add(_mainWindow);
            SetKeyBinds();
        }


        private static (View mainWindow, ScrollView contentWindow) InitializeMainWindow()
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
                ContentSize = new Size(0, Application.Top.Frame.Height),
                AutoHideScrollBars = true,
            };
            Application.Resized += (Application.ResizedEventArgs args) =>
            {
                Size curSize = scroll.ContentSize;
                scroll.ContentSize = new Size(curSize.Width, Application.Top.Frame.Height);
            };
            scroll.ContentSize = new Size(ApplicationUiConstants.ToDoListSize, Application.Top.Frame.Height);
            view.Add(scroll);
            return (view, scroll);
        }

        private void SetKeyBinds()
        {
            _contentWindow.KeyDown+= (e) =>
            {
                if (_contentWindow.HasFocus)
                {
                    switch (e.KeyEvent.Key)
                    {
                        case Key.G:
                            AddToDoGroup(_applicationController);
                            break;
                        case Key.S:
                            Application.RequestStop();
                            break;
                        default:
                            break;

                    }
                }

            };
        }

        private void AddToDoGroup(ApplicationController controller)
        {
            _applicationAddTaskGroupDialogUi.AddToDoGroupDialog(_mainWindow, _contentWindow, controller);
        }



    }
}
