using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalServerManager.Model.Basic
{
    class Room : SqlTable
    {
		public int PlacesNumber { get; protected set; }
		public bool IsSpecialCare { get; protected set; }

		protected Room() : base() { }
		public Room(List<string> listOfValues) 
			: base(listOfValues[0], "Nr_sali", new List<string>())
		{
			if (int.TryParse(listOfValues[1], out int placesNumber))
				PlacesNumber = placesNumber;
			IsSpecialCare = bool.Parse(listOfValues[2]);
		}
		[JsonConstructor]
		protected Room(int roomNumber, int numberOfBeds, bool increasedCare)
			:base(roomNumber.ToString(), "Nr_sali", new List<string>())
		{
			PlacesNumber = numberOfBeds;
			IsSpecialCare = increasedCare;
		}
    }
}
