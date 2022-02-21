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

namespace OpenSaludSecurity.Pages.Requests
{
    public class DeleteModel : DI_BasePageModel
    {
        private readonly OpenSaludSecurity.Data.ApplicationDbContext _context;

        public DeleteModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        [BindProperty]
        public Request Request { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            Request? _request = await Context.Request.FirstOrDefaultAsync(
                                             m => m.RequestId == id);

            if (_request == null)
            {
                return NotFound();
            }
            Request = _request;

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                     User, Request,
                                                     ContactOperations.Delete);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            var request = await Context
            .Request.AsNoTracking()
            .FirstOrDefaultAsync(m => m.RequestId == id);

            if (request == null)
            {
                return NotFound();
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                     User, request,
                                                     ContactOperations.Delete);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            Context.Request.Remove(request);
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
