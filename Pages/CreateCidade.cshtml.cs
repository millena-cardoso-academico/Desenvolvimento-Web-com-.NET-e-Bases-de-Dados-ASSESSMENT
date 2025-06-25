using AgenciaTurismo.Data;
using AgenciaTurismo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering; // Necessário no SelectList
using Microsoft.EntityFrameworkCore;

namespace AgenciaTurismo.Pages
{
    public class CreateCidadeModel : PageModel
    {
        [BindProperty]
        public CidadeDestino Cidade { get; set; }

        private readonly AgenciaTurismoContext _context;
        // Propriedade para popular o dropdown de países.
        public SelectList PaisesSelectList { get; set; }
        public CreateCidadeModel(AgenciaTurismoContext context)
        {
            _context = context;
        }
        // Método para carregar dados iniciais (simulados)
        private async Task CarregarPaisesAsync()
        {
            var paises = await _context.PaisesDestino.OrderBy(p => p.Nome).ToListAsync();
            PaisesSelectList = new SelectList(paises, "Id", "Nome");
        }

        public async Task OnGetAsync()
        {
            await CarregarPaisesAsync();
        }
        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                CarregarPaisesAsync();
                return Page();
            }
            _context.CidadesDestino.Add(Cidade);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = $"A cidade '{Cidade.Nome}' foi cadastrada com sucesso!";

            return RedirectToPage("./Index");
        }
    }
}