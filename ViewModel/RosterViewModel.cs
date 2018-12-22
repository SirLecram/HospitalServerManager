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
            await DbReader.ReadDataFromDatabase(@"Data Source=MARCELPC;Initial Catalog = DB_s439397; Integrated Security = true;",
                 "SELECT * FROM Pacjenci", typeof(Model.Basic.Patient));
            // TO TYLKO DLA TESTOW, DO USUNIECIA I NIE KOPIOWAC MECHANIKI !! 
            List<ISqlTableModelable> lista = new List<ISqlTableModelable>();
            DbReader.LastReadedModels.ToList().ForEach(model => lista.Add(new PatientViewModel(model as HospitalServerManager.Model.Basic.Patient)));
            _Roster.AddRange(lista);
            ModelsCollection.AddRange(ModelsList);
        }
    }
}
