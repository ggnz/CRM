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

namespace OpenSaludSecurity.Pages.Clinicas
{
    public class DeleteModel : _BasePageModel
    {
        public DeleteModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        [BindProperty]
        public Clinica Clinica { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Clinica? clinica = await Context.Clinica.FirstOrDefaultAsync(
                                             m => m.IdClinica == id);

            if (clinica == null)
            {
                return NotFound();
            }

            Clinica = clinica;

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                     User, Clinica,
                                                     ContactOperations.Delete);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clinica = await Context
            .Clinica.AsNoTracking()
            .FirstOrDefaultAsync(m => m.IdClinica == id);

            if (clinica == null)
            {
                return NotFound();
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                     User, clinica,
                                                     ContactOperations.Delete);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            Context.Clinica.Remove(clinica);
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
