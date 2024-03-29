﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;
using ToDoApplicationConsole.ToDoModel;
using ToDoApplicationConsole.ToDoView;

namespace ToDoApplicationConsole.ToDoController
{
    internal class ApplicationController
    {
        private readonly ToDoListCollections _toDoList;
        private ApplicationUi _applicationUi;
        public ApplicationController()
        {
            _toDoList = new ToDoListCollections(this);
            _applicationUi = new ApplicationUi(this);
        }

        public (bool success, string messsage) AddToDoGroup(string name)
        {
            return _toDoList.AddListGroup(name);
        }

        public (bool success, string messsage) AddToDoTask(string groupName, string taskName, DateTime dateTime)
        {
            return _toDoList.AddTask(taskName, groupName, dateTime);
        }

        public (bool success, string message) RemoveToDoTask(string groupName, string taskName)
        {
            return _toDoList.RemoveTask(taskName, groupName);
        }

        public int GetListSize()
        {
            return _toDoList.GetListSize();
        }

    }
}
