using System.ComponentModel.DataAnnotations;

namespace OpenSaludSecurity.Models
{
    public class Cita
    {
        [Key]
        public int IdCita { get; set; }

        [Key]
        //ForeignKey from Clinica
        public int IdClinica { get; set; }

        [Key]
        //ForeignKey from Medico
        public int IdMedico { get; set; }

        [Key]
        //ForeignKey from Usuario
        public int IdUsuario { get; set; }


        [Required]
        [MaxLength(600)]
        public string DescripcionCita { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public string FechaCita { get; set; }

        public RequestEstado Estado { get; set; }
    }

    public enum RequestEstado
    {
        Pendiente,
        Cancelada,
        Confirmada
    }
}
