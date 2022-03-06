using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OpenSaludSecurity.Data;
using OpenSaludSecurity.Models;

namespace OpenSaludSecurity.Pages.Medicos
{
    public class EditModel : PageModel
    {
        private readonly OpenSaludSecurity.Data.ApplicationDbContext _context;

        public EditModel(OpenSaludSecurity.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Medico).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicoExists(Medico.IdMedico))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MedicoExists(int id)
        {
            return _context.Medico.Any(e => e.IdMedico == id);
        }
    }
}
