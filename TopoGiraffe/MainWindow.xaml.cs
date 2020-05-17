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
			Environment.Exit(0);
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

		private void Activate_Title_Icons(object sender, MouseEventArgs e)
		{
			//hover effect, make sure your grid is named "Main" or replace "Main" with the name of your grid
			Close_btn.Fill = (ImageBrush)Main.Resources["Close_act"];
			Min_btn.Fill = (ImageBrush)Main.Resources["Min_act"];
			Max_btn.Fill = (ImageBrush)Main.Resources["Max_act"];
		}

		private void Deactivate_Title_Icons(object sender, MouseEventArgs e)
		{
			Close_btn.Fill = (ImageBrush)Main.Resources["Close_inact"];
			Min_btn.Fill = (ImageBrush)Main.Resources["Min_inact"];
			Max_btn.Fill = (ImageBrush)Main.Resources["Max_inact"];
		}

		private void Close_pressing(object sender, MouseButtonEventArgs e)
		{
			Close_btn.Fill = (ImageBrush)Main.Resources["Close_pr"];
		}

		private void Min_pressing(object sender, MouseButtonEventArgs e)
		{
			Min_btn.Fill = (ImageBrush)Main.Resources["Min_pr"];
		}

		private void Max_pressing(object sender, MouseButtonEventArgs e)
		{
			Max_btn.Fill = (ImageBrush)Main.Resources["Max_pr"];
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



		
	}
}