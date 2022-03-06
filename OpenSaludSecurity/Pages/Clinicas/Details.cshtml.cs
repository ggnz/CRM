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
    [AllowAnonymous]
    public class DetailsModel : _BasePageModel
    {
        public DetailsModel(
        ApplicationDbContext context,
        IAuthorizationService authorizationService,
        UserManager<IdentityUser> userManager)
        : base(context, authorizationService, userManager)
        {
        }

        public Clinica Clinica { get; set; }

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

            var isAuthorized = User.IsInRole(Constants.RequestAdministratorsRole);

            var currentUserId = UserManager.GetUserId(User);

            if (!isAuthorized
                && currentUserId != Clinica.IdRepresentante
                && Clinica.Status != Constants.RequestStatus.Approved)
            {
                return Forbid();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id, Constants.RequestStatus status)
        {
            var clinica = await Context.Clinica.FirstOrDefaultAsync(
                                                      m => m.IdClinica == id);

            if (clinica == null)
            {
                return NotFound();
            }

            var clinicaOperation = (status == Constants.RequestStatus.Approved)
                                                       ? ClinicaOperations.Approve
                                                       : ClinicaOperations.Reject;

            var isAuthorized = await AuthorizationService.AuthorizeAsync(User, clinica,
                                        clinicaOperation);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }
            clinica.Status = status;
            Context.Clinica.Update(clinica);
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
