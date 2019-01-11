using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HospitalServerManager.InterfacesAndEnums;
using HospitalServerManager.Model.Basic;
using HospitalServerManager.Model.Controllers;
using Windows.UI.Popups;

namespace HospitalServerManager.Model
{
    class ModelRoster
    {
        private List<ISqlTableModel> _modelsList = new List<ISqlTableModel>();
		private WebService webService = new WebService(new Uri("http://localhost:8080/"));
		public string ActualTableName { get; private set; }
		public IEnumerable<string> ColumnNames { get; private set; }
		public IDictionary<int, string> ColumnTypes { get; private set; }
		public Dictionary<string, Type> EnumTypes { get; protected set; }
		public IEnumerable<ISqlTableModel> ModelsEnumerable { get => _modelsList; }
        
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
			ActualTableName = tableName;
			/*ColumnNames = await GetColumnNames();
			ColumnTypes = await GetColumnTypes();*/
			IEnumerable<ISqlTableModel> response = new List<ISqlTableModel>();
			switch (tableName)
			{
				case "Przyjecia":
					response = await webService.GetRecordAsync<Admission>(tableName);
					break;
				case "Pacjenci":
					response = await webService.GetRecordAsync<Patient>(tableName);
					break;
				case "Lekarze":
					response = await webService.GetRecordAsync<Doctor>(tableName);
					break;
				case "Diagnozy":
					response = await webService.GetRecordAsync<Diagnosis>(tableName);
					break;
				case "Operacje":
					response = await webService.GetRecordAsync<Surgery>(tableName);
					break;
				case "Sale":
					response = await webService.GetRecordAsync<Room>(tableName);
					break;
				default:
					response = null;
					break;
			}
			_modelsList.AddRange(response);
		}
		public async Task<IEnumerable<string>> GetColumnNames(string tableName)
		{
			ActualTableName = tableName;
			if (ActualTableName == string.Empty)
				throw new Exception();
			ColumnNames = await webService.GetColumnNamesAsync(ActualTableName);

			return ColumnNames;
		}
		public async Task<IDictionary<int, string>> GetColumnTypes(string tableName)
		{
			ActualTableName = tableName;
			if (ActualTableName == string.Empty)
				throw new Exception();
			ColumnTypes = await webService.GetColumnTypesAsync(ActualTableName);
			return ColumnTypes;
		}
		public async Task CreateRecordAsync(string tableName, IEnumerable<string> valueList)
		{
			if (!await webService.CreateNewRecordAsync(tableName, valueList))
				await new MessageDialog("Wystąpił błąd, sprawdź poprawność danych").ShowAsync();
		}
		public async Task UpdateRecordAsync(string tableName, string primaryKey, string primaryKeyName, string fieldToUpdate, string valueToUpdate)
		{
			if(!await webService.UpdateRecordAsync(tableName, primaryKey, primaryKeyName, fieldToUpdate, valueToUpdate))
				await new MessageDialog("Wystąpił błąd, sprawdź poprawność danych").ShowAsync();
		}
		public async Task DeleteRecordAsync(string tableName, SqlTable modelToDelete)
		{
			if(!(modelToDelete is Admission))
			{
				var centralTableRecords = await webService.GetRecordAsync<Admission>("Przyjecia");
				if (IsCentralTableContainsDeletedModelForeginKey(tableName, modelToDelete.PrimaryKey, centralTableRecords.ToList()))
				{
					IUICommand response = null;
					MessageDialog mDialog = new MessageDialog("W tabeli Przyjęcia znajduje się odwołanie do usuwanego rekordu." +
							"Jeśli usuniesz ten rekord, wpis z tabeli Przyjęcia zostanie również usunięty. Czy nadal chcesz usunąć rekord?");
					mDialog.Commands.Add(new UICommand("Tak, usuń"));
					mDialog.Commands.Add(new UICommand("Nie, pozostaw rekord"));
					response = await mDialog.ShowAsync();
					if (response == mDialog.Commands.First())
					{
						await DeleteRecord("Przyjecia", modelToDelete.PrimaryKey, GetForeignKeyNameFromAdmissionsTable(tableName));
						await DeleteRecord(tableName, modelToDelete.PrimaryKey, modelToDelete.PrimaryKeyName);
					}
				}
				else
					await DeleteRecord(tableName, modelToDelete.PrimaryKey, modelToDelete.PrimaryKeyName);
			}
			else
				await DeleteRecord(tableName, modelToDelete.PrimaryKey, modelToDelete.PrimaryKeyName);
		}
		private async Task DeleteRecord(string tableName, string primaryKey, string primaryKeyName)
		{
			await webService.DeleteRecordAsync(tableName, primaryKey, primaryKeyName);
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
			newDictionary.Add("Dziedzina_chirurgii", typeof(SurgeryField));
			return newDictionary;
		}
		
