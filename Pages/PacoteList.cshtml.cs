using AgenciaTurismo.BusinessLogic;
using AgenciaTurismo.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AgenciaTurismo.Pages
{
    public class PacoteListModel : PageModel
    {
        public List<PacoteTuristico> Pacotes { get; set; }

        public void OnGet()
        {
            var service = new PacoteService();
            Pacotes = service.GetAll();
        }
    }
}