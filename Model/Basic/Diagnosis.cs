using HospitalServerManager.InterfacesAndEnums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalServerManager.Model.Basic
{
    class Diagnosis : SqlTable
    {
		public string Name { get; protected set; }
		public SurgeryField FieldOfSurgery { get; protected set; }
		public string Description { get; protected set; }

		protected Diagnosis() : base() {}
		public Diagnosis(List<string> listOfValues) 
			: base(listOfValues[0], "Symbol_ICD", new List<string>())
		{
			Name = listOfValues[1];
			FieldOfSurgery = listOfValues[2].GetEnumFromDescription<SurgeryField>();
			Description = listOfValues[3];
		}
		[JsonConstructor]
		protected Diagnosis(string icdSymbol, string name, string fieldOfSurgery, string description)
			: base(icdSymbol, "Symbol_ICD", new List<string>())
		{
			Name = name;
			FieldOfSurgery = fieldOfSurgery.GetEnumFromDescription<SurgeryField>();
			Description = description;
		}
    }
}