		public void AddRange(IEnumerable<ISqlTableModel> modelsList)
        {
            _modelsList.Clear();
            _modelsList.AddRange(modelsList);
        }
		private string GetForeignKeyNameFromAdmissionsTable(string typeOfTable)
		{
			string stringToReturn = null;
			switch (typeOfTable)
			{
				case "Przyjecia":
					stringToReturn = null;
					break;
				case "Pacjenci":
					stringToReturn = "PESEL_pacjenta";
					break;
				case "Lekarze":
					stringToReturn = "Lekarz_prowadzacy";
					break;
				case "Diagnozy":
					stringToReturn = "Symbol_diagnozy";
					break;
				case "Operacje":
					stringToReturn = "Planowana_operacja";

					break;
				case "Sale":
					stringToReturn = "Nr_sali";
					break;
			}

			return stringToReturn;
		}
		private bool IsCentralTableContainsDeletedModelForeginKey(string tableName, string searchedModelPrimaryKey, List<Admission> centralTableList)
		{
			bool contains;
			switch (tableName)
			{
				case "Przyjecia":
					contains = false;
					break;
				case "Pacjenci":
					contains = centralTableList.FindIndex(x => x.PatientPESEL == searchedModelPrimaryKey) >= 0 ? true : false;
					break;
				case "Lekarze":
					contains = centralTableList.FindIndex(x => x.MainDoctor.ToString() == searchedModelPrimaryKey) >= 0 ? true : false;
					break;
				case "Diagnozy":
					contains = centralTableList.FindIndex(x => x.DiagnosisSymbol == searchedModelPrimaryKey) >= 0 ? true : false;
					break;
				case "Operacje":
					contains = centralTableList.FindIndex(x => x.OperationID.ToString() == searchedModelPrimaryKey) >= 0 ? true : false;
					break;
				case "Sale":
					contains = centralTableList.FindIndex(x => x.RoomNumber.ToString() == searchedModelPrimaryKey) >= 0 ? true : false;
					break;
				default:
					contains = false;
					break;
			}

			return contains;
		}
		public async Task Sort(string tableName, string orderBy, string criterium)
		{
			_modelsList.Clear();
			IEnumerable<ISqlTableModel> response = new List<ISqlTableModel>();
			switch (tableName)
			{
				case "Przyjecia":
					response = await webService.GetSortedRecordsAsync<Admission>(tableName, orderBy, criterium);
					break;
				case "Pacjenci":
					response = await webService.GetSortedRecordsAsync<Patient>(tableName, orderBy, criterium);
					break;
				case "Lekarze":
					response = await webService.GetSortedRecordsAsync<Doctor>(tableName, orderBy, criterium);
					break;
				case "Diagnozy":
					response = await webService.GetSortedRecordsAsync<Diagnosis>(tableName, orderBy, criterium);
					break;
				case "Operacje":
					response = await webService.GetSortedRecordsAsync<Surgery>(tableName, orderBy, criterium);
					break;
				case "Sale":
					response = await webService.GetSortedRecordsAsync<Room>(tableName, orderBy, criterium);
					break;
				default:
					response = null;
					break;
			}
			_modelsList.AddRange(response);
		}

		public async Task Search(string tableName, string orderBy, string criterium, string searchIn, string searchValue)
		{
			_modelsList.Clear();
			IEnumerable<ISqlTableModel> response = new List<ISqlTableModel>();
			switch (tableName)
			{
				case "Przyjecia":
					response = await webService.SearchRecordsAsync<Admission>(tableName, orderBy, criterium, searchIn, searchValue);
					break;
				case "Pacjenci":
					response = await webService.SearchRecordsAsync<Patient>(tableName, orderBy, criterium, searchIn, searchValue);
					break;
				case "Lekarze":
					response = await webService.SearchRecordsAsync<Doctor>(tableName, orderBy, criterium, searchIn, searchValue);
					break;
				case "Diagnozy":
					response = await webService.SearchRecordsAsync<Diagnosis>(tableName, orderBy, criterium, searchIn, searchValue);
					break;
				case "Operacje":
					response = await webService.SearchRecordsAsync<Surgery>(tableName, orderBy, criterium, searchIn, searchValue);
					break;
				case "Sale":
					response = await webService.SearchRecordsAsync<Room>(tableName, orderBy, criterium, searchIn, searchValue);
					break;
				default:
					response = null;
					break;
			}
			if(response != null && response.Any())
				_modelsList.AddRange(response);
		}
	}
}
