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
		public static string CreateNewRecord(string tableName, List<string> valuesList)
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
		public static string UpdateRecordHttpRequest(string tableName, string primaryKey, string primaryKeyName, string fieldToUpdate, string valueToUpdate)
		{
			string updateRequest = "/updaterec/" + tableName + "/pk/" + primaryKey + "/pkn/";
			updateRequest += primaryKeyName + "/ftu/" + fieldToUpdate + "/vti/" + valueToUpdate;
			return updateRequest;
		}
		public static string DeleteRecordHttpRequest(string tableName, string primaryKey, string primaryKeyName)
		{
			string updateRequest = "/deleterec/" + tableName + "/pk/" + primaryKey + "/pkn/" + primaryKeyName;
			return updateRequest;
		}
		public static string SortedRecordsHttpRequest(string tableName, string orderBy, string criterium)
		{
			string sortReq = "/sort/" + tableName + "/sortby/" + orderBy + "/sorttype/" + criterium;
			return sortReq;
		}
		public static string SearchRecordHttpRequest(string tableName, string orderBy, string criterium, string searchIn, string searchValue)
		{
			string search = "/search/" + tableName + "/where/" + searchIn + "/is/" + searchValue + "/sortby/" + orderBy + "/sorttype/" + criterium;
			return search;
		}
	}
}
