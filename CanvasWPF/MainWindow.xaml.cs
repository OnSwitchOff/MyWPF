using CanvasWPF.Models;
using CanvasWPF.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace CanvasWPF
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public static int topZIndex = 10000;

		public MainWindow()
		{			

			InitializeComponent();

			/*InfoCardModel sellerCard = new InfoCardModel();
			sellerCard.title = "Seller";
			sellerCard.imagePath = "Resources/images/operator.png";
			sellerCard.mainFields.Add("tin", "123456789012");
			sellerCard.mainFields.Add("Name", "ИП Пинчук Виталий Васильевич");
			sellerCard.mainFields.Add("Address", "Казахстан, Акмолинская обл., г.Степногорск, ул. 3, д. 41");
			sellerCard.additionalProperties.Add("Address1", "Казахстан, Акмолинская обл., г.Степногорск, ул. 3, д. 41");
			sellerCard.additionalProperties.Add("Address2", "Казахстан, Акмолинская обл., г.Степногорск, ул. 3, д. 41");

			InfoCard infoCard1 = new InfoCard(sellerCard, mainPanel ,true);
			InfoCard infoCard2 = new InfoCard(sellerCard, mainPanel, false);
			Canvas.SetTop(infoCard1, 0);
			Canvas.SetLeft(infoCard1, 0);
			mainPanel.Children.Add(infoCard1);

			Canvas.SetTop(infoCard2, 0);
			Canvas.SetLeft(infoCard2, 0);
			mainPanel.Children.Add(infoCard2);*/

			ProductsDataGrid productsDataGrid = new ProductsDataGrid();
			/*Canvas.SetTop(productsDataGrid, 600);
			Canvas.SetLeft(productsDataGrid, 0);*/
			mainPanel.Children.Add(productsDataGrid);
		}
	}
}
