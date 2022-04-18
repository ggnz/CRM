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

        public async Task OnGetAsync(int? idClinica)
        {

            

            Calificaciones = await Context.Calificaciones.ToListAsync();

            // Popular datos de clinica para cada item de Calificaciones
            await PopularDatosDeClinica(Calificaciones);
            // Popular datos de usuario para cada item de Calificaciones
            await PopularDatosDeUsuario(Calificaciones);
        }

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
