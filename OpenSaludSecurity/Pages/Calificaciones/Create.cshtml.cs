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

namespace OpenSaludSecurity.Pages.Calificaciones
{
    public class CreateModel : _BasePageModel
    {
        public CreateModel(ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        public string UserId { get; set; }

        public Clinica Clinica { get; set; }

        public List<SelectListItem> IdClinicasDisponibles { get; set; }
        public IList<Clinica> Clinicas { get; set; }


        /// <summary>
        /// Se cargan los elementos necesarios para display la pagina de CREATE. Se buscan las clinicas que existen en la base de datos para popular el dropdown del formulario. 
        /// </summary>
        /// <returns></returns>
        public async Task OnGet()
        {
            UserId = UserManager.GetUserId(User);

            var clinicas = from c in Context.Clinica
                           select c;

            Clinicas = await clinicas.ToListAsync();

            UserId = UserManager.GetUserId(User);

            IdClinicasDisponibles = new List<SelectListItem>();
            foreach (Clinica c in Clinicas)
            {
                IdClinicasDisponibles.Add(new SelectListItem { Value = c.IdClinica.ToString(), Text = c.Nombre });
            }
        }

        [BindProperty]
        public Calificacion Calificacion { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        /// <summary>
        /// Salva el objecto de Calificacion con los datos del formulario.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Context.Calificaciones.Add(Calificacion);
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
