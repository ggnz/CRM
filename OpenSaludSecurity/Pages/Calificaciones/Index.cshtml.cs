using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OpenSaludSecurity.Data;
using OpenSaludSecurity.Models;
using OpenSaludSecurity.Pages.Shared;

namespace OpenSaludSecurity.Pages.Calificaciones
{
    public class IndexModel : _BasePageModel
    {

        public IndexModel(ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        public IList<Calificacion> Calificaciones { get;set; }

        public Clinica Clinica { get; set; }

        /// <summary>
        /// Como parametro opcional, idClinica se usa para filtrar calificaciones que pertenezcan a dicha clinica, luego de que se hayan recolectado los datos de la base de datos.
        /// </summary>
        /// <param name="idClinica"></param>
        /// <returns></returns>
        public async Task OnGetAsync(int? idClinica)
        {

            var calificaciones = from c in Context.Calificaciones
                          select c;
            
            if (idClinica > 0)
            {
                calificaciones = calificaciones.Where(c => c.ClinicaRefId == idClinica);
            }

            Calificaciones = await calificaciones.ToListAsync();

            // Popular datos de clinica para cada item de Calificaciones
            await PopularDatosDeClinica(Calificaciones);
            // Popular datos de usuario para cada item de Calificaciones
            await PopularDatosDeUsuario(Calificaciones);
        }

        /// <summary>
        /// Se buscan los datos de clinica correspondiente de cada calificacion en la lista del parametro para popular el objecto y mostrar detalles en la pagina.
        /// </summary>
        /// <param name="calificaciones"></param>
        /// <returns></returns>
        private async Task PopularDatosDeClinica(IList<Calificacion> calificaciones)
        {
            foreach (Calificacion c in calificaciones)
            {
                if (c.ClinicaRefId == 0)
                {
                    continue;
                }
                // Traer datos del clinicaRefId que existe en la calificacion
                var clinicas = from clinica in Context.Clinica
                               where clinica.IdClinica == c.ClinicaRefId
                               select clinica;

                List<Clinica> Clinicas = await clinicas.ToListAsync();
                Clinica Clinica = Clinicas[0];

                if (Clinica == null)
                {
                    continue;
                }

                c.Clinica = new Clinica { Nombre = Clinica.Nombre, Categoria = Clinica.Categoria };
            }

        }

        /// <summary>
        /// Se buscan los datos de usuario correspondiente de cada calificacion en la lista del parametro para popular el objecto y mostrar detalles en la pagina.
        /// </summary>
        /// <param name="calificaciones"></param>
        /// <returns></returns>
        private async Task PopularDatosDeUsuario(IList<Calificacion> calificaciones)
        {
            foreach (Calificacion c in calificaciones)
            {
                if (c.IdUsuario == null)
                {
                    continue;
                }
                // Traer datos del usuarioId que existe en la cita
                var usuarios = from u in Context.Users
                               where u.Id == c.IdUsuario
                               select u;

                List<IdentityUser> Usuarios = await usuarios.ToListAsync();
                IdentityUser Usuario = Usuarios[0];

                if (Usuario == null)
                {
                    continue;
                }

                c.Usuario = new Usuario { CorreoUsuario = Usuario.UserName };
            }

        }
    }
}
