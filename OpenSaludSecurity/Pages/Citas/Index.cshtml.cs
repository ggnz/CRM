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

namespace OpenSaludSecurity.Pages.Citas
{
    public class IndexModel : _BasePageModel
    {

        public IndexModel(
              ApplicationDbContext context,
              IAuthorizationService authorizationService,
              UserManager<IdentityUser> userManager)
              : base(context, authorizationService, userManager)
        {
        }

        public IList<Cita> Citas { get;set; }

        public async Task OnGetAsync()
        {
            Citas = await Context.Citas.ToListAsync();

            // Popular datos de usuario para cada item de Cita
            await PopularDatosDeUsuario(Citas);
            // Popular datos de clinica para cada item de Cita
            await PopularDatosDeClinica(Citas);
            // Popular datos de medico para cada item de Cita
            await PopularDatosDeMedico(Citas);

        }

        private async Task PopularDatosDeUsuario(IList<Cita> citas)
        {
            foreach (Cita c in citas)
            {
                if (c.IdUsuario == null)
                {
                    continue;
                }
                // Traer datos del usuarioId que existe en la cita
                var usuarios = from u in Context.Users where u.Id == c.IdUsuario
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

        private async Task PopularDatosDeClinica(IList<Cita> citas)
        {
            foreach (Cita c in citas)
            {
                if (c.ClinicaRefId == 0)
                {
                    continue;
                }
                // Traer datos del clinicaRefId que existe en la cita
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

        private async Task PopularDatosDeMedico(IList<Cita> citas)
        {
            foreach (Cita c in citas)
            {
                if (c.MedicoRefId == 0)
                {
                    continue;
                }
                // Traer datos del clinicaRefId que existe en la cita
                var medicos = from m in Context.Medico
                              where m.IdMedico == c.MedicoRefId
                              select m;

                List<Medico> Medicos = await medicos.ToListAsync();
                Medico Medico = Medicos[0];

                if (Medico == null)
                {
                    continue;
                }

                c.Medico = new Medico { Nombre = Medico.Nombre, Apellido1 = Medico.Apellido1, Apellido2 = Medico.Apellido2, Especialidad = Medico.Especialidad };
            }

        }
    }
}
