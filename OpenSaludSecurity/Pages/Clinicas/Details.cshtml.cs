using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OpenSaludSecurity.Data;
using OpenSaludSecurity.Models;
using OpenSaludSecurity.Pages.Shared;

namespace OpenSaludSecurity.Pages.Clinicas
{
    [AllowAnonymous]
    public class DetailsModel : _BasePageModel
    {
        private readonly IEmailSender _emailSender;
        private readonly ILogger<DetailsModel> _logger;

        public DetailsModel(
        ApplicationDbContext context,
        IAuthorizationService authorizationService,
        UserManager<IdentityUser> userManager,
        ILogger<DetailsModel> logger,
        IEmailSender emailSender)
        : base(context, authorizationService, userManager)
        {
            this._logger = logger;
            this._emailSender = emailSender;
        }

        public Clinica Clinica { get; set; }

        /// <summary>
        /// Basado en un parametro de ruta id, busca el elemento de la base de datos correspondiente para mostrar los detalles de la clinica
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Clinica? clinica = await Context.Clinica.FirstOrDefaultAsync(m => m.IdClinica == id);

            if (clinica == null)
            {
                return NotFound();
            }
            Clinica = clinica;

            var isAuthorized = User.IsInRole(Constants.RequestAdministratorsRole);

            var currentUserId = UserManager.GetUserId(User);

            if (!isAuthorized
                && currentUserId != Clinica.IdRepresentante
                && Clinica.Status != Constants.RequestStatus.Approved)
            {
                return Forbid();
            }
            return Page();
        }

        /// <summary>
        /// Basado en un parametro de ruta id, se busca el elemento al que se le dio aprobar, para actualizar la base de datos, validando que el usuario que ejecuta la accion
        /// tenga el acceso necesario.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync(int id, Constants.RequestStatus status)
        {
            var clinica = await Context.Clinica.FirstOrDefaultAsync(
                                                      m => m.IdClinica == id);

            if (clinica == null)
            {
                return NotFound();
            }

            var clinicaOperation = (status == Constants.RequestStatus.Approved)
                                                       ? ClinicaOperations.Approve
                                                       : ClinicaOperations.Reject;

            var isAuthorized = await AuthorizationService.AuthorizeAsync(User, clinica,
                                        clinicaOperation);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }
            clinica.Status = status;
            Context.Clinica.Update(clinica);
            await Context.SaveChangesAsync();

            if (status == Constants.RequestStatus.Approved)
            {
                _logger.LogInformation("El usuario ha aprobado una clinica.");

                await _emailSender.SendEmailAsync(clinica.Email, "Nueva Clinica ha sido aprobada",
                    $"Su clinica ha sido aprobada.");
            }
            else
            {
                _logger.LogInformation("El usuario ha denegado una clinica.");

                
                await _emailSender.SendEmailAsync(clinica.Email, "Nueva Clinica ha sido denegada",
                    $"Su clinica ha sido denegada.");
            }


            // If we got this far, something failed, redisplay form

            return RedirectToPage("./Index");
        }
    }
}
