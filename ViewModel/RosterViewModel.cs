using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalServerManager.InterfacesAndEnums;
using HospitalServerManager.Model;
using HospitalServerManager.Model.Controllers;

namespace HospitalServerManager.ViewModel
{
    class RosterViewModel
    {
        private ModelRoster _Roster { get; set; }
		private IProvideDBInfo databaseInfoProvider;
        private Controllers.DatabaseReader DbReader { get; set; }
        private List<ISqlTableModelable> ModelsList => _Roster.ModelsEnumerable.ToList();
        public RangeObservableCollection<ISqlTableModelable> ModelsCollection = new RangeObservableCollection<ISqlTableModelable>();
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
            /*await DbReader.ReadDataFromDatabase(@"Data Source=MARCELPC;Initial Catalog = DB_s439397; Integrated Security = true;",
                 "SELECT * FROM Pacjenci", typeof(Model.Basic.Patient));*/
            List<ISqlTableModelable> lista = new List<ISqlTableModelable>();
			// DbReader.LastReadedModels.ToList().ForEach(model => lista.Add(new PatientViewModel(model as HospitalServerManager.Model.Basic.Patient)));
			await _Roster.ReadModels(tableName);
			ModelsList.ToList().ForEach(model => lista.Add((ISqlTableModelable)Activator.CreateInstance(viewModel, model)));
			//_Roster.AddRange(lista);
			ModelsCollection.AddRange(lista);
			return;
        }
		public void CreateRecord(string tableName, IEnumerable<string> valuesList)
		{
			_Roster.CreateRecord(tableName, valuesList);
		}
    }
}
