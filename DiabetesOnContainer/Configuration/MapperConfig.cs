using AutoMapper;
using DiabetesOnContainer.Models;
using DiabetesOnContainer.DTOs.Admin;
using DiabetesOnContainer.DTOs.Admin.log_In_Out;
using DiabetesOnContainer.DTOs.GestionPatient;
using DiabetesOnContainer.DTOs.FicheMed;
using DiabetesOnContainer.DTOs.FichePatient;

namespace DiabetesOnContainer.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            //CreateMap<DiabeticienCRUD, Diabeticien>()
            //.ForMember(mem => mem.Email
            //      , customizedData => customizedData.MapFrom(map => $"{map.Email}${MemberAccessException.}"))


            //Admin
            //--[Diabeticien]
            CreateMap<Diabeticien, DiabeticienREAD>()
                .ForMember(full => full.FullName,
                        data => data.MapFrom(map => $"Dr { map.Nom} {map.Prenom}"))
             .ReverseMap();

            CreateMap<DiabeticienCD, Diabeticien>()
                .ForMember(mem => mem.Email,
                d => d.MapFrom(map => string.Format("{0}", map.Email.EndsWith("@AlAmel.com") ? map.Email : map.Email + "@AlAmel.com")))
                .ReverseMap();

            CreateMap<DiabeticienUpdate, Diabeticien>()
                .ForMember(mem => mem.Email, d => d.MapFrom(data => data.Email + "@AlAmel.com"))
                .ReverseMap();

            //--[Assistant]
            CreateMap<Assistant, AssistREAD>()
                  .ForMember(full => full.FullName,
                        data => data.MapFrom(map => string.Format("{0} {1} {2}", map.Sexe.ToLower() == "f" ? "Mm/Mlle" : "Mr", map.Prenom, map.Nom)))
           .ReverseMap();

            CreateMap<AssistCD, Assistant>()
                .ForMember(mem => mem.Email, 
                d => d.MapFrom(map=> string.Format("{0}",map.Email.EndsWith("@AlAmel.com")?map.Email:map.Email + "@AlAmel.com")))
                .ReverseMap();
            CreateMap<Assist_Update, Assistant>()
             .ForMember(mem => mem.Email, d => d.MapFrom(data => data.Email + "@AlAmel.com"))
             .ReverseMap();

            CreateMap<AssistRegister,Assistant>()
                .ReverseMap();
            
            CreateMap<AssistCD,Assistant>()
                .ReverseMap();

            //--[GestionPatient]
            //CasComplication
            CreateMap<CasComplication, Complication_CUD>().ReverseMap();
            CreateMap<CasComplication, Complication_Read>().ReverseMap();

            //Patient
            CreateMap<Patient, Patient_READ>()
                .ForMember(mem => mem.FullName,
                        d => d.MapFrom(map => string.Format("{0} {1} {2}", map.Sexe.ToLower() == "f" ? "Mm/Mlle" : "Mr", map.Prenom, map.Nom)))
                .ReverseMap();

            CreateMap<Patient_CUD, Patient>().ReverseMap();
            CreateMap<PatientUpdate, Patient>().ReverseMap();

            //Historique
            CreateMap<Historique, Historique_READ>().ReverseMap();
            CreateMap<Historique_CUD, Historique>().ReverseMap();

            //consultation
            //CreateMap<Consultation, Consultation_Read>()
            //                    //.ForMember(mem => mem.QuestionIS, val => val.MapFrom
            //                    //               (data => $"{data.Question.Question1}"))
            //                    .ReverseMap();
            //CreateMap<Consultation_Create, Consultation>().ReverseMap();

            //Fiche Patient
            CreateMap<FichePatient, FichePatient_Read>()
                .ForMember(mem => mem.patientFullName, d => d.MapFrom
                       (data => string.Format("{0} {1} {2}", data.CinNavigation.Sexe.ToLower() == "f" ? "Mm/Mlle" : "Mr", data.CinNavigation.Prenom, data.CinNavigation.Nom)))
                .ReverseMap();
            CreateMap<FichePatient_Create, FichePatient>().ReverseMap();
            CreateMap<FichePatient,FichePatient_Patch>().ReverseMap();
         

            //--[ExamainMedical]
            //Questions
            CreateMap<Question, Question_CUD>().ReverseMap();
            CreateMap<Question, Question_READ>().ReverseMap();


            //--{examen}
            CreateMap<ExamainMedical, ExamenMed_Read>().ReverseMap();
            CreateMap<ExamenMed_CD, ExamainMedical>().ReverseMap();
            CreateMap<ExamenMed_Update  , ExamainMedical>().ReverseMap();
            CreateMap<ExamenMed_Patch  , ExamainMedical>().ReverseMap();
            //Bilan
            CreateMap<Bilan, Bilan_READ>().ReverseMap();
            CreateMap<Bilan_CUD, Bilan>().ReverseMap();

            ////ParamBio
            CreateMap<ParamsBio, ParamBio_Read>().ReverseMap();
            CreateMap<ParamsBio, ParamBio_CD>().ReverseMap();
            CreateMap<ParamsBio, ParamBio_Update>().ReverseMap();

            ////Echographie
            CreateMap<Echography, Echographie_READ>().ReverseMap();
            CreateMap<Echographie_CD, Echography>().ReverseMap();


            //-[ficheMedical]
            ///fichemed
            CreateMap<FicheMedical,FicheMedical_READ>().ReverseMap();
            CreateMap<FicheMedical_CUD, FicheMedical>().ReverseMap();
            
            
            ///Analysis
            CreateMap<Analysis, Analysis_READ>().ReverseMap();
            CreateMap<Analysis_CUD,Analysis>().ReverseMap();
  
            ///bilan
            CreateMap<Bilan,Bilan_READ>().ReverseMap();
            CreateMap<Bilan_CUD, Bilan>().ReverseMap();

            ///Traitement
            CreateMap<Traitement, Traitement_READ>().ReverseMap();
            CreateMap<Traitement_CUD, Traitement>().ReverseMap();

        }
    }
}
