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
}
