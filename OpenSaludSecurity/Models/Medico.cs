using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenSaludSecurity.Models
{
    public class Medico
    {
        [Key]
        public int IdMedico { get; set; }
             

        [Required]
        [MaxLength(60)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(60)]
        public string Apellido1 { get; set; }
        
        [MaxLength(60)]
        public string Apellido2 { get; set; }

        public EspecialidadMedica Especialidad { get; set; }

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

