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
        public string NombreMedico { get; set; }

        [Required]
        [MaxLength(60)]
        public string Ap1Medico { get; set; }
        
        [MaxLength(60)]
        public string Ap2Medico { get; set; }

        public RequestEspecialidad Categoria { get; set; }
    
        public Clinica Clinica { get; set; }
    }

    public enum RequestEspecialidad
    {
        Pediatria,
        Farmacia,
        Odontología,
        Psiquiatría,
        Dermatología,
        Oftalmología,
        Ginecología
    }
    
   

}

