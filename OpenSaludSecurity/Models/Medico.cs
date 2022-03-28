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

    public enum EspecialidadMedica
    {
        Pediatria,
        Odontología,
        Psiquiatría,
        Dermatología,
        Oftalmología,
        Ginecología,
        Cardiología,
        Endicronología,
        Psicología,
        Otorrinolaringología,
        Urología,
        Vascular_Periferíco,
        Geriartría
    }
    
   

}

