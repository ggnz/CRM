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
    public class DetailsModel : PageModel
    {
        private readonly OpenSaludSecurity.Data.ApplicationDbContext _context;

        public DetailsModel(OpenSaludSecurity.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Calificacion Calificacion { get; set; }

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
    }
}
