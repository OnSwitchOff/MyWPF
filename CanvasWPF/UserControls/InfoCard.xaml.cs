using CanvasWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CanvasWPF.UserControls
{
	/// <summary>
	/// Interaction logic for InfoCard.xaml
	/// </summary>
	public partial class InfoCard : UserControl
	{	
		private Canvas parent;
		private bool isReadOnly;
		private bool isMovable = true;
		private bool moving = false;
		private bool isCollapsed = false;
		BitmapImage bmpCollapse;
		BitmapImage bmpPin;
		BitmapImage bmpUnPin;
		BitmapImage bmpExpand;
		Point start;
		Point finish;

		public InfoCard(InfoCardModel info, Canvas canvas , bool readOnly)
		{
			InitializeComponent();
			isReadOnly = readOnly;
			SetInfoCard(info);
			parent = canvas;
		}

		private void SetInfoCard(InfoCardModel info)
		{
			title.Text = info.title;

			ColumnDefinition column1 = new ColumnDefinition();
			column1.Width = new GridLength(80);
			gCard.ColumnDefinitions.Add(column1);


			ColumnDefinition column2 = new ColumnDefinition();
			column2.Width = new GridLength(1,GridUnitType.Auto);
			gCard.ColumnDefinitions.Add(column2);


			ColumnDefinition column3 = new ColumnDefinition();
			column3.Width = new GridLength(1, GridUnitType.Star);
			gCard.ColumnDefinitions.Add(column3);

			int i = 0;
			foreach  (KeyValuePair<string,string> field in info.mainFields)
			{
				
				RowDefinition row = new RowDefinition();
				row.Height = new GridLength(1, GridUnitType.Auto);
				gCard.RowDefinitions.Add(row);
				TextBlock key = new TextBlock();
				key.Text = field.Key + ":";
				Grid.SetRow(key, i);
				Grid.SetColumn(key, 1);
				key.Padding = new Thickness(10, 0, 10, 0);
				key.VerticalAlignment = VerticalAlignment.Center;
				gCard.Children.Add(key);
				TextBox value = new TextBox();
				value.Text = field.Value;
				value.Background = new SolidColorBrush(){ Opacity = 0 };
				Grid.SetRow(value, i);
				Grid.SetColumn(value, 2);
				value.Padding = new Thickness(0, 2, 0, 2);
				value.VerticalAlignment = VerticalAlignment.Center;
				value.IsReadOnly = isReadOnly;
				value.Margin = new Thickness(0, 2, 10, 2);
				value.TextWrapping = TextWrapping.Wrap;
				gCard.Children.Add(value);
				i++;
			}

			Image pic = new Image();
			pic.Width = 64;
			pic.Height = 64;
			BitmapImage bmp = new BitmapImage();
			
			bmp.BeginInit();
			string projectName = Assembly.GetCallingAssembly().GetName().Name;
			bmp.UriSource = new Uri("pack://application:,,,/"+projectName+";component/Resources/images/operator.png");
			bmp.EndInit();
			pic.Source = bmp;


			bmpPin = new BitmapImage();
			bmpPin.BeginInit();
			bmpPin.UriSource = new Uri("pack://application:,,,/" + projectName + ";component/Resources/images/pin.png");
			bmpPin.EndInit();

			bmpUnPin = new BitmapImage();
			bmpUnPin.BeginInit();
			bmpUnPin.UriSource = new Uri("pack://application:,,,/" + projectName + ";component/Resources/images/unpin.png");
			bmpUnPin.EndInit();

			bmpCollapse = new BitmapImage();
			bmpCollapse.BeginInit();
			bmpCollapse.UriSource = new Uri("pack://application:,,,/" + projectName + ";component/Resources/images/collapse.png");
			bmpCollapse.EndInit();

			bmpExpand = new BitmapImage();
			bmpExpand.BeginInit();
			bmpExpand.UriSource = new Uri("pack://application:,,,/" + projectName + ";component/Resources/images/expand.png");
			bmpExpand.EndInit();

			Grid.SetRowSpan(pic, i);
			
			gCard.Children.Add(pic);

			foreach (KeyValuePair<string, string> prop in info.additionalProperties)
			{
				RowDefinition row = new RowDefinition();
				row.Height = new GridLength(1,GridUnitType.Auto);
				gCard.RowDefinitions.Add(row);
				TextBlock key = new TextBlock();
				key.Text = prop.Key+":";
				Grid.SetRow(key, i);
				Grid.SetColumn(key, 0);
				key.Padding = new Thickness(10, 0, 10, 0);
				key.VerticalAlignment = VerticalAlignment.Center;
				gCard.Children.Add(key);
				TextBox value = new TextBox();
				value.Text = prop.Value;
				value.Background = new SolidColorBrush() { Opacity = 0 };
				value.VerticalAlignment = VerticalAlignment.Center;
				Grid.SetRow(value, i);
				Grid.SetColumn(value, 1);
				Grid.SetColumnSpan(value, 2);
				value.Padding = new Thickness(0, 2 , 0, 2);
				value.Margin = new Thickness(0, 2, 10, 2);
				value.IsReadOnly = isReadOnly;
				value.TextWrapping = TextWrapping.Wrap;
				gCard.Children.Add(value);

				i++;				
			}
		}

		private void btnMove_MouseEnter(object sender, MouseEventArgs e)
		{
			Mouse.OverrideCursor = Cursors.Hand;
		}

		private void btnMove_MouseDown(object sender, MouseButtonEventArgs e)
		{
			Panel.SetZIndex(this, MainWindow.topZIndex);
			MainWindow.topZIndex++;
			if (isMovable)
			{
				moving = true;
				Mouse.OverrideCursor = Cursors.ScrollAll;
				start = Mouse.GetPosition(parent);
				this.MouseMove += btnMove_MouseMove;
			}
		}

		private void btnMove_MouseMove(object sender, MouseEventArgs e)
		{
			if (moving)
			{
				finish = Mouse.GetPosition(parent);
				double oldLeft = Canvas.GetLeft(this);
				double oldTop = Canvas.GetTop(this);
				double newLeft = oldLeft + finish.X - start.X;
				double newTop = oldTop + finish.Y - start.Y;
				if (newLeft < 0)
				{
					newLeft = 0;
					moving = false;
					Mouse.OverrideCursor = Cursors.Hand;
					this.MouseMove -= btnMove_MouseMove;
				}
				else if (newLeft > (parent.ActualWidth - this.ActualWidth))
				{
					newLeft = parent.ActualWidth - this.ActualWidth;
					moving = false;
					Mouse.OverrideCursor = Cursors.Hand;
					this.MouseMove -= btnMove_MouseMove;
				}
				if (newTop < 0)
				{
					newTop = 0;
					moving = false;
					Mouse.OverrideCursor = Cursors.Hand;
					this.MouseMove -= btnMove_MouseMove;
				}
				else if (newTop > (parent.ActualHeight - this.ActualHeight))
				{
					newTop = parent.ActualHeight - this.ActualHeight;
					moving = false;
					Mouse.OverrideCursor = Cursors.Hand;
					this.MouseMove -= btnMove_MouseMove;
				}
			
				Canvas.SetLeft(this,newLeft);
				Canvas.SetTop(this,newTop);
				start = finish;
			}
		}

		private void btnMove_MouseUp(object sender, MouseButtonEventArgs e)
		{
			if (isMovable)
			{
				moving = false;
				Mouse.OverrideCursor = Cursors.Hand;
				this.MouseMove -= btnMove_MouseMove;
			}
		}

		private void btnMove_MouseLeave(object sender, MouseEventArgs e)
		{
			Mouse.OverrideCursor = Cursors.Arrow;
		}



		private void btnPin_MouseUp(object sender, MouseButtonEventArgs e)
		{
			isMovable = !isMovable;
			if (isMovable)
			{				
				btnPin.Background = Brushes.PaleGreen;
				//imgPin.Source = bmpPin;
			}
			else
			{
				btnPin.Background = Brushes.Firebrick;
				//imgPin.Source = bmpUnPin;
			}
		}

		private void btnCollapse_MouseUp(object sender, MouseButtonEventArgs e)
		{
			isCollapsed = !isCollapsed;
			if (isCollapsed)
			{
				gCard.Visibility = Visibility.Collapsed;
				imgCollapse.Source = bmpExpand; 
			}
			else
			{
				gCard.Visibility = Visibility.Visible;
				imgCollapse.Source = bmpCollapse;
			}
		}
	}
}
