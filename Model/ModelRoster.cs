using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalServerManager.InterfacesAndEnums;
using HospitalServerManager.Model.Basic;

namespace HospitalServerManager.Model
{
    class ModelRoster
    {
        private List<ISqlTableModelable> _modelsList = new List<ISqlTableModelable>();
        public IEnumerable<ISqlTableModelable> ModelsEnumerable { get => _modelsList; }
        //private Controllers.DatabaseReader DatabaseReader = new Controllers.DatabaseReader();
        public ModelRoster()
        {
      /*      Read();
            _modelsList.Clear();
            _modelsList.AddRange(DatabaseReader.LastReadedModels);*/
        }

        public async void Read()
        {
          /*  await DatabaseReader.ReadDataFromDatabase(@"Data Source=MARCEL\SQLEXPRESS;Initial Catalog = DB_s439397; Integrated Security = true;",
               "SELECT * FROM Pacjenci", typeof(Patient));*/
            
        }
        public void AddRange(IEnumerable<ISqlTableModelable> modelsList)
        {
            _modelsList.Clear();
            _modelsList.AddRange(modelsList);
        }
    }
}
