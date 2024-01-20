using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using NStack;
using Terminal.Gui;
using ToDoApplicationConsole.ToDoController;

namespace ToDoApplicationConsole.ToDoView
{
    internal class ApplicationTaskGroupUi
    {
        private readonly FrameView _groupFrame;
        private readonly ListView _taskListView;
        private readonly List<string> _taskList;
        private readonly ApplicationAddTaskDialogUi _applicationAddTaskDialog;
        public ApplicationTaskGroupUi(string groupName,View mainWindow, ScrollView contentWindow, ApplicationController controller)
        {
            _taskList = new List<string>();
            _applicationAddTaskDialog = new ApplicationAddTaskDialogUi(groupName, mainWindow, contentWindow);
            _groupFrame = new FrameView(groupName)
            {
                Y = 0,
                Height = Dim.Fill(),
                Width = Dim.Sized(ApplicationUiConstants.ToDoListSize),
                CanFocus = true
            };
            _taskListView = new ListView(_taskList)
            {
                AllowsMarking = true,
                Height = Dim.Fill(),
                Width = Dim.Fill()
            };
            _groupFrame.Add(_taskListView);
            SetupKeyBinds(controller);
        }

        private void SetupKeyBinds( ApplicationController controller)
        {
            _groupFrame.KeyPress += (e) =>
            {
                if (_groupFrame.HasFocus)
                {
                    switch (e.KeyEvent.Key)
                    {
                        case Key.T:
                            _applicationAddTaskDialog.ShowDialog(controller, _taskList);
                            break;
                    }
                }
            };
        }

        public FrameView AddTaskGroup(ScrollView contentWindow)
        {
            _groupFrame.X = 0;
            contentWindow.Add(_groupFrame);
            return _groupFrame;
        }

        public FrameView AddTaskGroup(ScrollView contentWindow, FrameView lastFrameView)
        {
            _groupFrame.X = Pos.Right(lastFrameView);
            contentWindow.Add(_groupFrame);
            return _groupFrame;
        }

    }
}
