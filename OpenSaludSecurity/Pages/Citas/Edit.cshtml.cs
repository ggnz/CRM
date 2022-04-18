using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OpenSaludSecurity.Data;
using OpenSaludSecurity.Models;
using OpenSaludSecurity.Pages.Shared;

namespace OpenSaludSecurity.Pages.Citas
{
    public class EditModel : _BasePageModel
    {
        public EditModel(
              ApplicationDbContext context,
              IAuthorizationService authorizationService,
              UserManager<IdentityUser> userManager)
              : base(context, authorizationService, userManager)
        {
        }

        [BindProperty]
        public Cita Cita { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Cita = await Context.Citas.FirstOrDefaultAsync(m => m.IdCita == id);

            if (Cita == null)
            {
                return NotFound();
            }
            // Popular datos de usuario para cada item de Cita
            await PopularDatosDeUsuario(Cita);
            // Popular datos de clinica para cada item de Cita
            await PopularDatosDeClinica(Cita);
            // Popular datos de medico para cada item de Cita
            await PopularDatosDeMedico(Cita);

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Context.Attach(Cita).State = EntityState.Modified;

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CitaExists(Cita.IdCita))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CitaExists(int id)
        {
            return Context.Citas.Any(e => e.IdCita == id);
        }

        private async Task PopularDatosDeUsuario(Cita cita)
        {
            if (cita.IdUsuario == null)
            {
                return;
            }
            // Traer datos del usuarioId que existe en la cita
            IdentityUser User = await Context.Users.FirstOrDefaultAsync(u => u.Id == cita.IdUsuario);

            if (User == null)
            {
                return;
            }

            cita.Usuario = new Usuario { CorreoUsuario = User.UserName };

        }

        private async Task PopularDatosDeClinica(Cita cita)
        {
            if (cita.ClinicaRefId == 0)
            {
                return;
            }
            // Traer datos del clinicaRefId que existe en la cita
            Clinica Clinica = await Context.Clinica.FirstOrDefaultAsync(c => c.IdClinica == cita.ClinicaRefId);

            if (Clinica == null)
            {
                return;
            }

            cita.Clinica = new Clinica { Nombre = Clinica.Nombre, Categoria = Clinica.Categoria };

        }

        private async Task PopularDatosDeMedico(Cita cita)
        {
            if (cita.MedicoRefId == 0)
            {
                return;
            }
            // Traer datos del clinicaRefId que existe en la cita
            Medico Medico = await Context.Medico.FirstOrDefaultAsync(c => c.IdMedico == cita.MedicoRefId);

            if (Medico == null)
            {
                return;
            }

            cita.Medico = new Medico { Nombre = Medico.Nombre, Apellido1 = Medico.Apellido1, Apellido2 = Medico.Apellido2, Especialidad = Medico.Especialidad };

        }
    }
}
