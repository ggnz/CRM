using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OpenSaludSecurity.Data;
using OpenSaludSecurity.Models;

namespace OpenSaludSecurity.Pages.Calificaciones
{
    public class DeleteModel : PageModel
    {
        private readonly OpenSaludSecurity.Data.ApplicationDbContext _context;

        public DeleteModel(OpenSaludSecurity.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Calificacion Calificacion { get; set; }

        /// <summary>
        /// Utilizando el id recibido como parametro de ruta, se busca la calificacion en la base de datos para mostrar los datos y el mensaje de confirmacion.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Calificacion = await _context.Calificaciones.FirstOrDefaultAsync(m => m.IdCalificacion == id);

            if (Calificacion == null)
            {
                return NotFound();
            }
            return Page();
        }

        /// <summary>
        /// Se ejecuta el comando de remover el objecto de calificacion de la base de datos basado en el parametro de ruta id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Calificacion = await _context.Calificaciones.FindAsync(id);

            if (Calificacion != null)
            {
                _context.Calificaciones.Remove(Calificacion);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
