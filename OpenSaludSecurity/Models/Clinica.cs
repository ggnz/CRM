using System;
using System.ComponentModel.DataAnnotations;

namespace OpenSaludSecurity.Models
{
    public class Clinica {

        [Key]
        public int IdClinica { get; set; }

        [Required]
        [MaxLength(60)]
        public string Nombre { get; set; }

        [MaxLength(300)]
        public string Descripcion { get; set; }

        [Required]
        public string IdRepresentante { get; set; }
        public string Direccion { get; set; }
        public ServicioMedico Categoria { get; set; }
        public string Ciudad { get; set; }
        public string Telefono { get; set; }
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
    }

    [Flags]
    public enum ServicioMedico
    {
        NoDisponible = 0,
        Pediatria = 1,
        Farmacia = 2,
        Odontología = 4,
        Psiquiatría = 8,
        Dermatología = 16,
        Oftalmología = 32,
        Ginecología = 64
    }


}
