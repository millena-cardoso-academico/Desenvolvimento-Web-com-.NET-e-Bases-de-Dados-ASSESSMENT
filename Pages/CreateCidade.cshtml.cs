using AgenciaTurismo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering; // Necessário no SelectList

namespace AgenciaTurismo.Pages
{
    public class CreateCidadeModel : PageModel
    {
        [BindProperty]
        public CidadeDestino Cidade { get; set; }

        // Propriedade para popular o dropdown de países.
        public SelectList PaisesSelectList { get; set; }

        // Método para carregar dados iniciais (simulados)
        private void CarregarPaises()
        {
            // Simulação de busca de países no banco de dados.
            var paises = new List<PaisDestino>
            {
                new PaisDestino { Id = 1, Nome = "Brasil" },
                new PaisDestino { Id = 2, Nome = "Portugal" },
                new PaisDestino { Id = 3, Nome = "Itália" }
            };
            PaisesSelectList = new SelectList(paises, "Id", "Nome");
        }


        public void OnGet()
        {
            CarregarPaises();
        }

        public IActionResult OnPost()
        {

            if (!ModelState.IsValid)
            {
                // Se o modelo NÃO é válido, eu recarrego a lista de países
                // (senão o dropdown ficaria vazio) e retorno a própria página.
                CarregarPaises();
                return Page();
            }

           
            TempData["SuccessMessage"] = $"A cidade '{Cidade.Nome}' foi cadastrada com sucesso!";

            return RedirectToPage("./Index");
        }
    }
}