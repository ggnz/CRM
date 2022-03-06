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

namespace OpenSaludSecurity.Pages.Medicos
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

        public IList<Medico> Medicos { get;set; }

        public async Task OnGetAsync()
        {
            var medicos = from c in Context.Medico
                           select c;

            Medicos = await medicos.ToListAsync();
        }
    }
}
