using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OpenSaludSecurity.Data;
using OpenSaludSecurity.Models;
using OpenSaludSecurity.Pages.Shared;

namespace OpenSaludSecurity.Pages.Clinicas
{
    public class CreateModel : _BasePageModel
    {

        public CreateModel(ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {
            ServiciosMedicos = new List<SelectListItem>();
            foreach (ServicioMedico s in Enum.GetValues(typeof(ServicioMedico)))
            {
                if (s != ServicioMedico.NoDisponible)
                {
                    ServiciosMedicos.Add(new SelectListItem { Value = ((int)s).ToString(), Text = s.ToString() });
                }
            }
        }

        /// <summary>
        /// Se cargan los elementos necesarios para display la pagina de CREATE. Se extrae el id de usuario que esta logeado para definir el IdRepresentante de la nueva clinica. 
        /// </summary>
        /// <returns></returns>
        public IActionResult OnGet()
        {
            IdRepresentante = UserManager.GetUserId(User);

            return Page();
        }

        [BindProperty]
        public Clinica Clinica { get; set; }

        public string IdRepresentante { get; set; }

        public List<SelectListItem> ServiciosMedicos { get; set; }

        [BindProperty]
        public List<int> SelectedIds { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD

        /// <summary>
        /// Salva el objecto de Clinica con los datos del formulario, antes verificando que el usuario tenga el acceso necesario para crear clinicas.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Clinica.IdRepresentante = UserManager.GetUserId(User);

            //Crear categoria string de la lista de Ids seleccionados
            Clinica.Categoria = GetEspecialidadMedicaDeListaDeInts(SelectedIds);

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                            User, Clinica,
                                            ContactOperations.Create);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            Context.Clinica.Add(Clinica);
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }


        private ServicioMedico GetEspecialidadMedicaDeListaDeInts(List<int> listaDeIds)
        {
            ServicioMedico valor = (ServicioMedico) listaDeIds.Sum();
            return valor;
        }
    }
}
