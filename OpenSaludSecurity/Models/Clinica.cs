using System;
using System.ComponentModel.DataAnnotations;
using static OpenSaludSecurity.Models.Constants;

namespace OpenSaludSecurity.Models
{
    public class Clinica {

        [Key]
        public int IdClinica { get; set; }

        [Required(ErrorMessage = "Por favor digite el nombre de la clínica")]
        [Display(Name = "Nombre de la Clínica")]
        [MaxLength(60)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Por favor digite la descripción sobre la clínica")]
        [MaxLength(300)]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Required]
        [Display(Name = "ID del Representante")]
        public string IdRepresentante { get; set; }

        [Required(ErrorMessage = "Por favor digite la dirección física de la clínica")]
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "Por favor digite la categoría de la clínica")]
        [Display(Name = "Categoría")]
        public ServicioMedico Categoria { get; set; }

        [Required(ErrorMessage = "Por favor digite la ciudad donde se encuentra la clínica")]
        [Display(Name = "Ciudad")]
        public string Ciudad { get; set; }

        [Required(ErrorMessage = "Por favor digite el número telefónico de la clínica")]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "Por favor digite la dirección de correo electrónico de la clínica")]
        [Display(Name = "Correo")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        public RequestStatus Status { get; set; }
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
