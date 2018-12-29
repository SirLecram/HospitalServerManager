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
using HospitalServerManager.InterfacesAndEnums;
using System.Collections.ObjectModel;
using HospitalServerManager.ViewModel;

//Szablon elementu Pusta strona jest udokumentowany na stronie https://go.microsoft.com/fwlink/?LinkId=234238

namespace HospitalServerManager.View
{
    /// <summary>
    /// Pusta strona, która może być używana samodzielnie lub do której można nawigować wewnątrz ramki.
    /// </summary>
    public sealed partial class PatientsPage : Page , IPageNavigateable
    {
        //public ObservableCollection<ISqlTableModelable> ModelCollection { get => RosterViewModel.ModelsCollection; }
        private ViewModel.Controllers.DatabaseReader databaseReader;
        public PatientsPage()
        {
            this.InitializeComponent();

        }

		private async void NewRecord()
		{
			Dictionary<int, string> typesOfColumnDictionary = (Dictionary<int,string>) RosterViewModel.ColumnTypes;
			NewRecordDialog createDialog = new NewRecordDialog(RosterViewModel.ColumnNames, typesOfColumnDictionary,
				RosterViewModel.EnumTypes);
			ContentDialogResult dialogResult = await createDialog.ShowAsync();
			if (dialogResult == ContentDialogResult.Primary && createDialog.ValuesOfNewObject.Any())
			{
				List<string> valuesList = createDialog.ValuesOfNewObject;
				RosterViewModel.CreateRecord("Pacjenci", valuesList);
			}
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
			NewRecord();
        }
        #endregion

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
			await RosterViewModel.Read(typeof(PatientViewModel), "Pacjenci");
            //databaseReader = e.Parameter as HospitalServerManager.ViewModel.Controllers.DatabaseReader;
            //_IsDataLoaded = false;
           // DatabaseController.OnPropertyChanged("IsDataLoaded");
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            databaseView.ItemsSource = RosterViewModel.ModelsCollection;
			lookInComboBox.ItemsSource = RosterViewModel.ColumnNames;
			var sortList = RosterViewModel.ColumnNames.ToList();
			sortList.RemoveAt(0);
			sortComboBox.ItemsSource = sortList;
        }

		public void UnloadPage()
		{
			;
		}
	}
}
