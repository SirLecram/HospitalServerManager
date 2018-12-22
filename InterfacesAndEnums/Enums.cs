using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalServerManager.InterfacesAndEnums
{
    public enum AcademicDegrees
    {
        [Description("lek. rez.")]
        LekRez,
        [Description("lek. med.")]
        LekMed,
        [Description("lek. spec.")]
        LekSpec,
        [Description("dr")]
        Dr,
        [Description("dr hab.")]
        DrHab,
        [Description("prof.")]
        Prof,
    }
    public enum MedicalSpecializations
    {
        [Description("Chirurgia ogólna")]
        GeneralSurgery,
        [Description("Chirurgia klatki piersiowej")]
        ThoracicSurgery,
        [Description("Chirurgia sercowo - naczyniowa")]
        CardiovascuralSurgery,
        [Description("Chirurgia układu nerwowego")]
        NewvousSurgery,
        [Description("Urologia")]
        Urology,
        [Description("Chirurgia szczękowo - twarzowa")]
        MaxillofacialSurgery,
        [Description("Chirurgia urazowa")]
        AccidentSurgery,
    }
    public enum JobPositions
    {

        [Description("Lekarz ogólny")]
        GeneralPracticioner,
        [Description("Lekarz prowadzący")]
        AttendingPhysician,
        [Description("Zastępca kierownika")]
        ViceManager,
        [Description("Kierownik")]
        Manager,
        [Description("Ordynator")]
        Director,
    }
    public enum PatientState
    {
        [Description("KRYTYCZNY")]
        Critical,
        [Description("STABILNY")]
        Stable,
        [Description("ZAGROŻONY")]
        Endangered,
        [Description("NULL")]
        None,
    }


    public enum Sex
    {
        [Description("K")]
        K,
        [Description("M")]
        M,
    }
	public enum SurgeryField
	{
		[Description("Ogólna")]
		General,
		[Description("Klatki piersiowej")]
		Throatic,
		[Description("Sercowo - naczyniowa")]
		Cardioviscular,
		[Description("Układu nerwowego")]
		NervousSystem,
		[Description("Urologia")]
		Urology,
		[Description("Szczękowo - twarzowa")]
		Maxillofacial,
		[Description("Urazowa")]
		Accidential,
		[Description("Inne")]
		Other,
	}
	public enum SurgeryKind
	{
		[Description("Szycie")]
		Stitching,
		[Description("Resekcja")]
		Resection,
		[Description("Amputacja")]
		Amputation,
		[Description("Drenaż")]
		Drainge,
		[Description("Nastawianie złamań")]
		Bonesetting,
		[Description("Ingerencja wewnątrz klatki piersiowej")]
		Throastic,
		[Description("Operacja serca")]
		Heart,
		[Description("Operacja układu nerwowego")]
		Nervous,
		[Description("Przeszczep")]
		Graft,
		[Description("Inne")]
		Other,
	}
}
