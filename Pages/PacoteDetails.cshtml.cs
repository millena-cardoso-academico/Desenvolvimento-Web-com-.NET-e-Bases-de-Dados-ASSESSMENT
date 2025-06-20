using AgenciaTurismo.BusinessLogic;
using AgenciaTurismo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AgenciaTurismo.Pages
{
    public class PacoteDetailsModel : PageModel
    {
        public PacoteTuristico Pacote { get; set; }

        public IActionResult OnGet(int id)
        {
            var service = new PacoteService();
            Pacote = service.GetById(id);

            if (Pacote == null)
            {
                return NotFound();
            }

            // Se encontrarmos, a página é renderizada normalmente.
            return Page();
        }
    }
}