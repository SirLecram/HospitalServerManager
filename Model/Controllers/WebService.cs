using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using HospitalServerManager.InterfacesAndEnums;
using Newtonsoft.Json;
using System.IO;
using HospitalServerManager.Model.Basic;

namespace HospitalServerManager.Model.Controllers
{
	class WebService
	{
		private HttpClient httpClient;
		private IProvideDBInfo databaseInfoProvider { get; }

		public WebService(HttpClient httpClient)
		{
			this.httpClient = httpClient;
			InitializeHttpClient(new Uri("http://localhost:1433/"));
		}
		public WebService(Uri baseAdress)
		{
			httpClient = new HttpClient(new HttpClientHandler(), false);
			InitializeHttpClient(baseAdress);
		}

		private void InitializeHttpClient(Uri baseAdress)
		{
			httpClient.BaseAddress = baseAdress;
			httpClient.DefaultRequestHeaders.Clear();
		}

		public async Task<IEnumerable<T>> GetRecordAsync<T>(string tableName)
		{
			// TODO: SPRAWDZIC CZY IMPLEMENTUJE INTERFACE!!
			List<ISqlTableModel> modelsList = new List<ISqlTableModel>();
			using (var message = new HttpRequestMessage(HttpMethod.Get, ApiCommandProvider.GetRecordHttpRequest(tableName)))
			using (var response = await httpClient.SendAsync(message))
			{
				response.EnsureSuccessStatusCode();
				var stream = await response.Content.ReadAsStreamAsync();
				return DeserializeJsonFromStream<IEnumerable<T>>(stream);
			}
		}
		public async Task<IEnumerable<T>> GetSortedRecordsAsync<T>(string tableName, string orderBy, string criterium)
		{
			List<ISqlTableModel> modelsList = new List<ISqlTableModel>();
			using (var message = new HttpRequestMessage(HttpMethod.Get, ApiCommandProvider.SortedRecordsHttpRequest(tableName, orderBy, criterium)))
			using (var response = await httpClient.SendAsync(message))
			{
				response.EnsureSuccessStatusCode();
				var stream = await response.Content.ReadAsStreamAsync();
				return DeserializeJsonFromStream<IEnumerable<T>>(stream);
			}
		}
		public async Task<IEnumerable<T>> SearchRecordsAsync<T>(string tableName, string orderBy, string criterium, string searchIn, string searchValue)
		{
			List<ISqlTableModel> modelsList = new List<ISqlTableModel>();
			using (var message = new HttpRequestMessage(HttpMethod.Get,
				ApiCommandProvider.SearchRecordHttpRequest(tableName, orderBy, criterium, searchIn, searchValue)))
			using (var response = await httpClient.SendAsync(message))
			{
				response.EnsureSuccessStatusCode();
				var stream = await response.Content.ReadAsStreamAsync();
				return DeserializeJsonFromStream<IEnumerable<T>>(stream);
			}
		}
		public async Task<IEnumerable<string>> GetColumnNamesAsync(string tableName)
		{
			using (var message = new HttpRequestMessage(HttpMethod.Get, ApiCommandProvider.GetColumnNamesHttpRequest(tableName)))
			using (var response = await httpClient.SendAsync(message))
			{
				response.EnsureSuccessStatusCode();
				var columns = await response.Content.ReadAsStringAsync();
				columns = columns.Remove(0, 1); 
				var columnNames = columns.Split(".");
				var x = columnNames.ToList();
				x.RemoveAt(x.Count - 1);
				return x;
			}
		}
		public async Task<IDictionary<int,string>> GetColumnTypesAsync(string tableName)
		{
			using (var message = new HttpRequestMessage(HttpMethod.Get, ApiCommandProvider.GetColumnTypesHttpRequest(tableName)))
			using (var response = await httpClient.SendAsync(message))
			{
				response.EnsureSuccessStatusCode();
				var columns = await response.Content.ReadAsStringAsync();
				var columnTypes = columns.Split(".");
				var x = columnTypes.ToList();
				x.RemoveAt(x.Count - 1);
				var dictionary = new Dictionary<int, string>();
				for(int i = 0; i<x.Count; i++)
				{
					dictionary.Add(i, x[i]);
				}
				return dictionary;
			}
		}
		public async Task<bool> CreateNewRecordAsync(string tableName, IEnumerable<string> valuesList)
		{
			using (var message = new HttpRequestMessage(HttpMethod.Get, ApiCommandProvider.CreateNewRecord(tableName, valuesList.ToList())))
			using (var response = await httpClient.SendAsync(message))
			{
				response.EnsureSuccessStatusCode();
				return true;
			}
		}
		public async Task<bool> UpdateRecordAsync(string tableName, string primaryKey, string primaryKeyName, string fieldToEdit, string valueToUpdate)
		{
			using (var message = new HttpRequestMessage(HttpMethod.Get, 
				ApiCommandProvider.UpdateRecordHttpRequest(tableName, primaryKey, primaryKeyName, fieldToEdit, valueToUpdate)))
			using (var response = await httpClient.SendAsync(message))
			{
				response.EnsureSuccessStatusCode();
				return true;
			}
		}
		public async Task<bool> DeleteRecordAsync(string tableName, string primaryKey, string primaryKeyName)
		{
			using (var message = new HttpRequestMessage(HttpMethod.Get,
				ApiCommandProvider.DeleteRecordHttpRequest(tableName, primaryKey, primaryKeyName)))
			using (var response = await httpClient.SendAsync(message))
			{
				response.EnsureSuccessStatusCode();
				return true;
			}
		}

		private static T DeserializeJsonFromStream<T>(Stream stream)
		{
			if (stream == null || stream.CanRead == false)
				return default(T);

			using (var sr = new StreamReader(stream))
			using (var textReader = new JsonTextReader(sr))
			{
				var js = new JsonSerializer();
				var searchResult = js.Deserialize<T>(textReader);
				return searchResult;
			}
		}
	}
}
