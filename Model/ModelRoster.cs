using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HospitalServerManager.InterfacesAndEnums;
using HospitalServerManager.Model.Basic;
using HospitalServerManager.Model.Controllers;

namespace HospitalServerManager.Model
{
    class ModelRoster
    {
        private List<ISqlTableModelable> _modelsList = new List<ISqlTableModelable>();
		private WebService webService = new WebService(new Uri("http://localhost:8080/"));
		public string ActualTableName { get; private set; }
		public IEnumerable<string> ColumnNames { get; private set; }
		public IDictionary<int, string> ColumnTypes { get; private set; }
		public Dictionary<string, Type> EnumTypes { get; protected set; }
		public IEnumerable<ISqlTableModelable> ModelsEnumerable { get => _modelsList; }
        //private Controllers.DatabaseReader DatabaseReader = new Controllers.DatabaseReader();
        public ModelRoster()
        {
			ActualTableName = string.Empty;
			ColumnNames = new List<string>();
			EnumTypes = CreateEnumTypesDictionary();
        }

		public async Task ReadModels(string tableName)
		{
			_modelsList.Clear();
			if(ColumnNames.Any())
				ColumnNames.ToList().Clear();
			/*MethodInfo method = typeof(WebService).GetMethod("GetRecordAsync");
			method = method.MakeGenericMethod(type);
			var resp = method.Invoke(webService, new[] { "Pacjenci" });*/
			ActualTableName = tableName;
			ColumnNames = await GetColumnNames();
			ColumnTypes = await GetColumnTypes();
			IEnumerable<ISqlTableModelable> response = new List<ISqlTableModelable>();
			if(tableName == "Pacjenci") response = await webService.GetRecordAsync<Patient>(tableName);
			else if(tableName == "Lekarze") response = await webService.GetRecordAsync<Doctor>(tableName);
			_modelsList.AddRange(response);
			
			//ColumnNames = await GetColumnNames();
		}
		public async Task<IEnumerable<string>> GetColumnNames()
		{
			if (ActualTableName == string.Empty)
				throw new Exception();
			return await webService.GetColumnNamesAsync(ActualTableName);
		}
		public async Task<IDictionary<int, string>> GetColumnTypes()
		{
			if (ActualTableName == string.Empty)
				throw new Exception();
			return await webService.GetColumnTypesAsync(ActualTableName);
		}
		public async void CreateRecord(string tableName, IEnumerable<string> valueList)
		{
			await webService.CreateNewRecordAsync(tableName, valueList);
		}
		private Dictionary<string, Type> CreateEnumTypesDictionary()
		{
			// TODO: Uzupełniać w miare dodawania tabel!
			Dictionary<string, Type> newDictionary = new Dictionary<string, Type>();
			newDictionary.Add("Plec", typeof(Sex));
			newDictionary.Add("Stan", typeof(PatientState));
			newDictionary.Add("Stopien_naukowy", typeof(AcademicDegrees));
			newDictionary.Add("Specjalizacja", typeof(MedicalSpecializations));
			newDictionary.Add("Stanowisko", typeof(JobPositions));
			return newDictionary;
		}
		public void AddRange(IEnumerable<ISqlTableModelable> modelsList)
        {
            _modelsList.Clear();
            _modelsList.AddRange(modelsList);
        }
    }
}
