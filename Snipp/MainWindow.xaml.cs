using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Diagnostics;
using System.Reflection;
using Snipp.ViewModel;

namespace Snipp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
		private CancellationTokenSource _cancellationToken = new CancellationTokenSource();
		private MainWindowViewModel _mainWindowViewModel;
		public MainWindow()
        {
            InitializeComponent();
			_mainWindowViewModel = new MainWindowViewModel();
			DataContext = _mainWindowViewModel;
        }

		private void btnExit_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private async void btnSave_Click(object sender, RoutedEventArgs e)
		{
			_cancellationToken.Cancel();
			_cancellationToken = new CancellationTokenSource();
			await ShuthDown(_cancellationToken.Token);
		}
		private void btnStopProcess_Click(object sender, RoutedEventArgs e)
		{
			_cancellationToken.Cancel();
			_mainWindowViewModel.TimeToLeftCounter.Stop();
		}
		private int CalculateDelay()
		{
			_mainWindowViewModel.TimeToCloseSystem = (int)slider.Value;
			return (int)slider.Value * 60 * 1000;
		}
		private async Task ShuthDown(CancellationToken token)
		{
			try
			{
				_mainWindowViewModel.TimeToLeftCounter.Start();
				token.ThrowIfCancellationRequested();
				int delay = CalculateDelay();
				await Task.Delay(delay,token);
				{
					_mainWindowViewModel.TimeToLeftCounter.Stop();
					Process.Start("shutdown", "/f /s /t 0");

					//For test
					//MessageBox.Show("Test", "Test",MessageBoxButton.OK);

				}
			}
			catch (Exception)
			{
				_mainWindowViewModel.TimeToLeftCounter.Stop();
			}

		}

		//TODO: In progress
		private void InstallMeOnStartUp()
		{
			try
			{
				Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
				Assembly curAssembly = Assembly.GetExecutingAssembly();
				key.SetValue(curAssembly.GetName().Name, curAssembly.Location);
			}
			catch (Exception)
			{
			}
		}
	}
}
