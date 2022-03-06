using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OpenSaludSecurity.Data;
using OpenSaludSecurity.Models;

namespace OpenSaludSecurity.Pages.Medicos
{
    public class CreateModel : PageModel
    {
        private readonly OpenSaludSecurity.Data.ApplicationDbContext _context;

        public CreateModel(OpenSaludSecurity.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Medico Medico { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Medico.Add(Medico);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
