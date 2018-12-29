using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalServerManager.InterfacesAndEnums;
using Newtonsoft.Json;

namespace HospitalServerManager.Model.Basic
{
    internal class Patient : SqlTable
    {
        public string Surname { get; protected set; }
        public string Name { get; protected set; }
        public DateTime BirthDate { get; protected set; }
        public PatientState PatientState { get; protected set; }
        public Sex PatientSex { get; protected set; }

        protected Patient() : base()
        {
        }
		[JsonConstructor]
        protected Patient(string pesel, string name, string surname, DateTime birthDate, string patientState,
            string patientSex) : base(pesel, "PESEL", new List<string>())
        {
            if (pesel.Length < 11 || pesel.Length > 11)
                throw new FormatException("PESEL musi mieć 11 cyfr");
            //PrimaryKey = pesel;
            Surname = surname;
            Name = name;
            BirthDate = birthDate;
            PatientState = patientState.GetEnumFromDescription<PatientState>();
            PatientSex = patientSex.GetEnumFromDescription<Sex>();
        }
		public Patient(string pesel, string name, string surname, DateTime birthDate, PatientState patientState,
		   Sex patientSex) : base(pesel, "PESEL", new List<string>())
		{
			if (pesel.Length < 11 || pesel.Length > 11)
				throw new FormatException("PESEL musi mieć 11 cyfr");
			//PrimaryKey = pesel;
			Surname = surname;
			Name = name;
			BirthDate = birthDate;
			PatientState = patientState;
			PatientSex = patientSex;
		}/// <summary>
		 /// List have to be in right order (pesel, surname, name, birth date, patient state, patient sex).
		 /// </summary>
		 /// <param name="listOfValues"></param>
		public Patient(List<string> listOfValues) : base(listOfValues[0], "PESEL", new List<string>())
        {
            // TODO: Dodać zabezpieczenia dla pozostałych wartosci
            // TODO: VALIDATOR! Lista kolumn nazw;
            if (listOfValues[0].Length < 11 || listOfValues[0].Length > 11)
                throw new FormatException("PESEL musi mieć 11 cyfr");
            PrimaryKey = listOfValues[0];
            Surname = listOfValues[1];
            Name = listOfValues[2];
            BirthDate = DateTime.Parse(listOfValues[3]);
            PatientState = listOfValues[4].GetEnumFromDescription<PatientState>();
            PatientSex = (Sex)Enum.Parse(typeof(Sex), listOfValues[5]);
        }
    }
}
