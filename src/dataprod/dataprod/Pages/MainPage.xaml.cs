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

			//OPERATOR DATA <AS> <GRAPH> <FOR> <JOB/SOC>
			if (e.Key == VirtualKey.Enter)
			{
				var dataBuilder = new List<testDataTemplate>();
				var commands = UserPrompt.Text.ToLower().Split(new char[] { ' ', ',' });

				//data
				switch (commands[1])
				{
					case "current employment":
						break;

					case "testdata":
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
						dataBuilder = testData;
						break;
				}

				//how
				if (commands[2] == "as")
				{
					//switch (commands[3])
					//{
					//	default:
					//		
					//		break;
					//}
				}
				else
				{
						
				}
				//operator
				switch (commands[0])
				{
					case "show":
						ShowChart(commands[3], dataBuilder);
						break;
					case "analyse":

						break;
				}
			}

		}

	    private void ShowChart(string chartType, List<testDataTemplate> data )
	    {

			//var dataChart = new Chart();
			//dynGrid.Children.Add(dataChart);
			//dataChart.Series.Add(new ScatterSeries
			//{
			//	Title = "Programmer",
			//	IndependentValuePath = "year",5
			//	DependentValuePath = "value",
			//	ItemsSource = testData
			//} );

		    (MainChart1.Series[0] as AreaSeries).ItemsSource = null;
		    (MainChart1.Series[1] as BarSeries).ItemsSource = null;
			(MainChart1.Series[2] as BubbleSeries).ItemsSource = null;
			(MainChart1.Series[3] as ColumnSeries).ItemsSource = null;
			(MainChart1.Series[4] as LineSeries).ItemsSource = null;
			(MainChart1.Series[5] as PieSeries).ItemsSource = null;
			(MainChart1.Series[6] as ScatterSeries).ItemsSource = null;

		    switch (chartType)
		    {
				case "area":
					(MainChart1.Series[0] as AreaSeries).ItemsSource = data;
					break;
				case "bar":
					(MainChart1.Series[1] as BarSeries).ItemsSource = data;
					break;
				case "bubble":
					(MainChart1.Series[2] as BubbleSeries).ItemsSource = data;
					break;
				case "column":
					(MainChart1.Series[3] as ColumnSeries).ItemsSource = data;
					break;
				case "line":
					(MainChart1.Series[4] as LineSeries).ItemsSource = data;
					break;
				case "pie":
					(MainChart1.Series[5] as PieSeries).ItemsSource = data;
					break;
				case "scatter":
					(MainChart1.Series[6] as ScatterSeries).ItemsSource = data;
					break;
		    }


		    //(MainChart1.Series[0] as ScatterSeries).ItemsSource = testData;

			//<Charting:AreaSeries Margin="0" IndependentValuePath="year" DependentValuePath="value" IsSelectionEnabled="True"/>
			//<Charting:BarSeries Margin="0" IndependentValuePath="year" DependentValuePath="value" IsSelectionEnabled="True"/>
			//<Charting:BubbleSeries Margin="0" IndependentValuePath="year" DependentValuePath="value" IsSelectionEnabled="True"/>
			//<Charting:ColumnSeries Margin="0" IndependentValuePath="year" DependentValuePath="value" IsSelectionEnabled="True"/>
			//<Charting:LineSeries Margin="0" IndependentValuePath="year" DependentValuePath="value" IsSelectionEnabled="True"/>
			//<Charting:PieSeries Margin="0" IndependentValuePath="year" DependentValuePath="value" IsSelectionEnabled="True"/>
			//<Charting:ScatterSeries Margin="0" IndependentValuePath="year" DependentValuePath="value" IsSelectionEnabled="True"/>

			//(MainChart1.Series[1] as PieSeries).ItemsSource = testData;

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
