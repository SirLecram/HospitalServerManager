using HospitalServerManager.InterfacesAndEnums;
using HospitalServerManager.Model.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace HospitalServerManager.ViewModel
{
	//Zmienic interfejs na taki dla viewmodeli
    class PatientViewModel : IPrimaryKeyGetable, IHasEmailAdress
    {
        private Patient model;
        public string PrimaryKey { get => model.PrimaryKey; }
        public string Name { get => model.Name; }
        public string Surname { get => model.Surname; }
        public string BirthDate { get => model.BirthDate.ToShortDateString(); }
        public string PatientState { get => model.PatientState.GetEnumDescription(); }
        public string PatientSex { get => model.PatientSex.GetEnumDescription(); }
		public string EmailAdress { get => model.EmailAdress.Address; }

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
		public override string ToString()
		{
			return PrimaryKey + " " + Name + " " + Surname;
		}

		public bool IsEmailAdressInitialized()
		{
			var resp = model.EmailAdress.Address != string.Empty ? true : false;
			return true; // TODO: Po testach zmienc
		}

		public MailAddress GetEmailAdress()
		{
			return new MailAddress("paker_7@o2.pl");
		}
	}
}
