using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tema_2.Utils;
using Tema_2.View;

namespace Tema_2.ViewModel
{
	public class AboutViewMode : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
        public string Text { get; set; } = "Name: Popa Razvan Ionut\r\n\t\t\t\t   Group: G213";

		public void GetText()
		{
			Text = "Name: Popa Razvan Ionut\r\n\t\t\t\t   Group: G213";
		}

		private ICommand aboutCommand;
		public ICommand AboutCommand
		{
			get
			{
				if (aboutCommand == null)
				{
					aboutCommand = new RelayCommand(GetText);
				}
				return aboutCommand;
			}
		}

		public void About(object param)
		{


			AboutWindow aboutWindow = new AboutWindow();
			aboutWindow.ShowDialog();

		}

	}
}
