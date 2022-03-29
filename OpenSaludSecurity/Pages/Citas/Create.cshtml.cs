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

namespace OpenSaludSecurity.Pages.Citas
{
    public class CreateModel : _BasePageModel
    {

        public CreateModel(ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {
            
        }

        public async Task OnGetAsync()
        {
            var clinicas = from c in Context.Clinica
                           select c;

            clinicas = clinicas.Where(c => c.Status == Constants.RequestStatus.Approved);

            Clinicas = await clinicas.ToListAsync();
            ClinicasDisponibles = new List<SelectListItem>();
            foreach (Clinica c in Clinicas)
            {
                 ClinicasDisponibles.Add(new SelectListItem { Value = c.IdClinica.ToString(), Text = c.Nombre });
            }

            var medicos = from m in Context.Medico
                           select m;

            if (!string.IsNullOrEmpty(IdClinicaSeleccion))
            {
                medicos = medicos.Where(m => m.ClinicaRefId == Int32.Parse(IdClinicaSeleccion));
            }

            Medicos = await medicos.ToListAsync();
            MedicosDisponibles = new List<SelectListItem>();
            foreach (Medico m in Medicos)
            {
                MedicosDisponibles.Add(new SelectListItem { Value = m.IdMedico.ToString(), Text = m.Nombre + " " + m.Apellido1 });
            }

            ViewData["Medicos"] = Medicos;

        }

        [BindProperty]
        public Cita Cita { get; set; }

        [BindProperty]
        public string IdClinicaSeleccion{ get; set; }

        [BindProperty]
        public string IdMedicoSeleccion { get; set; }

        public List<SelectListItem> ClinicasDisponibles { get; set; }

        public List<Clinica> Clinicas { get; set; }

        public List<SelectListItem> MedicosDisponibles { get; set; }

        public List<Medico> Medicos { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Cita.IdUsuario = UserManager.GetUserId(User);
            Cita.ClinicaRefId = Int32.Parse(IdClinicaSeleccion);
            Cita.MedicoRefId = Int32.Parse(IdMedicoSeleccion);
            Context.Citas.Add(Cita);
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        public void clinicaOnChange(string clinicaId)
        {
            Console.WriteLine($"ClinicaSelect changed {clinicaId}");
        }
    }
}
