using HospitalServerManager.InterfacesAndEnums;
using HospitalServerManager.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//Szablon elementu Pusta strona jest udokumentowany na stronie https://go.microsoft.com/fwlink/?LinkId=234238

namespace HospitalServerManager.View
{
	/// <summary>
	/// Pusta strona, która może być używana samodzielnie lub do której można nawigować wewnątrz ramki.
	/// </summary>
	public sealed partial class DoctorsPage : Page, IPageNavigateable
	{
		public DoctorsPage()
		{
			this.InitializeComponent();
		}

		#region Events

		private void SortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}

		private void RadionBtn_Click(object sender, RoutedEventArgs e)
		{

		}

		private void SearchButton_Click(object sender, RoutedEventArgs e)
		{

		}

		private void ResetButton_Click(object sender, RoutedEventArgs e)
		{

		}

		private void DeleteButton_Click(object sender, RoutedEventArgs e)
		{

		}

		private void EditButton_Click(object sender, RoutedEventArgs e)
		{

		}

		private void NewRecordButton_Click(object sender, RoutedEventArgs e)
		{

		}
		#endregion

		protected override async void OnNavigatedTo(NavigationEventArgs e)
		{
			await RosterViewModel.Read(typeof(DoctorViewModel), "Lekarze");
		}

		private async void Page_Loaded(object sender, RoutedEventArgs e)
		{
			databaseView.ItemsSource = RosterViewModel.ModelsCollection;
			sortComboBox.ItemsSource = RosterViewModel.ColumnNames;
		}

		public void UnloadPage()
		{
			//throw new NotImplementedException();
		}
	}
}
