using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace ToDoApplicationConsole.ToDoView
{
    internal class ApplicationTaskGroupUi
    {
        private readonly FrameView _groupFrameView;

        public ApplicationTaskGroupUi(string groupName)
        {
            _groupFrameView = new FrameView(groupName)
            {
                Y = 0,
                Height = Dim.Fill(),
                Width = Dim.Percent(5)
            };
        }

        public FrameView AddTaskGroup(ScrollView contentWindow)
        {
            _groupFrameView.X = 0;
            contentWindow.Add(_groupFrameView);
            return _groupFrameView;
        }

        public FrameView AddTaskGroup(ScrollView contentWindow, FrameView lastFrameView)
        {
            _groupFrameView.X = Pos.Right(lastFrameView);
            contentWindow.Add(_groupFrameView);
            return _groupFrameView;
        }

    }
}
