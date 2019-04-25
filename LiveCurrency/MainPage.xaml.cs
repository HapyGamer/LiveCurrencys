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
using Windows.UI.Xaml.Shapes;
using Windows.UI;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LiveCurrency
{
    
	public class AUDRates
	{
		public string Name { get; set; }
		public double Rate { get; set; }
	}

    public sealed partial class MainPage : Page
    {		
		private int gridWidth = 10;
		private List<string> currencies = new List<string>();

		public MainPage()
        {
            this.InitializeComponent();
			APIHelper.InitializeClient();
        }



		private async void LoadGrid(List<string> currencyRates)
		{
			CurrencyGraph.Series.Clear();
			var labelStyle = new Style(typeof(AxisLabel));
			labelStyle.Setters.Add(new Setter(AxisLabel.StringFormatProperty, "{##.###}"));
			LinearAxis linearAxis = new LinearAxis { Orientation = AxisOrientation.Y, ShowGridLines = true };
			for (int i = 0; i < currencies.Count; i++)
			{
				List<AUDRates> rates = new List<AUDRates>();
				for (int x = 0; x <= gridWidth; x++)
				{
					var dateAndTime = DateTime.Now.AddDays(-gridWidth + x);
					CurrencyModel rate;

					if (x < gridWidth)
						rate = await CurrencyProcessor.LoadCurrency(dateAndTime.Year, dateAndTime.Month, dateAndTime.Day);

					else
						rate = await CurrencyProcessor.LoadCurrency();

					var text = dateAndTime.Date.ToString("dd/MM/yyyy");
					rates.Add(new AUDRates() { Name = text, Rate = (double)Math.Round(rate.Rates[currencyRates[i]], 4) });
				}

				//create new graph

				LineSeries lineseries = new LineSeries();
				lineseries.Title = currencyRates[i] + "Rates";
				lineseries.Margin = new Thickness(0, 0, 0, 0);
				lineseries.IndependentValuePath = "Name";
				lineseries.DependentValuePath = "Rate";
				lineseries.IsSelectionEnabled = true;

				CurrencyGraph.Series.Add(lineseries);

				
				(CurrencyGraph.Series[i] as LineSeries).ItemsSource = rates;
				linearAxis = new LinearAxis { Orientation = AxisOrientation.Y, ShowGridLines = true,Title = currencyRates[i]};
				lineseries.DependentRangeAxis = linearAxis;
				var axis = (LinearAxis)lineseries.DependentRangeAxis;
				axis.AxisLabelStyle = labelStyle;
			}
		}

		private void EnableShowRates(object sender, RoutedEventArgs e)
		{
			CheckBox CheckBox = sender as CheckBox;
			string currency = CheckBox.Content.ToString();
			currencies.Add(currency);
		}

		private void DisableShowRates(object sender, RoutedEventArgs e)
		{
			CheckBox CheckBox = sender as CheckBox;
			string currency = CheckBox.Content.ToString();
			if (currencies.Count > 0)
			{
				for(int i = 0; i < currencies.Count; i++)
				{
					if (currencies[i] == currency)
					{
						currencies.RemoveAt(i);
					}
				}
			}
			
		}

		private void UpdateTheGraph(object sender, RoutedEventArgs e)
		{
			if(currencies.Count > 0)
				LoadGrid(currencies);
		}
	}
}
