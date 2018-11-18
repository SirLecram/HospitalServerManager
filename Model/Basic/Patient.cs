using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalServerManager.InterfacesAndEnums;

namespace HospitalServerManager.Model.Basic
{
    internal class Patient : SqlTable
    {
        /*public override string PrimaryKeyNameToSql { get; protected set; }
        public override string GetPrimaryKey => PeselNumber;*/
        public string Surname { get; protected set; }
        public string Name { get; protected set; }
        public DateTime BirthDate { get; protected set; }
        public PatientState _PatientState { get; protected set; }
        /*public string PatientState
        {
            get
            {
                if (_PatientState.GetEnumDescription() == "NULL")
                    return string.Empty;
                else
                    return _PatientState.GetEnumDescription();
            }
        }*/
        public Sex _PatientSex { get; protected set; }
       // public string PatientSex { get => _PatientSex.GetEnumDescription(); }



        protected Patient() : base()
        {
        }
        public Patient(string primaryKey, string surname, string name, DateTime birthDate, PatientState patientState,
            Sex patientSex) : base(primaryKey, "PESEL", new List<string>())
        {
            if (primaryKey.Length < 11 || primaryKey.Length > 11)
                throw new FormatException("PESEL musi mieć 11 cyfr");
            PrimaryKey = primaryKey;
            Surname = surname;
            Name = name;
            BirthDate = birthDate;
            _PatientState = patientState;
            _PatientSex = patientSex;
        }
        /// <summary>
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
            _PatientState = listOfValues[4].GetEnumFromDescription<PatientState>();
            _PatientSex = (Sex)Enum.Parse(typeof(Sex), listOfValues[5]);
        }
    }
}
