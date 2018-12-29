using HospitalServerManager.InterfacesAndEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalServerManager.ViewModel.Validators
{
	class InterfaceImplementValidator : IValidateIfInterfaceIsImplemented
	{
		public IProvideType TypeProvider { protected get; set; }

		public InterfaceImplementValidator() { }
		public InterfaceImplementValidator(IProvideType typeProvider) { TypeProvider = typeProvider; }

		public bool ValidateIfTypeImplementInterface(Type typeToCheck, string interfaceNameToCheck)
		{
			Type returnedType = typeToCheck.GetInterface(interfaceNameToCheck);
			if (returnedType == null)
				return false;
			return true;
		}

		public bool ValidateIfTypeImplementInterface(string typeName, string interfaceNameToCheck)
		{
			Type typeFromString = TypeProvider.GetTypeFromString(typeName);
			return ValidateIfTypeImplementInterface(typeFromString, interfaceNameToCheck);
		}

		public bool ValidateIfTypeImplementInterface(Type typeToCheck, Type interfaceToCheck)
		{
			string interfaceName = interfaceToCheck.Name;
			return ValidateIfTypeImplementInterface(typeToCheck, interfaceName);
		}
	}
}
