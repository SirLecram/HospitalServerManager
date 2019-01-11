using HospitalServerManager.InterfacesAndEnums;
using HospitalServerManager.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

//Szablon elementu Pusta strona jest udokumentowany na stronie https://go.microsoft.com/fwlink/?LinkId=234238

namespace HospitalServerManager.View
{
	/// <summary>
	/// Pusta strona, która może być używana samodzielnie lub do której można nawigować wewnątrz ramki.
	/// </summary>
	public sealed partial class AdmissionsPage : Page, IPageNavigateable
	{
		public AdmissionsPage()
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
				await RosterViewModel.CreateRecordAsync("Przyjecia", valuesList);
			}
		}
		private async void EditRecord()
		{
			AdmissionViewModel admission = databaseView.SelectedItem as AdmissionViewModel;
			string textToTitle = "Edytowany rekord: " + admission.PrimaryKey;
			EditRecordDialog dialog = new EditRecordDialog(RosterViewModel.ColumnNames, RosterViewModel.ColumnTypes, textToTitle,
				RosterViewModel.EnumTypes);
			ContentDialogResult dialogResult = await dialog.ShowAsync();
			if (dialogResult == ContentDialogResult.Primary && !string.IsNullOrEmpty(dialog.Result))
			{
				string result = dialog.Result;
				string fieldToEdit = dialog.FieldToUpdate;
				await RosterViewModel.UpdateRecordAsync("Przyjecia", admission, fieldToEdit, result);
			}
		}
		public void Sort(string orderBy, string criterium)
		{
			RosterViewModel.Sort(typeof(AdmissionViewModel), "Przyjecia", orderBy, criterium);
		}
		public void Search(string orderBy, string criterium, string searchIn, string searchValue)
		{
			RosterViewModel.Search(typeof(AdmissionViewModel), "Przyjecia", orderBy, criterium, searchIn, searchValue);
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
			await RosterViewModel.ReadAsync(typeof(AdmissionViewModel), "Przyjecia");
		}

		private async void DeleteButton_Click(object sender, RoutedEventArgs e)
		{
			if (databaseView.SelectedItem != null)
			{
				var admission = databaseView.SelectedItem as IPrimaryKeyGetable;
				await RosterViewModel.DeleteRecordAsync("Przyjecia", admission);
			}
		}

		private void EditButton_Click(object sender, RoutedEventArgs e)
		{
			if (databaseView.SelectedItem != null)
				EditRecord();
		}

		#endregion

		protected async override void OnNavigatedTo(NavigationEventArgs e)
		{
			await RosterViewModel.InitializeViewModelsAsync("Przyjecia");
			databaseView.ItemsSource = RosterViewModel.ModelsCollection;
			lookInComboBox.ItemsSource = sortComboBox.ItemsSource = RosterViewModel.ColumnNames;
			lookInComboBox.SelectedIndex = sortComboBox.SelectedIndex = 0;
			btnNewRecord.Click += (sender, x) => (e.Parameter as Action).Invoke();
		}

		public void UnloadPage()
		{
			;
		}
	}
}
