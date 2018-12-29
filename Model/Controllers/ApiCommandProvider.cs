using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalServerManager.Model.Controllers
{
	internal static class ApiCommandProvider
	{
		public static string GetRecordHttpRequest(string tableName)
		{
			return @"/getfromdb/" + tableName;
		}
		public static string GetColumnNamesHttpRequest(string tableName)
		{
			return @"/getcolumnnames/" + tableName;
		}
		public static string GetColumnTypesHttpRequest(string tableName)
		{
			return @"/getcolumntypes/" + tableName;
		}
		public static string CreateNewRecordAsync(string tableName, List<string> valuesList)
		{
			string createRequest = "/insertrec/" + tableName + "/nr/" + valuesList.Count() + "/pk/" + valuesList[0];
			for(int i = 1; i<8; i++)
			{
				var maxIndex = valuesList.Count();
				if (i < maxIndex)
					createRequest += "/" + valuesList[i];
				else
					createRequest += "/_";
			}
			return createRequest;
		}
	}
}
