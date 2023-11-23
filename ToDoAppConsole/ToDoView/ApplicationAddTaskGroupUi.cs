using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;
using ToDoApplicationConsole.ToDoModel;

namespace ToDoApplicationConsole.ToDoView
{
    internal class ApplicationAddTaskGroupUi
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

        public ApplicationAddTaskGroupUi()
        {
            _addToDoGroupDialog.Add(_addToDoGroupTextField);
            _addToDoGroupDialog.AddButton(_addToDoGroupOkButton);
            _addToDoGroupDialog.AddButton(_addToDoGroupCancelButton);
            _errorDialog.AddButton(_errorDialogOkButton);
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


        public void AddToDoGroupDialog(View mainWindow, ScrollView contentWindow, ToDoListCollections toDoListCollections)
        {
            _addToDoGroupTextField.Text = String.Empty;
            contentWindow.Enabled = false;
            mainWindow.Add(_addToDoGroupDialog);
            _addToDoGroupOkButton.Clicked += () =>
            {
                string? groupName = _addToDoGroupTextField.Text.ToString();
                mainWindow.Remove(_addToDoGroupDialog);
                if (!String.IsNullOrEmpty(groupName))
                {
                    (bool success, string message) result = toDoListCollections.AddListGroup(groupName);
                    _errorDialog.Text = result.message;
                    if (result.success)
                    {
                        contentWindow.Enabled = true;
                        contentWindow.FocusFirst();
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

            };
            _addToDoGroupCancelButton.Clicked += () =>
            {
                contentWindow.Enabled = true;
                contentWindow.FocusFirst();
                mainWindow.Remove(_addToDoGroupDialog);
            };
            _errorDialogOkButton.Clicked += () =>
            {
                mainWindow.Remove(_errorDialog);
                mainWindow.Add(_addToDoGroupDialog);
            };
        }
    }
}
