using HospitalServerManager.InterfacesAndEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalServerManager.ViewModel.DataProvider
{
	class TypeProvider : IProvideType
	{
		protected Dictionary<string, Type> NamesDictionary = new Dictionary<string, Type>();

		public TypeProvider() { }
		public TypeProvider(List<Type> typeToRegister)
		{
			typeToRegister.ForEach(x => RegisterType(x));
		}
		public TypeProvider(Type typeToRegister) : this(new List<Type> { typeToRegister }) { }

		public Type GetTypeFromString(string typeName)
		{
			if (!NamesDictionary.ContainsKey(typeName))
				throw new KeyNotFoundException("The key does not exist!");

			return NamesDictionary[typeName];
		}

		public virtual void RegisterType(Type typeToRegister)
		{
			string typeName = typeToRegister.Name;

			if (NamesDictionary.ContainsKey(typeName))
				throw new Exception("Type was registred before!");

			NamesDictionary.Add(typeName, typeToRegister);
		}
	}
}
