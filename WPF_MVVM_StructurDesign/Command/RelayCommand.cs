using System;
using System.Linq;
using System.Collections.Generic;

using System.Windows.Input;

namespace WPF_MVVM_StructurDesign.Command
{
    public class RelayCommand :ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action doWork;
        public RelayCommand(Action doWork)
        {
        	this.doWork=doWork;
        }
        public bool CanExecute(object param)
        {
        	return true;
        }
        public void Execute(object param)
        {
        	doWork();
        }
    }  
}