using System.Diagnostics;
using System.Runtime.CompilerServices;
using Terminal.Gui;
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
        private readonly ToDoListCollections _listCollections;
        public ApplicationUi(ToDoListCollections toDoListCollections)
        {
            _listCollections = toDoListCollections;
            (_mainWindow, _contentWindow) = InitializeMainWindow();
            SetKeyBinds();
        }

        public View GetUi()
        {
            return _mainWindow;
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
                scroll.ContentSize = new Size(Application.Top.Frame.Width + 500, Application.Top.Frame.Height);
            };
            scroll.ContentSize = new Size(Application.Top.Frame.Width + 500, Application.Top.Frame.Height);
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

        private static Dialog SetupAddToDoGroupDialogue()
        {
            int width = (int)Math.Round(Application.Top.Frame.Width * 0.40);
            int height = (int)Math.Round(Application.Top.Frame.Height * 0.40);
            Button okButton = new Button(ApplicationUiConstants.OkButtonName);
            Button cancelButton = new Button(ApplicationUiConstants.CancelButtonName);
            Dialog addToDoGroupDialog = new Dialog(ApplicationUiConstants.EnterGroupLabel, width, height)
            {
                X = Pos.Center(),
                Y = Pos.Center()
            };
            TextField textField = new TextField()
            {
                Y = Pos.Center(),
                X = Pos.Center(),
                Width = Dim.Percent(70),

            };
            addToDoGroupDialog.Add(textField);
            addToDoGroupDialog.AddButton(okButton);
            addToDoGroupDialog.AddButton(cancelButton);
            Application.Resized += (Application.ResizedEventArgs args) =>
            {
                width = (int)Math.Round(Application.Top.Frame.Width * 0.40);
                height = (int)Math.Round(Application.Top.Frame.Height * 0.40);
                addToDoGroupDialog.Width = width;
                addToDoGroupDialog.Height = height;
            };
            return addToDoGroupDialog;

        }
        private void AddToDoGroup()
        {
            Dialog addToDoGroupDialog = SetupAddToDoGroupDialogue();
            List<Button> buttons = new List<Button>();
            List<TextField> textFields = new List<TextField>();

            foreach (var subview in addToDoGroupDialog.Subviews)
            {
                if (subview is View container)
                {
                    buttons.AddRange(container.Subviews.OfType<Button>());
                    textFields.AddRange(container.Subviews.OfType<TextField>());
                }
            }

            // the buttons with their Text property set to "Ok" or "Cancel"
            var okButton = buttons.Find(button => button.Text == ApplicationUiConstants.OkButtonName);
            var cancelButton = buttons.Find(button => button.Text == ApplicationUiConstants.CancelButtonName);
            var textField = textFields[0];
            if (okButton == null || cancelButton == null || textField == null)
            {
                throw new InvalidOperationException(ApplicationUiConstants.DialogButtonNotFoundError);
            }
            okButton.Clicked += () =>
            {

                string? groupName = textField.Text.ToString();
                if (groupName != null)
                {
                    (bool success, string message) result = _listCollections.AddListGroup(groupName);
                    Console.WriteLine(result);
                }
                _contentWindow.Enabled = true;
                _contentWindow.FocusFirst();
                _mainWindow.Remove(addToDoGroupDialog);



            };
            cancelButton.Clicked += () =>
            {
                // re-enable window and focus it
                _contentWindow.Enabled = true;
                _contentWindow.FocusFirst();
                _mainWindow.Remove(addToDoGroupDialog);

            };
            _contentWindow.Enabled = false;
            _mainWindow.Add(addToDoGroupDialog);

        }



    }
}
