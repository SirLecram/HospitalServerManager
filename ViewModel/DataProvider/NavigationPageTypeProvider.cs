using HospitalServerManager.InterfacesAndEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalServerManager.ViewModel.DataProvider
{
	class NavigationPageTypeProvider : TypeProvider
	{
		private IValidateIfInterfaceIsImplemented InterfaceValidator = null;

		public NavigationPageTypeProvider(IValidateIfInterfaceIsImplemented validateInterface) { InterfaceValidator = validateInterface; }
		public NavigationPageTypeProvider(IValidateIfInterfaceIsImplemented validateInterface, List<Type> typeToRegister)
			: this(validateInterface)
		{
			typeToRegister.ForEach(x => RegisterType(x));
		}
		public NavigationPageTypeProvider(IValidateIfInterfaceIsImplemented validateInterface, Type typeToRegister)
			: this(validateInterface, new List<Type> { typeToRegister }) { }


		public override void RegisterType(Type typeToRegister)
		{
			if (!InterfaceValidator.ValidateIfTypeImplementInterface(typeToRegister, typeof(IPageNavigateable)))
				throw new ArgumentException("Registred type have to implement IPageNavigateable interface!");

			base.RegisterType(typeToRegister);
		}
	}
}
