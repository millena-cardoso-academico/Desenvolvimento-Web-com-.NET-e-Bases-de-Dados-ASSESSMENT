using AgenciaTurismo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering; // Necess�rio no SelectList

namespace AgenciaTurismo.Pages
{
    public class CreateCidadeModel : PageModel
    {
        [BindProperty]
        public CidadeDestino Cidade { get; set; }

        // Propriedade para popular o dropdown de pa�ses.
        public SelectList PaisesSelectList { get; set; }

        // M�todo para carregar dados iniciais (simulados)
        private void CarregarPaises()
        {
            // Simula��o de busca de pa�ses no banco de dados.
            var paises = new List<PaisDestino>
            {
                new PaisDestino { Id = 1, Nome = "Brasil" },
                new PaisDestino { Id = 2, Nome = "Portugal" },
                new PaisDestino { Id = 3, Nome = "It�lia" }
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
                // Se o modelo N�O � v�lido, eu recarrego a lista de pa�ses
                // (sen�o o dropdown ficaria vazio) e retorno a pr�pria p�gina.
                CarregarPaises();
                return Page();
            }

           
            TempData["SuccessMessage"] = $"A cidade '{Cidade.Nome}' foi cadastrada com sucesso!";

            return RedirectToPage("./Index");
        }
    }
}