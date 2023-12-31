﻿using System;
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
    public class DetailsModel : PageModel
    {
        private readonly OpenSaludSecurity.Data.ApplicationDbContext _context;

        public DetailsModel(OpenSaludSecurity.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Cita Cita { get; set; }

        /// <summary>
        /// Basado en un parametro de ruta id, busca el elemento de la base de datos correspondiente para mostrar los detalles de la cita
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
    }
}
