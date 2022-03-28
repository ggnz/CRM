using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenSaludSecurity.Models
{
    public class Cita
    {
        [Key]
        public int IdCita { get; set; }

        //[Key]
        ////ForeignKey from Clinica
        //public int IdClinica { get; set; }

        //[Key]
        ////ForeignKey from Medico
        //public int IdMedico { get; set; }

        //[Key]
        ////ForeignKey from Usuario
        //public int IdUsuario { get; set; }


        [Required(ErrorMessage = "Por favor describa brevemente su cita")]
        [MaxLength(600)]
        [Display(Name = "Descripción de Cita")]
        public string DescripcionCita { get; set; }

        /*
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Fecha de la Cita")] 
        public DateTime FechaCita { get; set; } // dato string */

        [Required(ErrorMessage = "Por favor digite la fecha para la cita")]
        [Display(Name = "Fecha de la cita")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public string FechaCita { get; set; }

        [Required(ErrorMessage = "Debe digitar el estado de su cita")]
        public RequestEstado Estado { get; set; }

        public string IdUsuario { get; set; }

        [Display(Name = "Clinica")]
        [Required(ErrorMessage = "Debe seleccionar una clínica para su cita")]
        [ForeignKey("Clinica")]
        public int ClinicaRefId { get; set; }
        public Clinica Clinica { get; set; }

        public Usuario Usuario { get; set; }

        [Display(Name = "Medico")]
        [Required(ErrorMessage = "Debe seleccionar un médico para su cita")]
        [ForeignKey("Medico")]
        public int MedicoRefId { get; set; }
        public Medico Medico { get; set; }
    }

    public enum RequestEstado
    {
        Pendiente,
        Cancelada,
        Confirmada
    }
}
