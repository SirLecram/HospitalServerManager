using HospitalServerManager.InterfacesAndEnums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalServerManager.Model.Basic
{
	class Surgery : SqlTable
	{
		public string SurgeryName { get; protected set; }
		public TimeSpan AverageTime { get; protected set; }
		public SurgeryKind KindOfSurgery { get; protected set; }
		public decimal Cost { get; protected set; }
		public int Refoundation { get; protected set; }

		protected Surgery() : base() { }
		public Surgery(List<string> listOfValues) 
			: base(listOfValues[0], "Id_operacji", new List<string>())
		{
			SurgeryName = listOfValues[1];
			AverageTime = TimeSpan.Parse(listOfValues[2]);
			KindOfSurgery = listOfValues[3].GetEnumFromDescription<SurgeryKind>();
			Cost = decimal.Parse(listOfValues[4]);
			Refoundation = int.Parse(listOfValues[5]) > 100 ? 100 : int.Parse(listOfValues[5]);
		}
		[JsonConstructor]
		protected Surgery(string operationID, string name, string averageTime, string operationType, decimal cost, int refoundation)
			:base(operationID, "Id_operacji", new List<string>())
		{
			SurgeryName = name;
			AverageTime = TimeSpan.Parse(averageTime);
			KindOfSurgery = operationType.GetEnumFromDescription<SurgeryKind>();
			Cost = cost;
			Refoundation = refoundation;
		}
	}
}
