using HospitalServerManager.InterfacesAndEnums;
using HospitalServerManager.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
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
	public sealed partial class NewAdmissionPage : Page, IPageNavigateable
	{
		private List<FrameworkElement> guiElements = new List<FrameworkElement>();
		public NewAdmissionPage()
		{
			this.InitializeComponent();
		}

		public void UnloadPage()
		{
			;
		}

		protected async override void OnNavigatedTo(NavigationEventArgs e)
		{
			await RosterViewModel.InitializeViewModelsAsync("Przyjecia");
			GenerateGUI();
			InitializeValues();
		}
		private async void InitializeValues()
		{
			await RosterViewModel.ReadAsync(typeof(AdmissionViewModel), "Przyjecia");
			lastAdmission.Text = "Ostatnie przyjęcie - " + RosterViewModel.ModelsCollection.Last().ToString();
			await RosterViewModel.GetDataWithoutSaveAsync(typeof(PatientViewModel), "Pacjenci");
			patientsId.ItemsSource = new List<IPrimaryKeyGetable>(RosterViewModel.ModelsCollection);
			patientsId.SelectedIndex = 0;
			await RosterViewModel.GetDataWithoutSaveAsync(typeof(DiagnosisViewModel), "Diagnozy");
			diagnosisSymbol.ItemsSource = new List<IPrimaryKeyGetable>(RosterViewModel.ModelsCollection);
			diagnosisSymbol.SelectedIndex = 0;
			await RosterViewModel.GetDataWithoutSaveAsync(typeof(DoctorViewModel), "Lekarze");
			mainDoctorId.ItemsSource = new List<IPrimaryKeyGetable>(RosterViewModel.ModelsCollection);
			mainDoctorId.SelectedIndex = 0;
			await RosterViewModel.GetDataWithoutSaveAsync(typeof(SurgeryViewModel), "Operacje");
			operationId.ItemsSource = new List<IPrimaryKeyGetable>(RosterViewModel.ModelsCollection);
			operationId.SelectedIndex = 0;
			await RosterViewModel.GetDataWithoutSaveAsync(typeof(RoomViewModel), "Sale");
			roomNumber.ItemsSource = new List<IPrimaryKeyGetable>(RosterViewModel.ModelsCollection);
			roomNumber.SelectedIndex = 0;
		}
		private async void GenerateGUI()
		{
			var columnList = RosterViewModel.ColumnNames.ToList();
			for(int i = 0; i<RosterViewModel.ColumnNames.Count(); i++)
			{
				var tb = new TextBlock();
				tb.Text = columnList[i];
				tb.FontSize = 18;
				grid.Children.Add(tb);
				Grid.SetRow(tb, i + 1);
				tb.HorizontalAlignment = HorizontalAlignment.Center;
				tb.VerticalAlignment = VerticalAlignment.Center;
			}
			
		}

		private async void Button_Click(object sender, RoutedEventArgs e)
		{
			RosterViewModel.SetActualViewModel(typeof(AdmissionViewModel));
			if(tbID.Text != string.Empty)
			{
				var valuesList = new[] {
					tbID.Text, admissionDate.Date.ToString(), leavingDate.Date.ToString(),
					(patientsId.SelectedItem as IPrimaryKeyGetable).GetPrimaryKey(),
					(diagnosisSymbol.SelectedItem as IPrimaryKeyGetable).GetPrimaryKey(),
					(mainDoctorId.SelectedItem as IPrimaryKeyGetable).GetPrimaryKey(),
					(operationId.SelectedItem as IPrimaryKeyGetable).GetPrimaryKey(),
					(roomNumber.SelectedItem as IPrimaryKeyGetable).GetPrimaryKey(),
					(bool)trueCheckBox.IsChecked ? "true" : "false",
				};
				await RosterViewModel.CreateRecordAsync("Przyjecia", valuesList);
				await RosterViewModel.ReadAsync(typeof(AdmissionViewModel), "Przyjecia");
				lastAdmission.Text = "Ostatnie przyjęcie - " + RosterViewModel.ModelsCollection.Last().ToString();
			}
		}
	}
}
