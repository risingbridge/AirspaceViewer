using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JsonCreator
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private string FlightRule = "IFR";
		int jsonCounter = 0;
		string jsonStart = "{\"type\":\"FeatureCollection\",\"features\":[";
		string jsonEnd = "]}";
		string jsonContent = "";



		public MainWindow()
		{
			InitializeComponent();
			
		}

		private void AddPointButtonPress(object sender, RoutedEventArgs e)
		{
			if (FixName.Text == string.Empty || LatitudeInput.Text.Length < 6 || LongitudeInput.Text.Length < 7)
			{
				MessageBox.Show("Check your input!");
			}
			else
			{
				AddPoint();
			}
		}

		private void IFRselectedPressed(object sender, RoutedEventArgs e)
		{
			IFRselected.IsChecked = true;
			VFRselected.IsChecked = false;
			FlightRule = "IFR";
		}

		private void VFRselectedPressed(object sender, RoutedEventArgs e)
		{
			VFRselected.IsChecked = true;
			IFRselected.IsChecked = false;
			FlightRule = "VFR";
		}

		private void AddPoint()
		{
			string PointName = FixName.Text;
			float lat = LatToDecLat(LatitudeInput.Text);
			float lng = LngToDecLng(LongitudeInput.Text);

			//JSON-eksempel
			//{
			//"type": "Feature",
			//"geometry": {
			//"type": "Point",
			//"coordinates": [

			//"23.371666666666666",
			//"69.97611111111111"
			//]
			//},
			//"properties": {
			//"Name": "Alta",
			//"ICAO": "ENAT",
			//"metar": "ENAT 041350Z 15005KT CAVOK 04/M00 Q0991 RMK WIND 700FT 17009KT",
			//"taf": "TAF ENAT 041100Z 0412/0420 17008KT CAVOK"

			//}
			//}
			string CurrentFixJson = "{"
									+ "\"type\": \"Feature\","
									+ "\"geometry\": {"
									+ "\"type\": \"Point\","
									+ "\"coordinates\":["
									+ "\"" + lng.ToString().Replace(",", ".") + "\","
									+ "\"" + lat.ToString().Replace(",", ".") + "\"]},"
									+ "\"properties\": {"
									+ "\"Name\": \"" + FixName.Text + "\","
									+ "\"Type\": \"" + FlightRule + "\"}}";
			if(jsonCounter > 0)
			{
				jsonContent += ",";
			}
			jsonContent += CurrentFixJson;
			output.Text = jsonStart + jsonContent + jsonEnd;
			jsonCounter++;

			FixName.Clear();
			LatitudeInput.Clear();
			LongitudeInput.Clear();
			FixName.Focus();

		}

		private float LatToDecLat(string lat)
		{
			float d = float.Parse(lat.Substring(0, 2));
			float m = float.Parse(lat.Substring(2, 2));
			float s = float.Parse(lat.Substring(4, 2));

			float dec = d + (m / 60) + (s / 3600);
			return dec;
		}

		private float LngToDecLng(string lng)
		{
			float d = float.Parse(lng.Substring(0, 3));
			float m = float.Parse(lng.Substring(3, 2));
			float s = float.Parse(lng.Substring(5, 2));

			float dec = d + (m/60) + (s / 3600);
			return dec;
		}
	}
}
