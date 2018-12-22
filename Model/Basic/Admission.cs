using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalServerManager.Model.Basic
{
    class Admission : SqlTable
    {
		public DateTime AdmissionDate { get; protected set; }
		public DateTime LeavingDate { get; protected set; }
		public string PatientPESEL { get; protected set; }
		public string DiagnosisSymbol { get; protected set; }
		public int MainDoctor { get; protected set; }
		public int? OperationID { get; protected set; }
		public int RoomNumber { get; protected set; }
		public bool IsPlanned { get; protected set; }

		protected Admission() : base() { }
		public Admission(List<string> listOfValues)
			: base(listOfValues[0], "Id_przyjecia", new List<string>())
		{
			AdmissionDate = DateTime.Parse(listOfValues[1]);
			LeavingDate = DateTime.Parse(listOfValues[2]);
			PatientPESEL = listOfValues[3];
			DiagnosisSymbol = listOfValues[4];
			MainDoctor = int.Parse(listOfValues[5]);
			OperationID = int.Parse(listOfValues[6]);
			RoomNumber = int.Parse(listOfValues[7]);
			IsPlanned = bool.Parse(listOfValues[8]);
		}
    }
}
