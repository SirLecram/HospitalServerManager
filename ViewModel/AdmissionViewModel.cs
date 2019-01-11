using HospitalServerManager.InterfacesAndEnums;
using HospitalServerManager.Model.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalServerManager.ViewModel
{
	class AdmissionViewModel : IPrimaryKeyGetable
	{
		private Admission model;
		public string PrimaryKey { get => model.PrimaryKey; }
		public string AdmissionDate { get => model.AdmissionDate.ToShortDateString(); }
		public string LeavingDate { get => model.LeavingDate.HasValue ? ((DateTime)model.LeavingDate).ToShortDateString() : string.Empty; }
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
			return "Nr " + PrimaryKey + " | " + AdmissionDate + " | Pacjent: " + PatientPESEL + " Lekarz: " + MainDoctor;
		}
	}
}
