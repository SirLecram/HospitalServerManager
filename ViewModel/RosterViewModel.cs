using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalServerManager.InterfacesAndEnums;
using HospitalServerManager.Model;

namespace HospitalServerManager.ViewModel
{
    class RosterViewModel
    {
        private ModelRoster _Roster { get; set; }
        private Controllers.DatabaseReader DbReader { get; set; }
        private List<ISqlTableModelable> ModelsList => _Roster.ModelsEnumerable.ToList();
        public RangeObservableCollection<ISqlTableModelable> ModelsCollection = new RangeObservableCollection<ISqlTableModelable>();

        public RosterViewModel()
        {
            DbReader = new Controllers.DatabaseReader();
            _Roster = new ModelRoster();
            Read();
            
        }
        public async void Read()
        {
            await DbReader.ReadDataFromDatabase(@"Data Source=MARCEL\SQLEXPRESS;Initial Catalog = DB_s439397; Integrated Security = true;",
                 "SELECT * FROM Pacjenci", typeof(Model.Basic.Patient));
            _Roster.AddRange(DbReader.LastReadedModels);
            ModelsCollection.AddRange(ModelsList);
        }
    }
}
