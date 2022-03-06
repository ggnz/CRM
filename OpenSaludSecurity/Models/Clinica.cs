using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenSaludSecurity.Models
{
    public class Clinica    {
        
        [Key]
        public int IdClinica { get; set; }

        [Required]
        [MaxLength(60)]
        public string NombreClinica { get; set; }

        [MaxLength(300)]
        public string Descripcion { get; set; }

        [Required]
        public int IdRepresentante { get; set; }
        public string Direccion { get; set; }
        public RequestCategoria Categoria { get; set; }



        public ICollection<Medico> Medicos { get; set; }
        public ICollection<Calificacion> Calificaciones { get; set; }
        public ICollection<Cita> Citas { get; set; }
    }

    public enum RequestCategoria
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
