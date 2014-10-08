﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace FieldService.View
{
	public partial class CustomReportsPage : PhoneApplicationPage
	{
		public CustomReportsPage()
		{
            this.Language = XmlLanguage.GetLanguage(CultureInfo.CurrentUICulture.Name);
			InitializeComponent();
		}

		private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e) 
		{ 
			//throw new NotImplementedException(); 
			dpToDate.Value = DateTime.Today;

			var button = ApplicationBar.Buttons[0] as ApplicationBarIconButton;

			if (button != null) {
				button.Text = StringResources.CustomReportsPage_RunReport;
			}
		}
		
		private void abibRunReport_Click(object sender, EventArgs e) 
		{ 
			//throw new NotImplementedException(); 
			if (dpToDate.Value == null || dpStartDate.Value == null) {
				App.ToastMe(StringResources.CustomReportsPage_ErrorText);
				return;
			}
			DateTime from = dpStartDate.Value.Value;
			DateTime to = dpToDate.Value.Value;

			NavigationService.Navigate(new Uri(string.Format("/View/TimeReport.xaml?from={0}&to={1}", from.ToString("MM-dd-yyyy"), to.ToString("MM-dd-yyyy")), UriKind.Relative));
		}

		private void ButtonSetRangeWeek_Click(object sender, RoutedEventArgs e)
		{
			if (dpStartDate.Value != null) dpToDate.Value = dpStartDate.Value.Value.AddDays(7);
		}

		private void ButtonSetRangeMonth_Click(object sender, RoutedEventArgs e)
		{
			if (dpStartDate.Value != null) dpToDate.Value = dpStartDate.Value.Value.AddMonths(1).AddDays(-1);
		}
		private void ButtonSetRangeYear_Click(object sender, RoutedEventArgs e)
		{
			if (dpStartDate.Value != null) dpToDate.Value = dpStartDate.Value.Value.AddYears(1).AddDays(-1);
		}

	    private void ButtonSetRangeLastMonth_Click(object sender, RoutedEventArgs e)
	    {
	        dpStartDate.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1,0,0,0).AddMonths(-1);
	        dpToDate.Value = dpStartDate.Value.Value.AddMonths(1).AddDays(-1);
	    }

	    private void ButtonSetRangeLastServiceYear_Click(object sender, RoutedEventArgs e)
	    {
	        if (DateTime.Today.Month <= 8) {
	            dpStartDate.Value = new DateTime(DateTime.Today.Year - 2, 9, 1, 0, 0, 0);
	        }
	        else {
	            dpStartDate.Value = new DateTime(DateTime.Today.Year - 1, 9, 1, 0, 0, 0);
	        }
            dpToDate.Value = dpStartDate.Value.Value.AddYears(1).AddDays(-1);
	    }
	}
}