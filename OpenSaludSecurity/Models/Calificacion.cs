using System;
using System.ComponentModel.DataAnnotations;
namespace OpenSaludSecurity.Models
{
    public class Calificacion{
        

        [Key]
        
        public int IdCalificacion { get; set; }
        //[Key]
        ////ForeignKey from Usuario
        //public int IdUsuario { get; set; }

        //[Key]
        ////ForeignKey from Clinica
        //public int IdClinica { get; set; }


        [Required]
        [MaxLength(600)]
        public string Comentario { get; set; }


        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/mm/yyyy}")]
        //public string FecCalificacion { get; set; }

        public static DateTime FecCalificacion { get; }

        [Required]
        public RequestEstrellas Estrellas { get; set; }


        public Clinica Clinica { get; set; }

        public Usuario Usuario { get; set; }
    }

    public enum RequestEstrellas
    {
        Una,
        Dos,
        Tres,
        Cuatro,
        Cinco
    }
}