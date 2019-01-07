using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace HospitalServerManager.InterfacesAndEnums
{
    public interface ISqlTableModel
    {

        List<string> GetColumnNames();
    }
    public interface IPrimaryKeyGetable
    {
        string GetPrimaryKey();
        string GetPrimaryKeyName();
    }
	public interface INavigator
	{
		bool IsParameterSet { get; }
		void SetParameter(object parameter);
		void RemoveParameters();
		IPageNavigateable ChangeFrame(Type typeOfPage, Frame navigationFrame);
	}
	public interface IPageNavigateable
	{
		void UnloadPage();
	}
	public interface IValidateIfInterfaceIsImplemented
	{
		/// <summary>
		/// Validates if type implement interface. TypeProvider have to be initialized before use this overloaded method.
		/// </summary>
		/// <param name="typeName"></param>
		/// <param name="interfaceNameToCheck"></param>
		/// <returns></returns>
		bool ValidateIfTypeImplementInterface(string typeName, string interfaceNameToCheck);
		bool ValidateIfTypeImplementInterface(Type typeToCheck, string interfaceNameToCheck);
		bool ValidateIfTypeImplementInterface(Type typeToCheck, Type interfaceToCheck);
	}
	public interface IProvideType
	{
		void RegisterType(Type typeToRegister);
		Type GetTypeFromString(string typeName);
	}
	public interface IProvideDBInfo
	{
		IEnumerable<string> GetColumnNames(string tableName);
		IDictionary<int, string> GetColumnTypesNames(string tableName);
	}
}
