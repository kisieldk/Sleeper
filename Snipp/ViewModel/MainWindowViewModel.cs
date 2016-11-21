using Snipp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Snipp.ViewModel
{
	public class MainWindowViewModel : BindableObjectBase
	{
		private int _timeToCloseSystem;
		private DispatcherTimer _dispatcherTimer = new DispatcherTimer();

		public MainWindowViewModel()
		{
			_dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
			_dispatcherTimer.Interval = new TimeSpan(0, 5, 0);
		}

		public int TimeToCloseSystem
		{
			get { return _timeToCloseSystem; }
			set
			{
				_timeToCloseSystem = value;
				RaisePropertyChanged();
			}
		}
		public DispatcherTimer TimeToLeftCounter
		{
			get { return _dispatcherTimer; }

		}

		private void dispatcherTimer_Tick(object sender, EventArgs e)
		{
			if (TimeToCloseSystem >= 5)
			{
				TimeToCloseSystem -= 5;
			}
			else _dispatcherTimer.Stop();
		}
	}
}
