using CanvasWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace CanvasWPF.UserControls
{
	/// <summary>
	/// Interaction logic for ProductsDataGrid.xaml
	/// </summary>
	public partial class ProductsDataGrid : UserControl
	{
		public ObservableCollection<ProductV2> sourceList { get; set; }

	public ProductsDataGrid()
		{
			InitializeComponent();
			products.DataContext = this;
			sourceList = new ObservableCollection<ProductV2>();


			FillSomeData();
		}

		private void FillSomeData()
		{
			ObservableCollection<ProductV2> list = new ObservableCollection<ProductV2>();
			for (int i = 1; i <= 10; i++)
			{
				ProductV2 p = new ProductV2();
				p.additional = "additional" + i;
				p.catalogTruId = "catalogTruId" + i;
				p.description = "description" + i;
				p.exciseAmount = i * 100;
				p.exciseRate = 10;
				p.kpvedCode = "kpvedcode";
				p.ndsAmount = i * 110;
				p.ndsRate = 10;
				p.priceWithoutTax = i * 1000;
				p.priceWithTax = 1.1f * 1.1f * i * 1000;
				p.productDeclaration = "productDecl" + i;
				p.productNumberInDeclaration = "productNumberInDec" + i;
				p.quantity = 1;
				p.rowNumber = i;
				p.tnvedName = "tnvedName";
				p.truOriginCode = (i-1) % 6+1;
				p.turnoverSize = i * 1000;
				p.unitCode = "unitCOde" + i;
				p.unitNomenclature = "UnitNimenclature" + i;
				p.unitPrice = 1000;
				AddRow(p);				
			}
		}

		private void AddRow(ProductV2 p)
		{
			sourceList.Add(p);
		}

		private void isEditable_Click(object sender, RoutedEventArgs e)
		{
			if (isEditable.IsChecked == true)
			{
				isEditable.Background = Brushes.PaleGreen;
				isEditable.Foreground = Brushes.Black;
				btnAddProduct.Visibility = Visibility.Visible;
				btnAddProduct.IsEnabled = true;
				btnDeleteProduct.Visibility = Visibility.Visible;
				btnEditProduct.Visibility = Visibility.Visible;
				currencyCodeBlock.Visibility = Visibility.Collapsed;
				currencyRateBlock.Visibility = Visibility.Collapsed;
				currencyCodeBox.Visibility = Visibility.Visible;
				currencyRateBox.Visibility = Visibility.Visible;
				productForm.productGrid.IsEnabled = true;
			}
			else
			{
				isEditable.Background = Brushes.LightGray;
				isEditable.Foreground = Brushes.SteelBlue;
				btnAddProduct.Visibility = Visibility.Hidden;
				btnDeleteProduct.Visibility = Visibility.Hidden;
				btnEditProduct.Visibility = Visibility.Hidden;
				currencyCodeBlock.Visibility = Visibility.Visible;
				currencyRateBlock.Visibility = Visibility.Visible;
				currencyCodeBox.Visibility = Visibility.Collapsed;
				currencyRateBox.Visibility = Visibility.Collapsed;
				productForm.productGrid.IsEnabled = false;
			}
		}

		private void btnAddProduct_Click(object sender, RoutedEventArgs e)
		{
			productForm.productGrid.Visibility = Visibility.Visible;
		}

		private void products_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (products.SelectedItems.Count == 0)
			{
				btnDeleteProduct.IsEnabled = false;
				btnEditProduct.IsEnabled = false;
			}
			else if (products.SelectedItems.Count == 1)
			{
				btnEditProduct.IsEnabled = true;
				btnDeleteProduct.IsEnabled = true;
				productForm.fillByProduct((ProductV2)products.SelectedItem);
			}
			else
			{
				btnEditProduct.IsEnabled = false;
				btnDeleteProduct.IsEnabled = true;
			}
		}

		private void btnAddProduct_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			btnAddProduct.Background = btnAddProduct.IsEnabled ? Brushes.PaleGreen : Brushes.LightGray;
		}

		private void btnDeleteProduct_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			btnDeleteProduct.Background = btnDeleteProduct.IsEnabled ? Brushes.PaleGreen : Brushes.LightGray;
		}

		private void btnEditProduct_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			btnEditProduct.Background = btnEditProduct.IsEnabled ? Brushes.PaleGreen : Brushes.LightGray;
		}

		private void btnDeleteProduct_Click(object sender, RoutedEventArgs e)
		{
			int temp = products.SelectedItems.Count;
			for (int i = 0; i < temp; i++)
			{
				sourceList.Remove((ProductV2)products.SelectedItem);
			}

				

		}

		private void btnEditProduct_Click(object sender, RoutedEventArgs e)
		{
			productForm.fillByProduct((ProductV2)products.SelectedItem);
			productForm.productGrid.Visibility = Visibility.Visible;
		}
	}
}
