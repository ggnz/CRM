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
using Microsoft.Extensions.Configuration;

namespace OpenSaludSecurity.Pages.Clinicas
{
    [AllowAnonymous]
    public class IndexModel : _BasePageModel
    {
        private readonly IConfiguration Configuration;

        public IndexModel(
              ApplicationDbContext context,
              IAuthorizationService authorizationService,
              UserManager<IdentityUser> userManager, IConfiguration configuration)
              : base(context, authorizationService, userManager)
        {
            Configuration = configuration;
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

        public PaginatedList<Clinica> Clinicas { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public SelectList ServiciosMedicos { get; set; }

        [BindProperty(SupportsGet = true)]
        public List<string> ServicioMedicoSeleccionado { get; set; }

        //public List<Clinica> Clinicas { get;set; }

        /// <summary>
        /// Se buscan todas las citas existentes en la base de datos, y se les cargan los datos de usuario, clinica y medico correspondientes para mostrar mas detalles.
        /// Ademas se aplican la logica correspondiente para la paginacion
        /// </summary>
        /// <param name="sortOrder"></param>
        /// <param name="currentFilter"></param>
        /// <param name="searchString"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public async Task OnGetAsync(string sortOrder,
                                    string currentFilter,
                                    string searchString,
                                    int? pageIndex)
        {

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            IQueryable<Clinica> clinicas = from c in Context.Clinica
                           select c;

            if (!string.IsNullOrEmpty(SearchString))
            {
                clinicas = clinicas.Where(s => s.Nombre.Contains(SearchString));
            }

            List<ServicioMedico> selecciones = new List<ServicioMedico>();
            ServicioMedico serviciosMedicosSeleccionados = ServicioMedico.NoDisponible;
            if (ServicioMedicoSeleccionado.Any())
            {

                foreach (string item in ServicioMedicoSeleccionado)
                {
                    Enum.TryParse(item, out ServicioMedico s);
                    if (!s.Equals(ServicioMedico.NoDisponible))
                    {
                        selecciones.Add(s);
                        serviciosMedicosSeleccionados |= s;
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

            var pageSize = Configuration.GetValue("PageSize", 4);
            Clinicas = await PaginatedList<Clinica>.CreateAsync(clinicas.AsNoTracking(), pageIndex ?? 1, pageSize);

            if (selecciones.Any())
            {
                var c = await clinicas.ToListAsync();
                Clinicas = ClinicasConCategoriaParteDeSeleccion(c, selecciones, pageIndex, pageSize);
            }
        }
        
        private PaginatedList<Clinica> ClinicasConCategoriaParteDeSeleccion(List<Clinica> clinicas, List<ServicioMedico> selecciones, int? pageIndex, int? pageSize)
        {
           
            clinicas = clinicas.Where(c => CategoriaEsParteDeSeleccion(c.Categoria, selecciones)).ToList();

            return new PaginatedList<Clinica>(clinicas, clinicas.Count, pageIndex ?? 1, pageSize ?? 1);
        }

        private bool CategoriaEsParteDeSeleccion(ServicioMedico categoria, List<ServicioMedico> selecciones)
        {
            foreach (ServicioMedico seleccion in selecciones)
            {
                if (categoria.HasFlag(seleccion))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
