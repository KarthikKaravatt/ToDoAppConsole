﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using NStack;
using Terminal.Gui;
using ToDoApplicationConsole.ToDoController;

namespace ToDoApplicationConsole.ToDoView
{
    internal class ApplicationAddTaskDialogUi
    {
        private readonly string _groupName;
        private readonly Dialog _addTaskDialog = new()
        {
            Height = Dim.Percent(ApplicationUiConstants.AddTaskDialogSize),
            Width = Dim.Percent(ApplicationUiConstants.AddTaskDialogSize)
        };
        private readonly Button _cancelButton = new(ApplicationUiConstants.CancelButtonName);
        private readonly Button _okButton = new(ApplicationUiConstants.OkButtonName);
        private string _taskName = String.Empty;
        private int _day;
        private int _month;
        private int _year;

        public ApplicationAddTaskDialogUi(string groupName)
        {
            _groupName = groupName;
            FrameView name = SetUpNameFrame();
            FrameView date = SetUpDateFrame(name);
            _addTaskDialog.AddButton(_okButton);
            _addTaskDialog.AddButton(_cancelButton);
            _addTaskDialog.Add(name);
            _addTaskDialog.Add(date);
        }

        private FrameView SetUpNameFrame()
        {
            FrameView taskNameFrame = CreateFrameView(40, Dim.Fill());
            TextField nameField = CreateTextField(50, Pos.Center(), Pos.Center());
            Label nameFieldLabel = CreateLabel(20, 30, Pos.Bottom(nameField), TextAlignment.Centered, "Task Name",
                Pos.Center());
            taskNameFrame.Add(nameField);
            taskNameFrame.Add(nameFieldLabel);

            nameField.Leave += (arg) =>
            {
                _taskName = nameField.Text.ToString() ?? string.Empty;
            };
            return taskNameFrame;
        }


        private FrameView SetUpDateFrame(FrameView nameFrame)
        {
            FrameView date = CreateFrameView(40, Dim.Fill());
            date.Y = Pos.Bottom(nameFrame);
            DateTime curDateTime = DateTime.Now;
            _day = curDateTime.Day;
            _month = curDateTime.Month;
            _year = curDateTime.Year;

            View[] inputViews = new View[3];
            TextField[] textFields = new TextField[3];
            string[] labels = { "Day", "Month", "Year" };

            for (int i = 0; i < 3; i++)
            {
                inputViews[i] = CreateView(90, 33, i > 0 ? Pos.Right(inputViews[i - 1]) : null, Pos.Center());
                textFields[i] = CreateTextField(50, Pos.Center(), Pos.Center());
                inputViews[i].Add(textFields[i]);
                date.Add(inputViews[i]);
                date.Add(CreateLabel(20, 30, Pos.Bottom(inputViews[i]), TextAlignment.Centered, labels[i],
                    i > 0 ? Pos.Right(inputViews[i - 1]) : null));
            }

            textFields[0].Leave += (args) => { _day = ParseDate(textFields[0]); };
            textFields[1].Leave += (args) => { _month = ParseDate(textFields[1]); };
            textFields[2].Leave += (args) => { _year = ParseDate(textFields[2]); };

            textFields[0].Text = _day.ToString();
            textFields[1].Text = _month.ToString();
            textFields[2].Text = _year.ToString();

            return date;
        }

        private int ParseDate(TextField textField)
        {
            string dateString = textField.Text.ToString() ?? string.Empty;
            return dateString != string.Empty && int.TryParse(dateString, out int date) ? date : 0;
        }


        private FrameView CreateFrameView(int heightPercent, Dim width)
        {
            return new FrameView
            {
                Height = Dim.Percent(heightPercent),
                Width = width,
            };
        }

        private View CreateView(int heightPercent, int widthPercent, Pos? x = null, Pos? y = null)
        {
            return new View
            {
                Height = Dim.Percent(heightPercent),
                Width = Dim.Percent(widthPercent),
                X = x,
                Y = y
            };
        }

        private TextField CreateTextField(int widthPercent, Pos x, Pos y)
        {
            return new TextField
            {
                Width = Dim.Percent(widthPercent),
                X = x,
                Y = y
            };
        }

        private Label CreateLabel(int heightPercent, int widthPercent, Pos y, TextAlignment textAlignment, string text,
            Pos? x = null)
        {
            return new Label
            {
                Height = Dim.Percent(heightPercent),
                Width = Dim.Percent(widthPercent),
                Y = y,
                X = x,
                TextAlignment = textAlignment,
                Text = text
            };
        }


        public void ShowDialog(View mainView, ScrollView contentView, ApplicationController controller,
            List<string> taskList)
        {
            SetupButtons(mainView, contentView, controller, taskList);
            contentView.Enabled = false;
            mainView.Add(_addTaskDialog);
            _addTaskDialog.EnsureFocus();
        }

        private void SetupButtons(View mainVew, ScrollView contentView, ApplicationController controller, List<string> taskList)
        {
            _cancelButton.Clicked += () => OnCancelButtonOnClicked(mainVew, contentView);

            _okButton.Clicked += () => OnOkButtonClicked(controller, taskList);
        }
        private void OnCancelButtonOnClicked(View mainVew, ScrollView contentView)
        {
            mainVew.Remove(_addTaskDialog);
            contentView.Enabled = true;
            contentView.EnsureFocus();
        }
        private void OnOkButtonClicked(ApplicationController controller, List<string> taskList)
        {
            try
            {
                (bool success, string message) result;
            //TODO: Add the ability to set a time
#pragma warning disable S6562 // Always set the "DateTimeKind" when creating new "DateTime" instances
                DateTime date = new(_year, _month, _day);
#pragma warning restore S6562 // Always set the "DateTimeKind" when creating new "DateTime" instances
                result = controller.AddToDoTask(_groupName, _taskName, date);
                if (result.success)
                {
                    string taskString = _taskName + date;
                    taskList.Add(taskString);
                    Application.Refresh();
                }
                else
                {
                    //TODO: show error dialog
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                //TODO: show error dialog
                Console.WriteLine(e);
            }

        }

    }
}
