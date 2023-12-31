using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        private readonly IWebHostEnvironment webHostEnvironment;

        public CreateModel(ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager,
            IWebHostEnvironment hostEnvironment)
            : base(context, authorizationService, userManager)
        {
            webHostEnvironment = hostEnvironment;
        }

        /// <summary>
        /// Se cargan los elementos necesarios para display la pagina de CREATE. Se extrae el id de usuario que esta logeado para comparar con las clinicas existentes
        /// y verificar que el usuario sea representante de la clinica. Solo se mostrara como disponible crear medicos en las clinicas de las que el usuario sea representante.
        /// </summary>
        /// <returns></returns>
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
                if (c.IdRepresentante == UserId || User.IsInRole(Constants.RequestAdministratorsRole))
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

        [BindProperty]
        public IFormFile ImagenPerfil { get; set; }


        /// <summary>
        /// Salva el objecto de Medico con los datos del formulario
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string uniqueFileName = UploadedFile();
            Medico.MedicoImagen = uniqueFileName;

            Context.Medico.Add(Medico);
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        /// <summary>
        /// Se salva el archivo subido por el ususario en el formulario en el directorio respectivo.
        /// </summary>
        /// <returns></returns>
        private string UploadedFile()
        {
            string uniqueFileName = null;

            if (ImagenPerfil != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + ImagenPerfil.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    ImagenPerfil.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
