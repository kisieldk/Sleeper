using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Snipp.Model
{
	public class BindableObjectBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public void RaisePropertyChanged([CallerMemberName] string propertyName = null)
		{
			if (PropertyChanged != null)
			{
				try
				{
					PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

				}
				catch (Exception)
				{	
				}
			}
		}
	}
}

