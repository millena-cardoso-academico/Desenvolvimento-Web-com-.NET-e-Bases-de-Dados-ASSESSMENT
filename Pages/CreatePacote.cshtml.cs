using AgenciaTurismo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AgenciaTurismo.Pages
{
    public class CreatePacoteModel : PageModel
    {
        [BindProperty]
        public PacoteTuristico Pacote { get; set; }

        public void OnGet()
        {
            // Define uma data padrão no campo de data, pra não vir vazio.
            Pacote = new PacoteTuristico
            {
                DataPartida = DateTime.Today.AddDays(7)
            };
        }

        public IActionResult OnPost()
        {
            // Validação extra que não pode ser feita com atributos.
            // Regra de negócio: a data de partida não pode ser no passado.
            if (Pacote.DataPartida < DateTime.Today)
            {
                ModelState.AddModelError("Pacote.DataPartida", "A data de partida não pode ser uma data passada.");
            }

            if (!ModelState.IsValid)
            {
                // Se o modelo for inválido, apenas retorna a página.
                return Page();
            }


            TempData["SuccessMessage"] = $"O pacote '{Pacote.Titulo}' foi cadastrado com sucesso!";
            return RedirectToPage("./Index");
        }
    }
}