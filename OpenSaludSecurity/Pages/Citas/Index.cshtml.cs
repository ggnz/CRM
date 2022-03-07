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
    public class IndexModel : PageModel
    {
        private readonly OpenSaludSecurity.Data.ApplicationDbContext _context;

        public IndexModel(OpenSaludSecurity.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Cita> Cita { get;set; }

        public async Task OnGetAsync()
        {
            Cita = await _context.Citas.ToListAsync();
        }
    }
}
