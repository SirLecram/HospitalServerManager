using HospitalServerManager.InterfacesAndEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace HospitalServerManager.ViewModel.Controllers
{
	class ViewNavigator : INavigator
	{
		public IPageNavigateable ActualPage { get; protected set; }
		public bool IsParameterSet { get => Parameters == null ? false : true; }
		private object Parameters { get; set; }
		private IValidateIfInterfaceIsImplemented Validator = null;
		// TODO: Przemyslec czy potrzebne actualPage
		protected ViewNavigator()
		{ Parameters = null; ActualPage = null; }
		public ViewNavigator(IValidateIfInterfaceIsImplemented validator, IPageNavigateable actualPage, object parametersToPage = null)
		{
			ActualPage = actualPage;
			Validator = validator;
			Parameters = parametersToPage;
			/*Type type = actualPage.GetType();
            ChangeFrame(type, )*/
		}

		/// <summary>
		/// Sets parameters which is passed to the target page.
		/// </summary>
		/// <param name="parameter"></param>
		public void SetParameter(object parameter)
		{
			Parameters = parameter;
		}
		/// <summary>
		/// Removes parameters.
		/// </summary>
		public void RemoveParameters()
		{
			Parameters = null;
		}
		/// <summary>
		/// Navigates inside navigationFrame to new page and passes the parameter to new page.
		/// </summary>
		/// <param name="typeOfPage">The type of the target page</param>
		/// <param name="navigationFrame">The navigation frame</param>
		/// <returns></returns>
		public IPageNavigateable ChangeFrame(Type typeOfPage, Frame navigationFrame)
		{
			if (!Validator.ValidateIfTypeImplementInterface(typeOfPage, "IPageNavigateable"))
				throw new ArgumentException("Wrong page type! Page have to implement IPageNavigateable.");

			ActualPage.UnloadPage();
			if (IsParameterSet)
				navigationFrame.Navigate(typeOfPage, Parameters);
			else
				navigationFrame.Navigate(typeOfPage);
			ActualPage = navigationFrame.Content as IPageNavigateable;

			return ActualPage;
		}
	}
}
