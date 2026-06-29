using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using controlcbtis.Models;
using controlcbtis.Services;
using controlcbtis.PDF;

namespace controlcbtis.Pages
{
    public class pasesModel : PageModel
    {
        private readonly MongoDBService _mongoService;
        private readonly EmailService _emailService;

        public pasesModel(MongoDBService mongoService, EmailService emailService)
        {
            _mongoService = mongoService;
            _emailService = emailService;
        }

        [BindProperty]
        public Docente NuevoDocente { get; set; }

        [BindProperty]
        public PaseSalida NuevoPase { get; set; }

        public List<Docente> ListaDocentes { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (HttpContext.Session.GetString("Usuario") == null)
            {
                return RedirectToPage("/Login");
            }

            ListaDocentes = await _mongoService.GetDocentesAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _mongoService.CreateDocenteAsync(NuevoDocente);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEliminarAsync(string id)
        {
            await _mongoService.DeleteDocenteAsync(id);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostGenerarPaseAsync()
        {
            await _mongoService.CreatePaseSalidaAsync(NuevoPase);

            var pdf = PaseSalidaPDF.Generar(NuevoPase);

            await _emailService.EnviarPaseAsync(pdf);

            return File(pdf, "application/pdf", "PaseSalida.pdf");
        }


    }
}
