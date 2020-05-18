using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TopoGiraffe.Noyau;
using TopoSurf.MessageBoxStyle;
using System.Runtime.InteropServices;
using System.Windows.Interop;



namespace TopoGiraffe
{
	public partial class MainWindow : Window
	{

		public MainWindow()
		{
		 

		InitializeComponent();


		}

		private void EXIT(object sender, MouseButtonEventArgs e)
		{
			MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Vous êtes sûr?", "Confirmation", System.Windows.MessageBoxButton.YesNo);
			if (messageBoxResult == MessageBoxResult.Yes)
			{
				Environment.Exit(0);

			}
		}

		private void MINIMIZE(object sender, MouseButtonEventArgs e)
		{
			this.WindowState = WindowState.Minimized;
		}


		private void MAX_RESTORE(object sender, MouseButtonEventArgs e)
		{
			if (this.WindowState == WindowState.Normal) this.WindowState = WindowState.Normal;
			else this.WindowState = WindowState.Normal;
		}







		public const int WM_NCLBUTTONDOWN = 0xA1;
		public const int HT_CAPTION = 0x2;

		[DllImportAttribute("user32.dll")]
		public static extern int SendMessage(IntPtr hWnd, int Msg,
				int wParam, int lParam);

		[DllImportAttribute("user32.dll")]
		public static extern bool ReleaseCapture();

		public void move_window(object sender, MouseButtonEventArgs e)
		{
			ReleaseCapture();
			SendMessage(new WindowInteropHelper(this).Handle,
				WM_NCLBUTTONDOWN, HT_CAPTION, 0);
		}

		private void mainFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
		{

		}

		private void Exit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Vous êtes sûr?", "Confirmation", System.Windows.MessageBoxButton.YesNo);
			if (messageBoxResult == MessageBoxResult.Yes)
			{
				Environment.Exit(0);

			}
		}
	}
}