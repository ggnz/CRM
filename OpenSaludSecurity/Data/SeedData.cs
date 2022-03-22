using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OpenSaludSecurity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static OpenSaludSecurity.Models.Constants;

namespace OpenSaludSecurity.Data
{
	public static class SeedData
	{
		public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw = "")
		{
			using (var context = new ApplicationDbContext(
					serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
			{
				// For sample purposes seed both with the same password.
				// Password is set with the following:
				// dotnet user-secrets set SeedUserPW <pw>
				// The admin user can do anything

				var adminID = await EnsureUser(serviceProvider, testUserPw, "admin@contoso.com");
				await EnsureRole(serviceProvider, adminID, Constants.RequestAdministratorsRole);

				// allowed user can create and edit contacts that they create
				var managerID = await EnsureUser(serviceProvider, testUserPw, "manager@contoso.com");
				await EnsureRole(serviceProvider, managerID, Constants.RequestManagersRole);

				// allowed user can create and edit clinicas that they create
				var clinicaManagerId1 = await EnsureUser(serviceProvider, testUserPw, "clinicamanager1@contoso.com");
				await EnsureRole(serviceProvider, clinicaManagerId1, Constants.ClinicasManagersRole);

				var clinicaManagerId2 = await EnsureUser(serviceProvider, testUserPw, "clinicamanager2@contoso.com");
				await EnsureRole(serviceProvider, clinicaManagerId2, Constants.ClinicasManagersRole);

				SeedDB(context, adminID, clinicaManagerId1, clinicaManagerId2);
			}
		}

		private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider,
															  string uid, string role)
		{
			var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

			if (roleManager == null)
			{
				throw new Exception("roleManager null");
			}

			IdentityResult IR;
			if (!await roleManager.RoleExistsAsync(role))
			{
				IR = await roleManager.CreateAsync(new IdentityRole(role));
			}

			var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

			//if (userManager == null)
			//{
			//    throw new Exception("userManager is null");
			//}

			var user = await userManager.FindByIdAsync(uid);

			if (user == null)
			{
				throw new Exception("La contraseña no es suficientemente segura");
			}

			IR = await userManager.AddToRoleAsync(user, role);

			return IR;
		}

		private static async Task<string> EnsureUser(IServiceProvider serviceProvider,
											string testUserPw, string UserName)
		{
			var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

			var user = await userManager.FindByNameAsync(UserName);
			if (user == null)
			{
				user = new IdentityUser
				{
					UserName = UserName,
					EmailConfirmed = true
				};
				await userManager.CreateAsync(user, testUserPw);
			}

			if (user == null)
			{
				throw new Exception("La contraseña no es suficientemente segura!");
			}

			return user.Id;
		}

		public static void SeedDB(ApplicationDbContext context, string adminID, string clinicaManager1, string clinicaManager2)
		{
			if (!context.Request.Any())
			{
				context.Request.AddRange(
				new Request
				{
					Name = "Debra Garcia",
					Email = "debra@example.com",

					Status = RequestStatus.Approved,
					OwnerID = adminID
				},
				new Request
				{
					Name = "Thorsten Weinrich",
					Email = "thorsten@example.com"
				},
				new Request
				{
					Name = "Yuhong Li",
					Email = "yuhong@example.com",

					Status = RequestStatus.Submitted,
					OwnerID = adminID
				},
				new Request
				{
					Name = "Jon Orton",
					Email = "jon@example.com",

					Status = RequestStatus.Rejected,
					OwnerID = adminID
				},
				new Request
				{
					Name = "Diliana Alexieva-Bosseva",
					Email = "diliana@example.com",

					Status = RequestStatus.Submitted,
					OwnerID = adminID
				},
				new Request
				{
					Name = "Diliana2 Alexieva-Bosseva",
					Email = "diliana2@example.com",

					OwnerID = adminID
				}
				);
			}
			Clinica clinica1, clinica2, clinica3, clinica4;
			clinica1 = new Clinica
			{
				Nombre = "Clinica Alajuela",
				Descripcion = "30 años de brindarle los mejores servicios medicos",

				IdRepresentante = clinicaManager1,
				Direccion = "25 m norte del Super los Jardines",
				Categoria = ServicioMedico.Pediatria | ServicioMedico.Farmacia | ServicioMedico.Odontología,
				Ciudad = "Alajueal Centro",
				Telefono = "24432424",
				Email = "clinicalajuela@gmail.com",


			};
			clinica2 = new Clinica
			{
				Nombre = "Clinica Heredia",
				Descripcion = "10 años de brindarle los mejores servicios pediatricos",

				IdRepresentante = clinicaManager1,
				Direccion = "100 metros norte de la Universidad Nacional",
				Categoria = ServicioMedico.Pediatria,
				Ciudad = "Heredia Centro",
				Telefono = "23542261",
				Email = "clinicaheredia@gmail.com",
			};
			clinica3 = new Clinica
			{
				Nombre = "Clinica San Jose",
				Descripcion = "Clinica Familiar",

				IdRepresentante = clinicaManager2,
				Direccion = "250 metros norte del Museo Nacional",
				Categoria = ServicioMedico.Pediatria | ServicioMedico.Farmacia | ServicioMedico.Odontología | ServicioMedico.Dermatología,
				Ciudad = "San Jose Centro",
				Telefono = "21546399",
				Email = "clinicasanjose@gmail.com",
			};
			clinica4 = new Clinica
			{
				Nombre = "Clinica Cartago",
				Descripcion = "Estamos para mejorar su salud",

				IdRepresentante = clinicaManager2,
				Direccion = "150 metros este del Tecnologico",
				Categoria = ServicioMedico.Pediatria | ServicioMedico.Farmacia | ServicioMedico.Odontología | ServicioMedico.Psiquiatría,
				Ciudad = "Cartago Centro",
				Telefono = "28674563",
				Email = "clinicacartago@gmail.com",
			};

			if (!context.Clinica.Any())
			{				
				// Agregar Clinicas de ejemplo
				context.Clinica.AddRange(
					clinica1,
					clinica2,
					clinica3,
					clinica4
				);
			}

			if (!context.Medico.Any())
			{
				// Agregar Medicos de ejemplo
				context.Medico.AddRange(
				new Medico

				{
					Nombre = "Nathalia",
					Apellido1 = "Hernandez",
					Apellido2 = "Oreamuno",
					Especialidad = EspecialidadMedica.Dermatología,
					Clinica = clinica1
				},
				new Medico

				{
					Nombre = "Maria",
					Apellido1 = "Perez",
					Apellido2 = "Garzona",
					Especialidad = EspecialidadMedica.Cardiología,
					Clinica = clinica2
				},
				new Medico

				{
					Nombre = "Javier",
					Apellido1 = "Fernandez",
					Apellido2 = "Lopez",
					Especialidad = EspecialidadMedica.Endicronología,
					Clinica = clinica3
				},
				new Medico

				{
					Nombre = "Luis",
					Apellido1 = "Carlos",
					Apellido2 = "Soto",
					Especialidad = EspecialidadMedica.Ginecología,
					Clinica = clinica4
				},
				new Medico

				{
					Nombre = "Carlos",
					Apellido1 = "Perez",
					Apellido2 = "Solis",
					Especialidad = EspecialidadMedica.Pediatria,
					Clinica = clinica1
				},
				new Medico

				{
					Nombre = "Francisco",
					Apellido1 = "Oduber",
					Apellido2 = "Cruz",
					Especialidad = EspecialidadMedica.Urología,
					Clinica = clinica2
				},
				new Medico

				{
					Nombre = "David",
					Apellido1 = "Jimenez",
					Apellido2 = "Jimenez",
					Especialidad = EspecialidadMedica.Vascular_Periferíco,
					Clinica = clinica3
				},
				new Medico

				{
					Nombre = "Olga",
					Apellido1 = "Martinez",
					Apellido2 = "Del Carmen",
					Especialidad = EspecialidadMedica.Oftalmología,
					Clinica = clinica4
				}
				);
			}
			context.SaveChanges();
		}

	}
}
