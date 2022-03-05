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
    public class DetailsModel : DI_BasePageModel
    {
        private readonly OpenSaludSecurity.Data.ApplicationDbContext _context;

        public DetailsModel(
        ApplicationDbContext context,
        IAuthorizationService authorizationService,
        UserManager<IdentityUser> userManager)
        : base(context, authorizationService, userManager)
        {
        }

        public Request Request { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            Request? _request = await Context.Request.FirstOrDefaultAsync(m => m.RequestId == id);

            if (_request == null)
            {
                return NotFound();
            }
            Request = _request;

            var isAuthorized = User.IsInRole(Constants.RequestManagersRole) ||
                               User.IsInRole(Constants.RequestAdministratorsRole);

            var currentUserId = UserManager.GetUserId(User);

            if (!isAuthorized
                && currentUserId != Request.OwnerID
                && Request.Status != RequestStatus.Approved)
            {
                return Forbid();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id, RequestStatus status)
        {
            var request = await Context.Request.FirstOrDefaultAsync(
                                                      m => m.RequestId == id);

            if (request == null)
            {
                return NotFound();
            }

            var contactOperation = (status == RequestStatus.Approved)
                                                       ? ContactOperations.Approve
                                                       : ContactOperations.Reject;

            var isAuthorized = await AuthorizationService.AuthorizeAsync(User, request,
                                        contactOperation);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }
            request.Status = status;
            Context.Request.Update(request);
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
