using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Tema_2.Utils
{
	class RelayCommand : ICommand
	{
		private Action commandTask;

		public RelayCommand(Action workToDo)
		{
			commandTask = workToDo;
		}

		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			commandTask();
		}
	}
}

