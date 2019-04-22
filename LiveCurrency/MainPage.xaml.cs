using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LiveCurrency
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

		private int gridHeight = 10;
		private int gridWidth = 15;

        public MainPage()
        {
            this.InitializeComponent();
			APIHelper.InitializeClient();
			LoadGrid();
        }

		private async void UpdateRate_Click(object sender, RoutedEventArgs e)
		{
			await LoadRate();
		}

		private async Task LoadRate()
		{
			var currency = await CurrencyProcessor.LoadCurrency();
		}

		private void LoadGrid()
		{

			for (int y = 0; y < gridHeight; y++)
			{

				TextBlock text = new TextBlock();
				CurrencyGraph.Children.Add(text);
				text.VerticalAlignment = VerticalAlignment.Bottom;
				text.HorizontalAlignment = HorizontalAlignment.Left;
				text.TextAlignment = TextAlignment.Center;

				text.Margin = new Thickness(-50, 0, 0, -10 + (y * 50));
				text.Height = 30;
				text.Width = 50;
				text.Text = y.ToString();

			}
			for (int x = 0; x < gridWidth; x++)
			{
				TextBlock text = new TextBlock();
				CurrencyGraph.Children.Add(text);
				text.VerticalAlignment = VerticalAlignment.Bottom;
				text.HorizontalAlignment = HorizontalAlignment.Left;
				text.TextAlignment = TextAlignment.Center;
				text.Margin = new Thickness(-10 + (x * 50), 0, 0, -50 );
				text.Height = 30;
				text.Width = 50;
				text.Text = x.ToString();
			}
		}
	}
}
