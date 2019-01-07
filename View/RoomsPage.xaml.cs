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
	public sealed partial class RoomsPage : Page, IPageNavigateable
	{
		public RoomsPage()
		{
			this.InitializeComponent();
		}
		private async void NewRecord()
		{
			Dictionary<int, string> typesOfColumnDictionary = (Dictionary<int, string>)RosterViewModel.ColumnTypes;
			NewRecordDialog createDialog = new NewRecordDialog(RosterViewModel.ColumnNames, typesOfColumnDictionary,
				RosterViewModel.EnumTypes);
			ContentDialogResult dialogResult = await createDialog.ShowAsync();
			if (dialogResult == ContentDialogResult.Primary && createDialog.ValuesOfNewObject.Any())
			{
				List<string> valuesList = createDialog.ValuesOfNewObject;
				RosterViewModel.CreateRecord("Sale", valuesList);
			}
		}
		private async void EditRecord()
		{
			RoomViewModel room = databaseView.SelectedItem as RoomViewModel;
			string textToTitle = "Edytowany rekord: " + room.PrimaryKey;
			EditRecordDialog dialog = new EditRecordDialog(RosterViewModel.ColumnNames, RosterViewModel.ColumnTypes, textToTitle,
				RosterViewModel.EnumTypes);
			ContentDialogResult dialogResult = await dialog.ShowAsync();
			if (dialogResult == ContentDialogResult.Primary && !string.IsNullOrEmpty(dialog.Result))
			{
				string result = dialog.Result;
				string fieldToEdit = dialog.FieldToUpdate;
				RosterViewModel.UpdateRecord("Sale", room, fieldToEdit, result);
			}
		}
		public void Sort(string orderBy, string criterium)
		{
			RosterViewModel.Sort(typeof(RoomViewModel), "Sale", orderBy, criterium);
		}
		public void Search(string orderBy, string criterium, string searchIn, string searchValue)
		{
			RosterViewModel.Search(typeof(RoomViewModel), "Sale", orderBy, criterium, searchIn, searchValue);
		}

		#region Events

		private void SortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			string criterium;
			if ((bool)radioBtn1.IsChecked)
				criterium = "asc";
			else
				criterium = "desc";
			Sort(sortComboBox.SelectedItem.ToString(), criterium);
		}

		private void RadionBtn_Click(object sender, RoutedEventArgs e)
		{
			string criterium;
			if ((bool)radioBtn1.IsChecked)
				criterium = "asc";
			else
				criterium = "desc";
			Sort(sortComboBox.SelectedItem.ToString(), criterium);
		}

		private void SearchButton_Click(object sender, RoutedEventArgs e)
		{
			if (searchBox.Text == string.Empty)
				return;
			string criterium;
			if ((bool)radioBtn1.IsChecked)
				criterium = "asc";
			else
				criterium = "desc";
			string searchIn = lookInComboBox.SelectedItem.ToString();
			string searchedExpression = searchBox.Text;
			Search(sortComboBox.SelectedItem.ToString(), criterium, searchIn, searchedExpression);
		}

		private async void ResetButton_Click(object sender, RoutedEventArgs e)
		{
			await RosterViewModel.Read(typeof(RoomViewModel), "Sale");
		}

		private void DeleteButton_Click(object sender, RoutedEventArgs e)
		{
			if (databaseView.SelectedItem != null)
			{
				var room = databaseView.SelectedItem as IPrimaryKeyGetable;
				RosterViewModel.DeleteRecord("Sale", room);
			}
		}

		private void EditButton_Click(object sender, RoutedEventArgs e)
		{
			if (databaseView.SelectedItem != null)
				EditRecord();
		}

		private void NewRecordButton_Click(object sender, RoutedEventArgs e)
		{
			NewRecord();
		}
		#endregion

		protected async override void OnNavigatedTo(NavigationEventArgs e)
		{
			//await RosterViewModel.Read(typeof(RoomViewModel), "Sale");
			await RosterViewModel.InitializeViewModels("Sale");
			databaseView.ItemsSource = RosterViewModel.ModelsCollection;
			lookInComboBox.ItemsSource = sortComboBox.ItemsSource = RosterViewModel.ColumnNames;
			lookInComboBox.SelectedIndex = sortComboBox.SelectedIndex = 0;
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			;
		}

		public void UnloadPage()
		{
			//throw new NotImplementedException();
		}
	}
}
