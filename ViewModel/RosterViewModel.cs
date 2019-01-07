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
        private List<ISqlTableModel> ModelsList => _Roster.ModelsEnumerable.ToList();
        public RangeObservableCollection<IPrimaryKeyGetable> ModelsCollection = new RangeObservableCollection<IPrimaryKeyGetable>();
		public IEnumerable<string> ColumnNames { get => _Roster.ColumnNames; }
		public IDictionary<int, string> ColumnTypes { get => _Roster.ColumnTypes; }
		public Dictionary<string, Type> EnumTypes { get => _Roster.EnumTypes; }

		public RosterViewModel()
        {
            DbReader = new Controllers.DatabaseReader();
            _Roster = new ModelRoster();
			//Read(typeof(PatientViewModel));
        }
        public async Task Read(Type viewModel, string tableName)
        {
			ModelsCollection.Clear();
            List<IPrimaryKeyGetable> lista = new List<IPrimaryKeyGetable>();
			await _Roster.ReadModels(tableName);
			ModelsList.ToList().ForEach(model => lista.Add((IPrimaryKeyGetable)Activator.CreateInstance(viewModel, model)));
			ModelsCollection.AddRange(lista);
			return;
        }
		public async Task InitializeViewModels(string tableName)
		{
			await _Roster.GetColumnNames(tableName);
			await _Roster.GetColumnTypes(tableName);
		}
		private async Task GetColumnNames(string tableName)
		{
			await _Roster.GetColumnNames(tableName);
		}
		public void CreateRecord(string tableName, IEnumerable<string> valuesList)
		{
			_Roster.CreateRecord(tableName, valuesList);
		}
		public void UpdateRecord(string tableName, IPrimaryKeyGetable viewModel, string fieldToUpdate, string valueToUpdate)
		{
			_Roster.UpdateRecord(tableName, viewModel.GetPrimaryKey(), viewModel.GetPrimaryKeyName() , fieldToUpdate, valueToUpdate);
		}
		public void DeleteRecord(string tableName, IPrimaryKeyGetable viewModel)
		{
			var actualModelType = ModelsList[0].GetType();
			var sqlModelToDelete = ModelsList.Where(x => (x as SqlTable).PrimaryKey == viewModel.GetPrimaryKey()).First();
			_Roster.DeleteRecord(tableName, sqlModelToDelete as SqlTable);
		}
		public async void Sort(Type viewModel, string tableName, string orderBy, string criterium)
		{
			ModelsCollection.Clear();
			List<IPrimaryKeyGetable> lista = new List<IPrimaryKeyGetable>();
			await _Roster.Sort(tableName, orderBy, criterium);
			ModelsList.ToList().ForEach(model => lista.Add((IPrimaryKeyGetable)Activator.CreateInstance(viewModel, model)));
			ModelsCollection.AddRange(lista);
			return;
		}
		public async void Search(Type viewModel, string tableName, string orderBy, string criterium, string searchIn, string searchValue)
		{
			ModelsCollection.Clear();
			List<IPrimaryKeyGetable> lista = new List<IPrimaryKeyGetable>();
			await _Roster.Search(tableName, orderBy, criterium, searchIn, searchValue);
			ModelsList.ToList().ForEach(model => lista.Add((IPrimaryKeyGetable)Activator.CreateInstance(viewModel, model)));
			ModelsCollection.AddRange(lista);
			return;
		}

	}
}
