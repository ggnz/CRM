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
using OpenSaludSecurity.Data;
using OpenSaludSecurity.Models;

namespace OpenSaludSecurity.Pages.Calificaciones
{
    public class CreateModel : PageModel
    {
        private readonly OpenSaludSecurity.Data.ApplicationDbContext _context;

        public CreateModel(OpenSaludSecurity.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public string UserId { get; set; }
        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Calificacion Calificacion { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Calificaciones.Add(Calificacion);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
