using HospitalServerManager.InterfacesAndEnums;
using HospitalServerManager.ViewModel.Controllers;
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
    /// WebService powinien byc chyba wlasciwoscia w roznych kontrolerach np. GetController/PutController itp.; Obsługa błędów !! Np. Error z servera;
	/// Reload rekordów po edit i update; Testy w insert i update głupich wartości; Może ten email??;
    public sealed partial class MainFrameView : Page
    {
		private INavigator Navigator { get; set; }
		private IProvideType TypeProvider { get; set; }
		public MainFrameView()
        {
            this.InitializeComponent();
			InitializeProperties();
        }
        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
			string pageTypeName = (sender as AppBarButton).Tag.ToString();
			if(pageTypeName == "AdmissionsPage")
			{
				Navigator.SetParameter(new Action(() => Navigator.ChangeFrame(typeof(NewAdmissionPage), mainFrame)));
			}
			Type pageType = TypeProvider.GetTypeFromString(pageTypeName);

			IPageNavigateable page = Navigator.ChangeFrame(pageType, mainFrame);
			Navigator.RemoveParameters();
			navigationBar.IsOpen = navigationBar.IsSticky = true;
		}
		private void InitializeProperties()
		{
			IValidateIfInterfaceIsImplemented validator = new ViewModel.Validators.InterfaceImplementValidator();
			Navigator = new ViewNavigator(validator, new PatientsPage());
			// TODO: Dodać pozostałe Page
			TypeProvider = new ViewModel.DataProvider.NavigationPageTypeProvider(validator,
				new List<Type>
				{
					typeof(PatientsPage), typeof(DoctorsPage), typeof(AdmissionsPage), typeof(DiagnosesPage),
					typeof(RoomsPage), typeof(SurgeriesPage), typeof(NewAdmissionPage),
				});
			Navigator.SetParameter(new Action(() => Navigator.ChangeFrame(typeof(NewAdmissionPage), mainFrame)));
			Type pageType = TypeProvider.GetTypeFromString("AdmissionsPage");

			Navigator.ChangeFrame(pageType, mainFrame);
			Navigator.RemoveParameters();
		}
	}
}
