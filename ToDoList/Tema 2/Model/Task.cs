using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Tema_2.Model.Enums;

namespace Tema_2.Model
{
	public class Task : IBaseEntity, INotifyPropertyChanged
	{
		private string _priority;
		private string _description;
		private bool _status;
		private DateTime _dueDate;
		private string _name;

		public string Priority
		{
			get => _priority;
			set
			{
				if (value == _priority) return;
				_priority = value;
				OnPropertyChanged();
			}
		}

		public string Description
		{
			get => _description;
			set
			{
				if (value == _description) return;
				_description = value;
				OnPropertyChanged();
			}
		}

		public bool Status
		{
			get => _status;
			set
			{
				if (value == _status) return;
				_status = value;
				OnPropertyChanged();
			}
		}

		public DateTime DueDate
		{
			get => _dueDate;
			set
			{
				if (value.Equals(_dueDate)) return;
				_dueDate = value;
				OnPropertyChanged();
			}
		}

		public string Name
		{
			get => _name;
			set
			{
				if (value == _name) return;
				_name = value;
				OnPropertyChanged();
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
	        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
	        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
	        field = value;
	        OnPropertyChanged(propertyName);
	        return true;
        }
	}
}
