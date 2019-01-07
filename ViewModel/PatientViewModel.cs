using HospitalServerManager.InterfacesAndEnums;
using HospitalServerManager.Model.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalServerManager.ViewModel
{
	//Zmienic interfejs na taki dla viewmodeli
    class PatientViewModel : IPrimaryKeyGetable
    {
        private Patient model;
        public string PrimaryKey { get => model.PrimaryKey; }
        public string Name { get => model.Name; }
        public string Surname { get => model.Surname; }
        public DateTime BirthDate { get => model.BirthDate; }
        public string PatientState { get => model.PatientState.GetEnumDescription(); }
        public string PatientSex { get => model.PatientSex.GetEnumDescription(); }

        public PatientViewModel(Patient patient)
        {
            model = patient;
        }

        public string GetPrimaryKey()
        {
			return PrimaryKey;
        }

        public string GetPrimaryKeyName()
        {
			return model.PrimaryKeyName;
        }
    }
}
