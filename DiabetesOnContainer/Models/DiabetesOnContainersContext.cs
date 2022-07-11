using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DiabetesOnContainer.Models
{
    public partial class DiabetesOnContainersContext : DbContext
    {
        public DiabetesOnContainersContext()
        {
        }

        public DiabetesOnContainersContext(DbContextOptions<DiabetesOnContainersContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Analysis> Analyses { get; set; } = null!;
        public virtual DbSet<Assistant> Assistants { get; set; } = null!;
        public virtual DbSet<Bilan> Bilans { get; set; } = null!;
        public virtual DbSet<CasComplication> CasComplications { get; set; } = null!;
        public virtual DbSet<Diabeticien> Diabeticiens { get; set; } = null!;
        public virtual DbSet<Echography> Echographies { get; set; } = null!;
        public virtual DbSet<ExamainMedical> ExamainMedicals { get; set; } = null!;
        public virtual DbSet<FicheMedical> FicheMedicals { get; set; } = null!;
        public virtual DbSet<FichePatient> FichePatients { get; set; } = null!;
        public virtual DbSet<Historique> Historiques { get; set; } = null!;
        public virtual DbSet<ParamsBio> ParamsBios { get; set; } = null!;
        public virtual DbSet<Patient> Patients { get; set; } = null!;
        public virtual DbSet<Question> Questions { get; set; } = null!;
        public virtual DbSet<Traitement> Traitements { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Analysis>(entity =>
            {
                entity.HasKey(e => e.AnalyseId);

                entity.Property(e => e.AnalyseId).HasColumnName("Analyse_ID");

                entity.Property(e => e.AnalyseImage).HasColumnName("Analyse_image");

                entity.Property(e => e.DateEnvoi).HasColumnType("date");

                entity.Property(e => e.FicheMedId).HasColumnName("FicheMed_ID");

                entity.Property(e => e.NomAnalyse)
                    .HasMaxLength(500)
                    .HasColumnName("Nom_Analyse");

                entity.Property(e => e.ResulatAnalyse).HasMaxLength(100);

                entity.HasOne(d => d.FicheMed)
                    .WithMany(p => p.Analyses)
                    .HasForeignKey(d => d.FicheMedId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Analyses_FicheMedicals");
            });

            modelBuilder.Entity<Assistant>(entity =>
            {
                entity.HasKey(e => e.Cin)
                    .HasName("PK_Assistant");

                entity.Property(e => e.Cin)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CIN");

                entity.Property(e => e.Email)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Nom)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordHash).HasColumnName("passwordHash");

                entity.Property(e => e.PasswordSalt).HasColumnName("passwordSalt");

                entity.Property(e => e.Prenom)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Sexe)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("sexe");
            });

            modelBuilder.Entity<Bilan>(entity =>
            {
                entity.Property(e => e.BilanId).HasColumnName("Bilan_ID");

                entity.Property(e => e.BilanImage).HasColumnName("Bilan_image");

                entity.Property(e => e.DateEnvoi).HasColumnType("date");

                entity.Property(e => e.FicheMedId).HasColumnName("FicheMed_ID");

                entity.Property(e => e.NomBilan)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("Nom_bilan");

                entity.Property(e => e.NoteMedecin).IsUnicode(false);

                entity.Property(e => e.ResulatBilan)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.FicheMed)
                    .WithMany(p => p.Bilans)
                    .HasForeignKey(d => d.FicheMedId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bilan_FicheMedical");
            });

            modelBuilder.Entity<CasComplication>(entity =>
            {
                entity.HasKey(e => e.ComplicationId)
                    .HasName("PK_CasComplication");

                entity.Property(e => e.ComplicationId).HasColumnName("Complication_ID");

                entity.Property(e => e.ComplicationName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DateComplication).HasColumnType("datetime");

                entity.Property(e => e.NoteMedecin).IsUnicode(false);

                entity.Property(e => e.PatientId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Patient_ID");

                entity.Property(e => e.PlaceUrgence).IsUnicode(false);

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.CasComplications)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CasComplication_Patient");
            });

            modelBuilder.Entity<Diabeticien>(entity =>
            {
                entity.HasKey(e => e.Cin)
                    .HasName("PK_Diabeticien");

                entity.HasIndex(e => e.RefMed, "RefMedUnique")
                    .IsUnique();

                entity.Property(e => e.Cin)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CIN");

                entity.Property(e => e.Email)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Nom)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordHash).HasColumnName("passwordHash");

                entity.Property(e => e.PasswordSalt).HasColumnName("passwordSalt");

                entity.Property(e => e.Prenom)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.RefMed)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sexe)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("sexe");
            });

            modelBuilder.Entity<Echography>(entity =>
            {
                entity.HasKey(e => e.EchographieId)
                    .HasName("PK_Echographie");

                entity.Property(e => e.EchographieId).HasColumnName("Echographie_ID");

                entity.Property(e => e.AutrePathology).IsUnicode(false);

                entity.Property(e => e.ExamainId).HasColumnName("Examain_ID");

                entity.Property(e => e.ImageEchographie).HasColumnName("imageEchographie");

                entity.Property(e => e.NomEchographie).IsUnicode(false);

                entity.Property(e => e.NoteMedecin).IsUnicode(false);

                entity.HasOne(d => d.Examain)
                    .WithMany(p => p.Echographies)
                    .HasForeignKey(d => d.ExamainId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Echographie_ExamainMedical");
            });

            modelBuilder.Entity<ExamainMedical>(entity =>
            {
                entity.HasKey(e => e.ExamainId)
                    .HasName("PK_Diagnostics");

                entity.Property(e => e.ExamainId).HasColumnName("Examain_ID");

                entity.Property(e => e.ImageDiagnositc).HasColumnName("Image_Diagnositc");

                entity.Property(e => e.NomDiagnostic).IsUnicode(false);

                entity.Property(e => e.NoteMedecin).IsUnicode(false);

                entity.Property(e => e.PrescriptionId).HasColumnName("Prescription_ID");

                entity.HasOne(d => d.Prescription)
                    .WithMany(p => p.ExamainMedicals)
                    .HasForeignKey(d => d.PrescriptionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Diagnostics_FichePatient");
            });

            modelBuilder.Entity<FicheMedical>(entity =>
            {
                entity.HasKey(e => e.FicheMedId)
                    .HasName("PK_FicheMedical");

                entity.Property(e => e.FicheMedId).HasColumnName("FicheMed_ID");

                entity.Property(e => e.PatientId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Patient_ID");

                entity.Property(e => e.RefMed)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.FicheMedicals)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FicheMedical_Patient");

                entity.HasOne(d => d.RefMedNavigation)
                    .WithMany(p => p.FicheMedicals)
                    .HasPrincipalKey(p => p.RefMed)
                    .HasForeignKey(d => d.RefMed)
                    .HasConstraintName("FK_FicheMedical_Diabeticien");
            });

            modelBuilder.Entity<FichePatient>(entity =>
            {
                entity.HasKey(e => e.PrescriptionId)
                    .HasName("PK_FichePatient");

                entity.Property(e => e.PrescriptionId).HasColumnName("Prescription_ID");

                entity.Property(e => e.Cin)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CIN");

                entity.Property(e => e.DatePres).HasColumnType("datetime");

                entity.Property(e => e.MotifPres).IsUnicode(false);

                entity.Property(e => e.NomPres)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RefMed)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("RefMED");

                entity.HasOne(d => d.CinNavigation)
                    .WithMany(p => p.FichePatients)
                    .HasForeignKey(d => d.Cin)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FichePatient_Patient");

                entity.HasOne(d => d.RefMedNavigation)
                    .WithMany(p => p.FichePatients)
                    .HasPrincipalKey(p => p.RefMed)
                    .HasForeignKey(d => d.RefMed)
                    .HasConstraintName("FK_FichePatient_Diabeticien");

                entity.HasMany(d => d.Questions)
                    .WithMany(p => p.Prescriptions)
                    .UsingEntity<Dictionary<string, object>>(
                        "Consultation",
                        l => l.HasOne<Question>().WithMany().HasForeignKey("QuestionId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Consultation_Questions"),
                        r => r.HasOne<FichePatient>().WithMany().HasForeignKey("PrescriptionId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Consultation_FichePatient"),
                        j =>
                        {
                            j.HasKey("PrescriptionId", "QuestionId").HasName("PK_Consultation");

                            j.ToTable("Consultations");

                            j.IndexerProperty<int>("PrescriptionId").HasColumnName("Prescription_ID");

                            j.IndexerProperty<int>("QuestionId").HasColumnName("Question_ID");
                        });
            });

            modelBuilder.Entity<Historique>(entity =>
            {
                entity.Property(e => e.HistoriqueId).HasColumnName("Historique_ID");

                entity.Property(e => e.DateHistorique).HasColumnType("datetime");

                entity.Property(e => e.HistoriqueImage).HasColumnName("Historique_Image");

                entity.Property(e => e.NoteMedecin).IsUnicode(false);

                entity.Property(e => e.PatientId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Patient_ID");

                entity.Property(e => e.Type)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Historiques)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Historique_Patient");
            });

            modelBuilder.Entity<ParamsBio>(entity =>
            {
                entity.HasKey(e => e.ParamBioId)
                    .HasName("PK_ParamBio");

                entity.ToTable("ParamsBio");

                entity.Property(e => e.ParamBioId).HasColumnName("ParamBio_ID");

                entity.Property(e => e.ExamainId).HasColumnName("Examain_ID");

                entity.Property(e => e.MesureParam).IsUnicode(false);

                entity.Property(e => e.NomParam).IsUnicode(false);

                entity.Property(e => e.NoteMedecin).IsUnicode(false);

                entity.HasOne(d => d.Examain)
                    .WithMany(p => p.ParamsBios)
                    .HasForeignKey(d => d.ExamainId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ParamBio_ExamainMedical");
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.Cin)
                    .HasName("PK_Patient");

                entity.Property(e => e.Cin)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CIN");

                entity.Property(e => e.AssistId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Assist_ID");

                entity.Property(e => e.DateNaissance).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Nom)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Prenom)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Sexe)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("sexe");

                entity.HasOne(d => d.Assist)
                    .WithMany(p => p.Patients)
                    .HasForeignKey(d => d.AssistId)
                    .HasConstraintName("FK_Patients_Assistants");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.Property(e => e.QuestionId).HasColumnName("Question_ID");

                entity.Property(e => e.EtatDuQuestion)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MedecinNotes).IsUnicode(false);

                entity.Property(e => e.Question1).HasColumnName("Question");
            });

            modelBuilder.Entity<Traitement>(entity =>
            {
                entity.HasKey(e => e.TraitId)
                    .HasName("PK_Traitement");

                entity.Property(e => e.TraitId).HasColumnName("Trait_ID");

                entity.Property(e => e.DateEnvoi).HasColumnType("date");

                entity.Property(e => e.DateFinTrait).HasColumnType("date");

                entity.Property(e => e.DateTrait).HasColumnType("date");

                entity.Property(e => e.FicheMedId).HasColumnName("FicheMed_ID");

                entity.Property(e => e.NomTraitement)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Nom_Traitement");

                entity.Property(e => e.NoteMedecin).IsUnicode(false);

                entity.HasOne(d => d.FicheMed)
                    .WithMany(p => p.Traitements)
                    .HasForeignKey(d => d.FicheMedId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Traitement_FicheMedical");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
