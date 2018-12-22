using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalServerManager.InterfacesAndEnums;
using HospitalServerManager.Model.Basic;

namespace HospitalServerManager.ViewModel
{
	class DoctorViewModel
	{
		private Doctor model;
		public string PrimaryKey { get => model.PrimaryKey; }
		public string Name { get => model.Name; }
		public string AdacemicDegree { get => model._AcademicDegree.GetEnumDescription(); }
		public string MedicalSpecialization { get => model._MedicalSpecialization.GetEnumDescription(); }
		public string Surname { get => model.Surname; }
		public DateTime EmploymentDate { get => model.DateOfEmployment; }
		public string JobPosition { get => model._JobPosition.GetEnumDescription(); }

		public DoctorViewModel(Doctor model)
		{
			this.model = model;
		}
	}
}
