using HospitalServerManager.Model.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalServerManager.ViewModel
{
	class AdmissionViewModel
	{
		private Admission model;
		public string PrimaryKey { get => model.PrimaryKey; }
		public DateTime AdmissionDate { get => model.AdmissionDate; }
		public DateTime LeavingDate { get => model.LeavingDate; }
		public string PatientPESEL { get => model.PatientPESEL; }
		public string DiagnosisSymbol { get => model.DiagnosisSymbol; }
		public int MainDoctor { get => model.MainDoctor; }
		public int? OperationID { get => model.OperationID; }
		public int RoomNumber { get => model.RoomNumber; }
		public bool IsPlanned { get => model.IsPlanned; }

		public AdmissionViewModel(Admission model)
		{
			this.model = model;
		}
	}
}
