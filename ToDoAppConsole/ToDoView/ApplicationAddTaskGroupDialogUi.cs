using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;
using ToDoApplicationConsole.ToDoController;
using ToDoApplicationConsole.ToDoModel;

namespace ToDoApplicationConsole.ToDoView
{
    internal class ApplicationAddTaskGroupDialogUi
    {
        private readonly Dialog _addToDoGroupDialog = SetupAddToDoGroupDialogue();
        private readonly Dialog _errorDialog = SetupErrorDialog();
        private readonly Button _addToDoGroupOkButton = new Button(ApplicationUiConstants.OkButtonName);
        private readonly Button _addToDoGroupCancelButton = new Button(ApplicationUiConstants.CancelButtonName);
        private readonly Button _errorDialogOkButton = new Button(ApplicationUiConstants.OkButtonName);
        private readonly TextField _addToDoGroupTextField = new TextField()
        {
            X = Pos.Center(),
            Y = Pos.Center(),
            Width = Dim.Percent(ApplicationUiConstants.AddToDoGroupTextFieldWidthPercent),
        };

        private FrameView _lastFrameView;

        private readonly ApplicationController _controller;

        public ApplicationAddTaskGroupDialogUi(View mainWindow,ScrollView contentWindow, ApplicationController controller)
        {
            _controller = controller;
            _addToDoGroupDialog.Add(_addToDoGroupTextField);
            _addToDoGroupDialog.AddButton(_addToDoGroupOkButton);
            _addToDoGroupDialog.AddButton(_addToDoGroupCancelButton);
            _errorDialog.AddButton(_errorDialogOkButton);
            ApplicationTaskGroupUi defaultGroupUi = new ApplicationTaskGroupUi(ToDoModelConstants.GlobalGroupName,mainWindow,contentWindow, _controller);
            _lastFrameView = defaultGroupUi.AddTaskGroup(contentWindow);

        }


        private static Dialog SetupAddToDoGroupDialogue()
        {
            int width = (int)Math.Round(Application.Top.Frame.Width * ApplicationUiConstants.SetUpDialogSize);
            int height = (int)Math.Round(Application.Top.Frame.Height * ApplicationUiConstants.SetUpDialogSize);
            Dialog addToDoGroupDialog = new Dialog(ApplicationUiConstants.EnterGroupLabel, width, height)
            {
                X = Pos.Center(),
                Y = Pos.Center(),
                Width = width,
                Height = height
            };
            Application.Resized += (Application.ResizedEventArgs args) =>
            {
                width = (int)Math.Round(Application.Top.Frame.Width * ApplicationUiConstants.SetUpDialogSize);
                height = (int)Math.Round(Application.Top.Frame.Height * ApplicationUiConstants.SetUpDialogSize);
                addToDoGroupDialog.Width = width;
                addToDoGroupDialog.Height = height;
            };
            return addToDoGroupDialog;

        }

        private static Dialog SetupErrorDialog()
        {
            int width = (int)Math.Round(Application.Top.Frame.Width * ApplicationUiConstants.ErrorDialogWidth);
            int height = (int)Math.Round(Application.Top.Frame.Height * ApplicationUiConstants.ErrorDialogHeight);
            Dialog errorDialog = new Dialog()
            {
                X = Pos.Center(),
                Y = Pos.Center(),
                Width = width,
                Height = height,
            };
            Application.Resized += (Application.ResizedEventArgs args) =>
            {
                width = (int)Math.Round(Application.Top.Frame.Width * ApplicationUiConstants.ErrorDialogWidth);
                height = (int)Math.Round(Application.Top.Frame.Height * ApplicationUiConstants.ErrorDialogHeight);
                errorDialog.Width = width;
                errorDialog.Height = height;
            };
            return errorDialog;
        }

        private void ResizeContentWindow(ScrollView contentWindow)
        {
            int listSize= _controller.GetListSize();
            Size contentSize = contentWindow.ContentSize;

            contentWindow.ContentSize = new Size(ApplicationUiConstants.ToDoListSize *listSize, contentSize.Height);
        }

        private void HandleToDoGroupOkayButtonClick(ref bool added, ScrollView contentWindow, View mainWindow, ApplicationController controller)
        {
                if (!added)
                {

                    string? groupName = _addToDoGroupTextField.Text.ToString();
                    if (!String.IsNullOrEmpty(groupName))
                    {
                        (bool success, string message) result = _controller.AddToDoGroup(groupName);
                        _errorDialog.Text = result.message;
                        if (result.success)
                        {
                            ApplicationTaskGroupUi taskGroup = new ApplicationTaskGroupUi(groupName,mainWindow,contentWindow, controller);
                            _lastFrameView = taskGroup.AddTaskGroup(contentWindow, _lastFrameView);
                            contentWindow.Enabled = true;
                            contentWindow.FocusFirst();
                            ResizeContentWindow(contentWindow);
                            added = true;
                        }
                        else
                        {
                            mainWindow.Add(_errorDialog);
                        }
                    }
                    else
                    {
                        _errorDialog.Text = ApplicationUiConstants.NameNotEnteredError;
                        mainWindow.Add(_errorDialog);
                    }
                }

                mainWindow.Remove(_addToDoGroupDialog);
        }

        private void HandleToDoGroupCancelButtonClicked(out bool added, ScrollView contentWindow, View mainWindow)
        {
                added = true;
                contentWindow.Enabled = true;
                contentWindow.FocusFirst();
                mainWindow.Remove(_addToDoGroupDialog);
        }

        private void HandleErrorDialogOkayButtonClick(out bool added, ScrollView contentWindow, View mainWindow)
        {
                added = true;
                contentWindow.Enabled = true;
                contentWindow.FocusFirst();
                mainWindow.Remove(_errorDialog);
        }

        public void AddToDoGroupDialog(View mainWindow, ScrollView contentWindow, ApplicationController controller)
        {
            _addToDoGroupTextField.Text = String.Empty;
            bool added = false;
            contentWindow.Enabled = false;
            mainWindow.Add(_addToDoGroupDialog);
            _addToDoGroupOkButton.Clicked += () => HandleToDoGroupOkayButtonClick(ref added, contentWindow, mainWindow, controller);
            _addToDoGroupCancelButton.Clicked +=
                () => HandleToDoGroupCancelButtonClicked(out added, contentWindow, mainWindow);
            _errorDialogOkButton.Clicked +=
                () => HandleErrorDialogOkayButtonClick(out added, contentWindow, mainWindow);
        }
    }
}
