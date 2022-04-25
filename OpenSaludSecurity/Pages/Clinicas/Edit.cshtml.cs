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

namespace OpenSaludSecurity.Pages.Clinicas
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
        public Clinica Clinica { get; set; }

        /// <summary>
        /// Basado en un parametro de ruta id, busca el elemento de la base de datos correspondiente para mostrar los detalles de la clinica
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Clinica? clinica = await Context.Clinica.FirstOrDefaultAsync(m => m.IdClinica == id);

            if (clinica == null)
            {
                return NotFound();
            }

            Clinica = clinica;

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                      User, Clinica,
                                                      ContactOperations.Update);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        /// Salva el objecto de Cita con los datos del formulario.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Fetch Clinica from DB to get IdRepresentante.
            var clinica = await Context
                .Clinica.AsNoTracking()
                .FirstOrDefaultAsync(m => m.IdClinica == id);

            if (clinica == null)
            {
                return NotFound();
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                     User, clinica,
                                                     ContactOperations.Update);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            Context.Attach(Clinica).State = EntityState.Modified;

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClinicaExists(Clinica.IdClinica))
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

        private bool ClinicaExists(int id)
        {
            return Context.Clinica.Any(e => e.IdClinica == id);
        }
    }
}
