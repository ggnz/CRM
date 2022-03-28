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
            List<SelectListItem> enumList = new List<SelectListItem>();

            foreach (ServicioMedico s in Enum.GetValues(typeof(ServicioMedico)))
            {
                if (s != ServicioMedico.NoDisponible)
                {
                    enumList.Add(new SelectListItem { Value = s.ToString(), Text = s.ToString() });
                }
            }
            ServiciosMedicos = new SelectList(enumList, "Value", "Text");
        }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public SelectList ServiciosMedicos { get; set; }

        [BindProperty(SupportsGet = true)]
        public List<string> ServicioMedicoSeleccionado { get; set; }

        public List<Clinica> Clinicas { get;set; }

        public async Task OnGetAsync()
        {
            var clinicas = from c in Context.Clinica
                           select c;

            if (!string.IsNullOrEmpty(SearchString))
            {
                clinicas = clinicas.Where(s => s.Nombre.Contains(SearchString));
            }

            List<ServicioMedico> selecciones = new List<ServicioMedico>();
            if (ServicioMedicoSeleccionado.Any())
            {
                
                foreach (string item in ServicioMedicoSeleccionado)
                {
                    Enum.TryParse(item, out ServicioMedico s);
                    if (!s.Equals(ServicioMedico.NoDisponible))
                    {
                        selecciones.Add(s);
                    }
                }

            }
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

            if (selecciones.Any())
            {
                Clinicas = Clinicas.Where(c => selecciones.Contains(c.Categoria)).ToList();
            }
        }
    }
}
