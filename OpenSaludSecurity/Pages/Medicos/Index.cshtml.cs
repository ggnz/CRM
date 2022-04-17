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
using Microsoft.Extensions.Configuration;
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
              IConfiguration configuration,
              IAuthorizationService authorizationService,
              UserManager<IdentityUser> userManager)
              : base(context, authorizationService, userManager)
        {
            List<SelectListItem> enumList = new List<SelectListItem>();
            foreach (EspecialidadMedica s in Enum.GetValues(typeof(EspecialidadMedica)))
            {
                if (s != EspecialidadMedica.NoDisponible)
                {
                    enumList.Add(new SelectListItem { Value = s.ToString(), Text = s.ToString() });
                }
            }
            EspecialidadesMedicas = new SelectList(enumList, "Value", "Text");            
            Configuration = configuration;
        }

        private readonly IConfiguration Configuration;


        //SearchBarV2
        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public PaginatedList<Medico> MedicosList { get; set; }


        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public SelectList EspecialidadesMedicas { get; set; }

        [BindProperty(SupportsGet = true)]
        public List<string> EspecialidadMedicaSeleccionada { get; set; }


        public List<Medico> Medicos { get; set; }

        public Clinica Clinica { get; set; }


        public async Task OnGetAsync(int? idClinica, string sortOrder,
            string currentFilter, string searchString, int? pageIndex)
        {
            var medicos = from c in Context.Medico
                          select c;

                        
            IQueryable<Medico> medicosIQ = from c in Context.Medico select c;
            
            Medicos = await medicosIQ.AsNoTracking().ToListAsync();

            //SearchBar
            if (!string.IsNullOrEmpty(SearchString))
            {
                medicos = medicos.Where(s => s.Nombre.Contains(SearchString)
                    || s.Apellido1.Contains(SearchString));

            }

            //Paginas
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var pageSize = Configuration.GetValue("PageSize", 3);
            MedicosList = await PaginatedList<Medico>.CreateAsync(
                medicosIQ.AsNoTracking(), pageIndex ?? 1, pageSize);



            //Filtros
            List<EspecialidadMedica> selecciones = new List<EspecialidadMedica>();
            if (EspecialidadMedicaSeleccionada.Any())
            {
                foreach (string item in EspecialidadMedicaSeleccionada)
                {
                    Enum.TryParse(item, out EspecialidadMedica s);
                    if (!s.Equals(EspecialidadMedica.NoDisponible))
                    {
                        selecciones.Add(s);
                    }
                }

            }                    

            
            Medicos = await medicos.ToListAsync();

            if (selecciones.Any())
            {
                Medicos = Medicos.Where(m => selecciones.Contains(m.Especialidad)).ToList();
            }




            if (idClinica != null)
            {
                Medicos = Medicos.Where(m => m.ClinicaRefId == idClinica).ToList();
            }

            if (Medicos.Any())
            {
                foreach (Medico m in Medicos)
                {
                    m.Clinica = await Context.Clinica.FirstOrDefaultAsync(c => c.IdClinica == m.ClinicaRefId);
                    if (Clinica == null && idClinica != null)
                    {
                        Clinica = m.Clinica;
                    }
                }
            }

        }
    }
}
