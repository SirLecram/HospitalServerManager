using HospitalServerManager.Model.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalServerManager.ViewModel
{
	class RoomViewModel
	{
		private Room model;
		public string PrimaryKey { get => model.PrimaryKey; }
		public int PlacesNumber { get => model.PlacesNumber; }
		public bool IsSpecialCare { get => model.IsSpecialCare; }

		public RoomViewModel(Room model)
		{
			this.model = model;
		}
	}
}
