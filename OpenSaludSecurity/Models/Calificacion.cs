using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenSaludSecurity.Models
{
    public class Calificacion{
        

        [Key]
        
        public int IdCalificacion { get; set; }

        [Required(ErrorMessage = "Por favor describa brevemente su comentario")]
        [Display(Name = "Comentario acerca del servicio")]
        [MaxLength(600)]
        public string Comentario { get; set; }


        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/mm/yyyy}")]
        //public string FecCalificacion { get; set; }

        public static DateTime FecCalificacion { get; }

        [Required(ErrorMessage = "Por favor puntúa el servicio")]
        [Display(Name = "Calificación")]
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