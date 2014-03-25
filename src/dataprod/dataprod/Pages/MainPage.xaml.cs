using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Storage.Search;
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
		    public  double value { get; set; }
	    }

	    public MainPage()
        {
            this.InitializeComponent();

        }
		private async void UserPrompt_OnKeyDown(object sender, KeyRoutedEventArgs e)
		{

			//OPERATOR, <EMPLOYMENT/SOC>, <AS>, <GRAPH>, <BETWEEN>, <start:end>
			//show programming as line
			 if (e.Key == VirtualKey.Enter)
			{
				var dataBuilder = new List<testDataTemplate>();
				var commands = UserPrompt.Text.ToLower().Split(new char[] { ' ', ',' });

				switch (commands[0])
				{
					case "show":
						switch (commands[1])
						{
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

							default:
								string soc;
								if (commands[1].Contains("soc"))
								{
									soc = commands[1].Split(':')[1];
								}
								else
								{
									soc = (await (dataGrabber.LMI.socSearch(commands[1]))).FirstOrDefault().soc.ToString();
								}
								string startYear;
								string endYear;

								if (commands.Length > 4)
								{
									startYear = commands[5].Split(':')[0];  
									endYear = commands[5].Split(':')[1];
								}
								else
								{
									startYear = "2013";
									endYear = "2020";
								}

								var y = await dataGrabber.LMI.wfpredict(soc, startYear, endYear);
								dataBuilder = y.predictedEmployment.Select(data => new testDataTemplate { year = data.year.ToString(), value = data.employment } ).ToList();
								break;
							}

						//how
						ShowChart(commands[2] == "as" ? commands[3] : "line", dataBuilder); //default to line if <AS> not given
						break;
					case "analyse":
						throw new NotImplementedException();
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
