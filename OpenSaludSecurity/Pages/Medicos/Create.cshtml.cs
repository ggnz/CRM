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

namespace OpenSaludSecurity.Pages.Medicos
{
    public class CreateModel : _BasePageModel
    {
        private readonly OpenSaludSecurity.Data.ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        public async Task OnGetAsync()
        {
            // Calcular el ClinicaRefId al que pertenece el representante que abrio la pagina de create
            var clinicas = from c in Context.Clinica
                          select c;

            Clinicas =  await clinicas.ToListAsync();

            UserId = UserManager.GetUserId(User);

            IdClinicasDisponibles = new List<SelectListItem>();
            foreach (Clinica c in Clinicas)
            {
                if (c.IdRepresentante == UserId)
                {
                    IdClinicasDisponibles.Add(new SelectListItem { Value = c.IdClinica.ToString(), Text = c.Nombre});
                }
            }
        }

        [BindProperty]
        public Medico Medico { get; set; }

        public int ClinicaRefId { get; set; }

        public IList<Clinica> Clinicas { get; set; }

        public List<SelectListItem> IdClinicasDisponibles { get; set; }

        public string UserId { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Context.Medico.Add(Medico);
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
