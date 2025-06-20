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
            // Define uma data padr�o no campo de data, pra n�o vir vazio.
            Pacote = new PacoteTuristico
            {
                DataPartida = DateTime.Today.AddDays(7)
            };
        }

        public IActionResult OnPost()
        {
            // Valida��o extra que n�o pode ser feita com atributos.
            // Regra de neg�cio: a data de partida n�o pode ser no passado.
            if (Pacote.DataPartida < DateTime.Today)
            {
                ModelState.AddModelError("Pacote.DataPartida", "A data de partida n�o pode ser uma data passada.");
            }

            if (!ModelState.IsValid)
            {
                // Se o modelo for inv�lido, apenas retorna a p�gina.
                return Page();
            }


            TempData["SuccessMessage"] = $"O pacote '{Pacote.Titulo}' foi cadastrado com sucesso!";
            return RedirectToPage("./Index");
        }
    }
}