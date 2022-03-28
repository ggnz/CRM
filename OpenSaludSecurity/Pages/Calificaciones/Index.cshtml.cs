﻿using System;
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
    public class IndexModel : PageModel
    {
        private readonly OpenSaludSecurity.Data.ApplicationDbContext _context;

        public IndexModel(OpenSaludSecurity.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Calificacion> Calificacion { get;set; }

        public async Task OnGetAsync(int? idClinica)
        {
            //var calificaciones = from c in _context.Calificaciones select c;

            //Calificacion = await calificaciones.ToListAsync();

            //if (idClinica != null)
            //{
            //    Calificacion = Calificacion.Where(m => m.Clinica == idClinica).ToList();
            //}

            //if (Calificacion.Any())
            //{
            //    foreach (Calificacion m in Calificacion)
            //    {
            //        m.Clinica = await _context.Clinica.FirstOrDefaultAsync(c => c.IdClinica == m.Clinica);
            //    }
            //}

            Calificacion = await _context.Calificaciones.ToListAsync();
        }
    }
}
