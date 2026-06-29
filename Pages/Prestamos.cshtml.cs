using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using controlcbtis.Models;
using controlcbtis.Services;

namespace controlcbtis.Pages
{
    public class PrestamosModel : PageModel
    {
        private readonly MongoDBService _mongoService;

        public PrestamosModel(MongoDBService mongoService)
        {
            _mongoService = mongoService;
        }

        [BindProperty]
        public Prestamo NuevoPrestamo { get; set; }

        public List<Prestamo> ListaPrestamos { get; set; }
        public List<Alumno> ListaAlumnos { get; set; }
        public List<Articulo> ListaArticulos { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (HttpContext.Session.GetString("Usuario") == null)
            {
                return RedirectToPage("/Login");
            }

            ListaPrestamos = await _mongoService.GetPrestamosAsync();
            ListaAlumnos = await _mongoService.GetAlumnosAsync();
            ListaArticulos = await _mongoService.GetArticulosAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var zonaHoraria = TimeZoneInfo.FindSystemTimeZoneById("America/Mazatlan");
            NuevoPrestamo.FechaPrestamo = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, zonaHoraria);

            await _mongoService.CreatePrestamoAsync(NuevoPrestamo);
            await _mongoService.RestarArticuloAsync(
    NuevoPrestamo.Articulo,
    NuevoPrestamo.Cantidad);

            return RedirectToPage();
        }
    }
}