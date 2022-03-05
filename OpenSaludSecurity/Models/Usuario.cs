using System.ComponentModel.DataAnnotations;

namespace OpenSaludSecurity.Models
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }        

        [Required]
        [MaxLength(60)]
        public string NombreUsuario { get; set; }

        [Required]
        [MaxLength(60)]
        public string Ap1Usuario { get; set; }

        [MaxLength(60)]
        public string Ap2Usuario { get; set; }

        [MaxLength(60)]
        [EmailAddress]
        public string CorreoUsuario { get; set; }

        [MaxLength(60)]
        [Phone]
        public string TelefonoUsuario { get; set; }

        public string DireccionUsuario { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{dd/mm/yyyy}")]
        public string FecNacUsuario { get; set; }

        
    }

    
}
