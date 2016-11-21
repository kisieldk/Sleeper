﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Snipp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
            private System.Windows.Forms.NotifyIcon _notifyIcon;
            private bool _isExit;

            protected override void OnStartup(StartupEventArgs e)
            {
                base.OnStartup(e);
                MainWindow = new MainWindow();
                MainWindow.Closing += MainWindow_Closing;

                _notifyIcon = new System.Windows.Forms.NotifyIcon();
                _notifyIcon.DoubleClick += (s, args) => ShowMainWindow();
                _notifyIcon.Icon = Resource.Icon1;
                _notifyIcon.Visible = true;

                CreateContextMenu();
            }

            private void CreateContextMenu()
            {
                _notifyIcon.ContextMenuStrip =
                  new System.Windows.Forms.ContextMenuStrip();
                _notifyIcon.ContextMenuStrip.Items.Add("Ustaw czas").Click += (s, e) => ShowMainWindow();
                _notifyIcon.ContextMenuStrip.Items.Add("Zamknij").Click += (s, e) => ExitApplication();
                _notifyIcon.Text = Resource.NotyfiIconText;
            }

            private void ExitApplication()
            {
                _isExit = true;
                MainWindow.Close();
                _notifyIcon.Dispose();
                _notifyIcon = null;
            }

            private void ShowMainWindow()
            {
                if (MainWindow.IsVisible)
                {
                    if (MainWindow.WindowState == WindowState.Minimized)
                    {
                        MainWindow.WindowState = WindowState.Normal;
                    }
                    MainWindow.Activate();
                }
                else
                {
                    MainWindow.Show();
                }
            }

            private void MainWindow_Closing(object sender, CancelEventArgs e)
            {
                if (!_isExit)
                {
                    e.Cancel = true;
                    MainWindow.Hide(); // A hidden window can be shown again, a closed one not
                }
            }
        }
}
