using HospitalServerManager.Model.Basic;
using HospitalServerManager.InterfacesAndEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalServerManager.ViewModel
{
	class DiagnosisViewModel : IPrimaryKeyGetable
	{
		private Diagnosis model;
		public string PrimaryKey { get => model.PrimaryKey; }
		public string Name { get => model.Name; }
		public string FieldOfSurgery { get => model.FieldOfSurgery.GetEnumDescription(); }
		public string Description { get => model.Description; }

		public DiagnosisViewModel(Diagnosis model)
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
	}
}
