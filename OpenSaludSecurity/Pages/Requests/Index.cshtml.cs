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
    public class IndexModel : DI_BasePageModel
    {
        private readonly OpenSaludSecurity.Data.ApplicationDbContext _context;

        public IndexModel(
              ApplicationDbContext context,
              IAuthorizationService authorizationService,
              UserManager<IdentityUser> userManager)
              : base(context, authorizationService, userManager)
        {
        }

        public IList<Request> Request { get;set; }

        public async Task OnGetAsync()
        {
            var requests = from c in Context.Request
                           select c;

            var isAuthorized = User.IsInRole(Constants.ContactManagersRole) ||
                               User.IsInRole(Constants.ContactAdministratorsRole);

            var currentUserId = UserManager.GetUserId(User);

            // Only approved contacts are shown UNLESS you're authorized to see them
            // or you are the owner.
            if (!isAuthorized)
            {
                requests = requests.Where(c => c.Status == RequestStatus.Approved
                                            || c.OwnerID == currentUserId);
            }

            Request = await requests.ToListAsync();
        }
    }
}
