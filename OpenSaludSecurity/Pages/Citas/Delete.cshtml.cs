using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OpenSaludSecurity.Data;
using OpenSaludSecurity.Models;

namespace OpenSaludSecurity.Pages.Citas
{
    public class DeleteModel : PageModel
    {
        private readonly OpenSaludSecurity.Data.ApplicationDbContext _context;

        public DeleteModel(OpenSaludSecurity.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Cita Cita { get; set; }

        /// <summary>
        /// Utilizando el id recibido como parametro de ruta, se busca la cita en la base de datos para mostrar los datos y el mensaje de confirmacion.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Cita = await _context.Citas.FirstOrDefaultAsync(m => m.IdCita == id);

            if (Cita == null)
            {
                return NotFound();
            }
            return Page();
        }

        /// <summary>
        /// Se ejecuta el comando de remover el objecto de cita de la base de datos basado en el parametro de ruta id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Cita = await _context.Citas.FindAsync(id);

            if (Cita != null)
            {
                _context.Citas.Remove(Cita);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
