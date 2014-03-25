using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238
using WinRTXamlToolkit.Controls.DataVisualization.Charting;

namespace dataprod
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
	    private class testDataTemplate
	    {
		    public  string year { get; set; }
		    public  int value { get; set; }
	    }

	    public MainPage()
        {
            this.InitializeComponent();

        }
		private void UserPrompt_OnKeyDown(object sender, KeyRoutedEventArgs e)
		{
			if (e.Key == VirtualKey.Enter)
			{
				switch (UserPrompt.Text)
				{
					case "pie":
						ShowChartPie();
						break;

					default :
						break;
					
				}
			}

		}

	    private void ShowChartPie()
	    {
		    var testData = new List<testDataTemplate>
		    {
			    new testDataTemplate {year = "2011", value = 14}, 
				new testDataTemplate {year = "2012", value = 15}, 
				new testDataTemplate {year = "2013", value = 16}, 
				new testDataTemplate {year = "2014", value = 20}, 
				new testDataTemplate {year = "2015", value = 18}, 

				new testDataTemplate {year = "2016", value = 20}, 
				new testDataTemplate {year = "2017", value = 24}, 
				new testDataTemplate {year = "2018", value = 27}, 
				new testDataTemplate {year = "2019", value = 29}, 
				new testDataTemplate {year = "2020", value = 24}, 

				new testDataTemplate {year = "2021", value = 31}, 
				new testDataTemplate {year = "2022", value = 32}, 
				new testDataTemplate {year = "2023", value = 33}, 
				new testDataTemplate {year = "2024", value = 39}, 
				new testDataTemplate {year = "2025", value = 37}, 

				new testDataTemplate {year = "2026", value = 42}, 
				new testDataTemplate {year = "2027", value = 43}, 
				new testDataTemplate {year = "2028", value = 44}, 
				new testDataTemplate {year = "2029", value = 45}, 
				new testDataTemplate {year = "2030", value = 41}, 
		    };

			//var dataChart = new Chart();
			//dynGrid.Children.Add(dataChart);
			//dataChart.Series.Add(new ScatterSeries
			//{
			//	Title = "Programmer",
			//	IndependentValuePath = "year",5
			//	DependentValuePath = "value",
			//	ItemsSource = testData
			//} );


		    (MainChart1.Series[0] as ScatterSeries).ItemsSource = testData;

			//MainChart1.Series.Add(new ScatterSeries
			//{
			//	Title = "ComputerScience",
			//	IndependentValuePath = "year",
			//	DependentValuePath = "value",
			//	ItemsSource = testData
			//});
	    }
    }
}
