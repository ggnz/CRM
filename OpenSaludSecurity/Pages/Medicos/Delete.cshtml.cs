using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OpenSaludSecurity.Data;
using OpenSaludSecurity.Models;

namespace OpenSaludSecurity.Pages.Medicos
{
    public class DeleteModel : PageModel
    {
        private readonly OpenSaludSecurity.Data.ApplicationDbContext _context;

        public DeleteModel(OpenSaludSecurity.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Medico Medico { get; set; }

        /// <summary>
        /// Utilizando el id recibido como parametro de ruta, se busca el medico en la base de datos para mostrar los datos y el mensaje de confirmacion.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Medico = await _context.Medico.FirstOrDefaultAsync(m => m.IdMedico == id);

            if (Medico == null)
            {
                return NotFound();
            }
            return Page();
        }

        /// <summary>
        /// Se ejecuta el comando de remover el objecto de medico de la base de datos basado en el parametro de ruta id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Medico = await _context.Medico.FindAsync(id);

            if (Medico != null)
            {
                _context.Medico.Remove(Medico);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
