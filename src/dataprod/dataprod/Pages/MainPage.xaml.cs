using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
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
using Bing.Maps;
using CommonLib.Numerical;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;

namespace dataprod
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
		List<testDataTemplate> dataBuilder = new List<testDataTemplate>();
	    private class testDataTemplate
	    {
		    public  string year { get; set; }
		    public  double value { get; set; }
	    }

	    private bool layer = false;

	    public MainPage()
        {
            this.InitializeComponent();

        }
		private async void UserPrompt_OnKeyDown(object sender, KeyRoutedEventArgs e)
		{

			//OPERATOR, <EMPLOYMENT/SOC>, <AS>, <GRAPH>, <BETWEEN>, <start:end>, <filter> <filtertype>
			//show programming as line
			 if (e.Key == VirtualKey.Enter)
			 {
				
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
								dataBuilder = await GetYearEmploymentDataSmart(commands);
								
								break;
							}

						ShowChart(commands.Length > 2 ? commands[3] : "line", dataBuilder); //default to line if <AS> not given
						break;
					case "map": //MEEEEEH, someone give me a polygon map
						dynGrid.Visibility = Visibility.Collapsed;
						mappletGrid.Visibility = Visibility.Visible;
						BINGIT.MapType = MapType.HighContrast;

						var loc = new Location(53.136954, -1.392331);
						BINGIT.SetView(loc, 7.00);

						var pushpin = new Pushpin()
						{
							Text = "pins yo'"
						};
						MapLayer.SetPosition(pushpin, new Location
						{
							
						});
						BINGIT.Children.Add(pushpin);


						Debug.WriteLine(await dataGrabber.LMI.essRegionJason(1, "2136"));
						break;
					case "analyse": //check degree from the normal
						switch (commands[2])
						{
							case "regression" :

								switch (commands[3])
								{
									case "auto" :

										if (commands[1] == "all")
										{
											var socs = new List<double>
											{
												2136,
												2213,
												2413,
												2311,
												2213,
												2451,
												4162,
												4216,
												3131,
												3213,
												8212,
												1190,
												1225,
												1241,
												1251,
												2111


											};

											var regressionsToPlot = new List<testDataTemplate>();

											foreach (var soc in socs) //waaa, this code is so un-refactored.
											{
												var y = await dataGrabber.LMI.wfpredict(soc.ToString(), "2013", "2020");
												var d = y.predictedEmployment.Select(
														data => new testDataTemplate { year = data.year.ToString(), value = data.employment }).ToList();

												var years = d.Select(year => Convert.ToDouble(year.year)).ToList();
												var values = d.Select(year => Convert.ToDouble(year.value)).ToList();
												var ds = new XYDataSet(years, values);

												double smallestYear = years.Min();
												double smallestValue = values.Min();

												for (int i = 0; i < years.Count; i++) //normalise
												{
													years[i] -= smallestYear;
												}
												for (int i = 0; i < values.Count; i++)
												{
													values[i] -= smallestValue;
												}
												OutputGrid.Visibility = Visibility.Visible;

												var gradient = ds.ComputeRSquared();

												var deviation = d.Select(data => data.value - (Convert.ToDouble(data.year) * (gradient))).ToList();

												double deviationAverage = deviation.Sum();

												deviationAverage = deviationAverage / deviation.Count;

												regressionsToPlot.Add(new testDataTemplate
												{
													year = soc.ToString(), //crap names is for graphing stuff, needs recode
													value = deviationAverage
												});

												ShowChart("column", regressionsToPlot);
											}
											regressionsToPlot = regressionsToPlot.OrderBy(x => x.value).ToList();
											ShowChart("column", regressionsToPlot);


										}
										else //do all
										{
											var d = await GetYearEmploymentDataSmart(commands);

											var years = d.Select(year => Convert.ToDouble(year.year)).ToList();
											var values = d.Select(year => Convert.ToDouble(year.value)).ToList();
											var ds = new XYDataSet(years, values);

											double smallestYear = years.Min();
											double smallestValue = values.Min();

											for (int i = 0; i < years.Count ; i++) //normalise
											{
												years[i] -= smallestYear;
											}
											for (int i = 0; i < values.Count; i++)
											{
												values[i] -= smallestValue;
											}
											OutputGrid.Visibility = Visibility.Visible;

											var gradient = ds.ComputeRSquared();

											var deviation = d.Select(data => data.value - (Convert.ToDouble(data.year)*(gradient))).ToList();

											double deviationAverage = deviation.Sum();

											deviationAverage = deviationAverage/deviation.Count;

											OutputGrid.Visibility = Visibility.Visible;
											Output.Visibility = Visibility.Visible;

											Output.Text = "Average Deviation: " + deviationAverage + "\n";
											Output.Text += "Slope: " + Math.Round(ds.Slope, 2) + "\n";
											Output.Text += "YIntercept: " + Math.Round(ds.YIntercept, 2) + "\n";
											Output.Text += "Rsquared: " + Math.Round(ds.ComputeRSquared(), 3) + "\n"; 
										}

										break;
								}

								break;
						}
						break;
					//case "analyse":
					//	var year = commands[1];
					//	var detailBuilder = new Templates.DetailedYear();
					//	dynGrid.Visibility = Visibility.Collapsed;
					//	detailView.Visibility = Visibility.Visible;

					//	for (int i = 0; i < 8000; i++)
					//	{
					//		try
					//		{
					//			var x = await dataGrabber.LMI.wfpredict(i.ToString(), year, year);
					//			if detailBuilder[x.soc]
					//		}
					//		catch (Exception ex)
					//		{
								
					//		}
							
					//	}

						//(DetailChart1.Series[0] as ColumnSeries).ItemsSource = detailBuilder.employments;
						//break;

					case ":" :
						switch (commands[1])
						{
							case "reset" :
								ClearGraph();
								break;
							case "layer" :
								layer = true;
								break;
							case "unlayer":
								layer = false;
								break;
							case "regression":

								if (dataBuilder.Count > 0) //compare to normal reg line
								{
									//var d = await GetYearEmploymentDataSmart(commands);

									var d = dataBuilder;

									var years = d.Select(year => Convert.ToDouble(year.year)).ToList();
									var values = d.Select(year => Convert.ToDouble(year.value)).ToList();
									var ds = new XYDataSet(years, values);

									double smallestYear = years.Min();
									double smallestValue = values.Min();

									//var yearsReg = new List<double>();
									//var valuesReg = new List<double>();

									var y = new List<testDataTemplate>();

									const double reg = 2000;

									for (int i = 0; i < years.Count; i++)
									{
										y.Add(new testDataTemplate
										{
											year = (smallestYear + i).ToString(), //fine
											value = smallestValue + (i*reg)
										});

										//yearsReg.Add(smallestYear + i);
										//valuesReg.Add(smallestValue + (smallestYear + i)*reg);
									}

									ShowChartReg(y);


									//OutputGrid.Visibility = Visibility.Visible;

									//Output.Text = "Slope: " + Math.Round(ds.Slope, 2) + "\n";
									//Output.Text += "YIntercept: " + Math.Round(ds.YIntercept, 2) + "\n";
									//Output.Text += "Rsquared: " + Math.Round(ds.ComputeRSquared(), 3) + "\n";
								}
								break;
								}
						break;
				}

				
			}

		}

	    private void ClearGraph()
	    {
			(MainChart1.Series[0] as AreaSeries).ItemsSource = null;
			(MainChart1.Series[1] as BarSeries).ItemsSource = null;
			(MainChart1.Series[2] as BubbleSeries).ItemsSource = null;
			(MainChart1.Series[3] as ColumnSeries).ItemsSource = null;
			(MainChart1.Series[4] as LineSeries).ItemsSource = null;
			(MainChart1.Series[5] as PieSeries).ItemsSource = null;
			(MainChart1.Series[6] as ScatterSeries).ItemsSource = null;
			(MainChart1.Series[7] as LineSeries).ItemsSource = null;
	    }

	    private async Task<List<testDataTemplate>> GetYearEmploymentDataSmart(string[] commands)
	    {
		    string soc;
			if (commands[1].Contains("soc"))
			{
				soc = commands[1].Split(':')[1];
			}
			else
			{
				soc = (await(dataGrabber.LMI.socSearch(commands[1]))).FirstOrDefault().soc.ToString();
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
			//if (commands.Length > 6)
			//{
			//	var y = await dataGrabber.LMI.wffilterpredict(soc, commands[5], startYear, endYear);

			//}
			//else
			//{
				var y = await dataGrabber.LMI.wfpredict(soc, startYear, endYear);
				return
					y.predictedEmployment.Select(
						data => new testDataTemplate { year = data.year.ToString(), value = data.employment }).ToList();

			//}
	    }

	    private void ShowChartReg(List<testDataTemplate> reg)
	    {
		    (MainChart1.Series[7] as LineSeries).ItemsSource = null;
			//(MainChart1.Series[4] as LineSeries).ItemsSource = data;
			(MainChart1.Series[7] as LineSeries).ItemsSource = reg;
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

			if (layer == false)
		    {
				(MainChart1.Series[0] as AreaSeries).ItemsSource = null;
				(MainChart1.Series[1] as BarSeries).ItemsSource = null;
				(MainChart1.Series[2] as BubbleSeries).ItemsSource = null;
				(MainChart1.Series[3] as ColumnSeries).ItemsSource = null;
				(MainChart1.Series[4] as LineSeries).ItemsSource = null;
				(MainChart1.Series[5] as PieSeries).ItemsSource = null;
				(MainChart1.Series[6] as ScatterSeries).ItemsSource = null;    
		    }

		    

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
