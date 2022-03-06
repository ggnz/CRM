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
    public class IndexModel : _BasePageModel
    {

        public IndexModel(
              ApplicationDbContext context,
              IAuthorizationService authorizationService,
              UserManager<IdentityUser> userManager)
              : base(context, authorizationService, userManager)
        {
        }

        public IList<Clinica> Clinicas { get;set; }

        public async Task OnGetAsync()
        {
            var clinicas = from c in Context.Clinica
                           select c;

            var isAuthorized = User.IsInRole(Constants.RequestManagersRole) ||
                               User.IsInRole(Constants.RequestAdministratorsRole);

            var currentUserId = UserManager.GetUserId(User);

            // Only approved clinicas are shown UNLESS you're authorized to see them
            // or you are the owner.
            if (!isAuthorized)
            {
                clinicas = clinicas.Where(c => c.Status == Constants.RequestStatus.Approved
                                            || c.IdRepresentante == currentUserId);
            }

            Clinicas = await clinicas.ToListAsync();
        }
    }
}
