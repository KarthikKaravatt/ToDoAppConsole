using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace ToDoApplicationConsole.ToDoView
{
    internal class ApplicationTaskGroupUi
    {
        private readonly FrameView _groupFrame;
        private readonly ListView _groupList;

        public ApplicationTaskGroupUi(string groupName)
        {
            List<string> testString = new List<string>();
            testString.Add("Bruh");
            testString.Add("test");
            testString.Add("lol");
            _groupFrame = new FrameView(groupName)
            {
                Y = 0,
                Height = Dim.Fill(),
                Width = Dim.Sized(ApplicationUiConstants.ToDoListSize),
            };
            _groupList = new ListView(testString)
            {
                Y = 0,
                Height = Dim.Fill(),
                Width = Dim.Sized(ApplicationUiConstants.ToDoListSize),
                AllowsMarking = true
            };
            _groupFrame.Add(_groupList);
            SetupKeyBinds(_groupList);
        }

        public static void SetupKeyBinds(ListView frame)
        {
            frame.KeyPress += (e) =>
            {
                switch (e.KeyEvent.Key)
                {
                    case Key.T:
                        Console.WriteLine("Test");
                        break;

                    default:
                        break;
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
