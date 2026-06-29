using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using controlcbtis.Models;
using controlcbtis.Services;

namespace controlcbtis.Pages
{
    public class ArticulosModel : PageModel
    {
        private readonly MongoDBService _mongoService;

        public ArticulosModel(MongoDBService mongoService)
        {
            _mongoService = mongoService;
        }

        [BindProperty]
        public Articulo NuevoArticulo { get; set; }

        public List<Articulo> ListaArticulos { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (HttpContext.Session.GetString("Usuario") == null)
            {
                return RedirectToPage("/Login");
            }

            ListaArticulos = await _mongoService.GetArticulosAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(NuevoArticulo.Id))
            {
                await _mongoService.CreateArticuloAsync(NuevoArticulo);
            }
            else
            {
                await _mongoService.ActualizarArticuloAsync(NuevoArticulo);
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnGetEditarAsync(string id)
        {
            var articulo = (await _mongoService.GetArticulosAsync())
                .FirstOrDefault(a => a.Id == id);

            if (articulo != null)
            {
                NuevoArticulo = articulo;
            }

            ListaArticulos = await _mongoService.GetArticulosAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostEliminarAsync(string id)
        {
            await _mongoService.EliminarArticuloAsync(id);
            return RedirectToPage();
        }
    }
}