using HospitalServerManager.Model.Basic;
using HospitalServerManager.InterfacesAndEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalServerManager.ViewModel
{
	class SurgeryViewModel : IPrimaryKeyGetable
	{
		private Surgery model;
		public string PrimaryKey { get => model.PrimaryKey; }
		public string SurgeryName { get => model.SurgeryName; }
		public TimeSpan AverageTime { get => model.AverageTime; }
		public string KindOfSurgery { get => model.KindOfSurgery.GetEnumDescription(); }
		public decimal Cost { get => model.Cost; }
		public int Refoundation { get => model.Refoundation; }

		public SurgeryViewModel(Surgery model)
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
