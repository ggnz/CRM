using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenSaludSecurity.Models
{
    public class Medico
    {
        [Key]
        public int IdMedico { get; set; }


        [Required(ErrorMessage = "Por favor digite el nombre del médico")]
        [Display(Name = "Nombre")]
        [MaxLength(60)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Por favor digite el apellido paterno del médico")]
        [Display(Name = "Apelido Paterno")]
        [MaxLength(60)]
        public string Apellido1 { get; set; }

        [Required(ErrorMessage = "Por favor digite el apellido materno del médico")]
        [Display(Name = "Apellido materno")]
        [MaxLength(60)]
        public string Apellido2 { get; set; }

        [Required(ErrorMessage = "Por favor digite la especialidad del médico")]
        [Display(Name = "Especialidad")]
        public EspecialidadMedica Especialidad { get; set; }

        [Required(ErrorMessage = "Por favor seleccione la clínica al que pertenece el médico")]
        [Display(Name = "Trabaja para:")]
        [ForeignKey("Clinica")]
        public int ClinicaRefId { get; set; }

        public Clinica Clinica { get; set; }

    }

    [Flags]
    public enum EspecialidadMedica
    {
        NoDisponible = 0,
        Pediatria = 1,
        Farmacia = 2,
        Odontología = 4,
        Psiquiatría = 8,
        Dermatología = 16,
        Oftalmología = 32,
        Ginecología = 64,
        Cardiología = 128,
        Endicronología = 256,
        Psicología = 512,
        Otorrinolaringología = 1024,
        Urología = 2048,
        Vascular_Periferíco = 4096,
        Geriartría = 8192

    }



}

