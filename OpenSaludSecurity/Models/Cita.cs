using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenSaludSecurity.Models
{
    public class Cita
    {
        [Key]
        public int IdCita { get; set; }

        [Required(ErrorMessage = "Por favor describa brevemente su cita")]
        [MaxLength(600)]
        [Display(Name = "Descripción de Cita")]
        public string DescripcionCita { get; set; }

        [Required(ErrorMessage = "Por favor digite la fecha para la cita")]
        [Display(Name = "Fecha de la cita")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public string FechaCita { get; set; }

        [Required(ErrorMessage = "Debe digitar el estado de su cita")]
        [Display(Name = "Estado de la cita")]
        public RequestEstado Estado { get; set; }

        public string IdUsuario { get; set; }

        [Display(Name = "Clínica")]
        [Required(ErrorMessage = "Debe seleccionar una clínica para su cita")]
        [ForeignKey("Clinica")]
        public int ClinicaRefId { get; set; }

        //[Display(Name = "Clínica")]
        //[Required(ErrorMessage = "Debe seleccionar una clínica para su cita")]
        public Clinica Clinica { get; set; }

        public Usuario Usuario { get; set; }

        [Display(Name = "Médico")]
        [Required(ErrorMessage = "Debe seleccionar un médico para su cita")]
        [ForeignKey("Medico")]
        public int MedicoRefId { get; set; }

        //[Required(ErrorMessage = "Debe seleccionar un médico para su cita")]
        public Medico Medico { get; set; }
    }

    public enum RequestEstado
    {
        Pendiente,
        Cancelada,
        Confirmada
    }
}
