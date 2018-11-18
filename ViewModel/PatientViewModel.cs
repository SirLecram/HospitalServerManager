using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalServerManager.ViewModel
{
    class PatientViewModel
    {
        public string PrimaryKey { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string PatientState { get; private set; }
        public string PatientSex { get; private set; }

        public PatientViewModel(Model.Basic.Patient patient)
        {

        }
    }
}
