using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalServerManager.InterfacesAndEnums;
using HospitalServerManager.Model;
using HospitalServerManager.Model.Basic;
using HospitalServerManager.Model.Controllers;

namespace HospitalServerManager.ViewModel
{
    class RosterViewModel
    {
        private ModelRoster _Roster { get; set; }
        private Controllers.DatabaseReader DbReader { get; set; }
		private Type ActualViewModelType { get; set; }
        private List<ISqlTableModel> ModelsList => _Roster.ModelsEnumerable.ToList();
        public RangeObservableCollection<IPrimaryKeyGetable> ModelsCollection = new RangeObservableCollection<IPrimaryKeyGetable>();
		public IEnumerable<string> ColumnNames { get => _Roster.ColumnNames; }
		public IDictionary<int, string> ColumnTypes { get => _Roster.ColumnTypes; }
		public Dictionary<string, Type> EnumTypes { get => _Roster.EnumTypes; }
		private SmtpMailSender mailSender = new SmtpMailSender();
		private Func<Task> lastReadMethod ;

		public RosterViewModel()
        {
            DbReader = new Controllers.DatabaseReader();
            _Roster = new ModelRoster();
        }
		public void SetActualViewModel(Type viewModel)
		{
			ActualViewModelType = viewModel;
		}
		private void RefreshRecords()
		{
			ModelsCollection.Clear();
			List<IPrimaryKeyGetable> lista = new List<IPrimaryKeyGetable>();
			ModelsList.ToList().ForEach(model => lista.Add((IPrimaryKeyGetable)Activator.CreateInstance(ActualViewModelType, model)));
			ModelsCollection.AddRange(lista);
		}
        public async Task ReadAsync(Type viewModel, string tableName)
        {
			/*ModelsCollection.Clear();
			ActualViewModelType = viewModel;*/
			lastReadMethod = async () => await _Roster.ReadModels(tableName);
			await GetDataWithoutSaveAsync(viewModel, tableName);
			//RefreshRecords();
        }
		public async Task GetDataWithoutSaveAsync(Type viewModel, string tableName)
		{
			ModelsCollection.Clear();
			ActualViewModelType = viewModel;
			await _Roster.ReadModels(tableName);
			RefreshRecords();
		}
		public async Task InitializeViewModelsAsync(string tableName)
		{
			await _Roster.GetColumnNames(tableName);
			await _Roster.GetColumnTypes(tableName);
		}
		private async Task GetColumnNamesAsync(string tableName)
		{
			await _Roster.GetColumnNames(tableName);
		}
		public async Task CreateRecordAsync(string tableName, IEnumerable<string> valuesList)
		{
			await _Roster.CreateRecordAsync(tableName, valuesList);
			await lastReadMethod();
			RefreshRecords();
		}
		public async Task UpdateRecordAsync(string tableName, IPrimaryKeyGetable viewModel, string fieldToUpdate, string valueToUpdate)
		{
			await _Roster.UpdateRecordAsync(tableName, viewModel.GetPrimaryKey(), viewModel.GetPrimaryKeyName() , fieldToUpdate, valueToUpdate);
			await lastReadMethod();
			RefreshRecords();
		}
		public async Task DeleteRecordAsync(string tableName, IPrimaryKeyGetable viewModel)
		{
			var actualModelType = ModelsList[0].GetType();
			var sqlModelToDelete = ModelsList.Where(x => (x as SqlTable).PrimaryKey == viewModel.GetPrimaryKey()).First();
			await _Roster.DeleteRecordAsync(tableName, sqlModelToDelete as SqlTable);
			await lastReadMethod();
			RefreshRecords();
		}
		public async void Sort(Type viewModel, string tableName, string orderBy, string criterium)
		{
			ModelsCollection.Clear();
			ActualViewModelType = viewModel;
			await (lastReadMethod = async () => await _Roster.Sort(tableName, orderBy, criterium)).Invoke();
			RefreshRecords();
		}
		public async void Search(Type viewModel, string tableName, string orderBy, string criterium, string searchIn, string searchValue)
		{
			ModelsCollection.Clear();
			ActualViewModelType = viewModel;
			await _Roster.Search(tableName, orderBy, criterium, searchIn, searchValue);
			RefreshRecords();
		}
		public async void SendEmailAsync(IHasEmailAdress sendToModel)
		{
			await mailSender.SendEmailAsync(sendToModel.GetEmailAdress().Address, "WIADOMOSC TESTOWA", "TEMAT TEST");
		}

	}
}
