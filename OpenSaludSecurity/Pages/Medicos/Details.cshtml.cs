using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OpenSaludSecurity.Data;
using OpenSaludSecurity.Models;

namespace OpenSaludSecurity.Pages.Medicos
{
    [AllowAnonymous]
    public class DetailsModel : PageModel
    {
        private readonly OpenSaludSecurity.Data.ApplicationDbContext _context;

        public DetailsModel(OpenSaludSecurity.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Medico Medico { get; set; }

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
    }
}
