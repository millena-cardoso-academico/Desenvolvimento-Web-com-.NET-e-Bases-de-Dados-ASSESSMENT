using AgenciaTurismo.Data; 
using AgenciaTurismo.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AgenciaTurismo.Pages
{
    public class PacoteListModel : PageModel
    {
        private readonly AgenciaTurismoContext _context;

        public PacoteListModel(AgenciaTurismoContext context)
        {
            _context = context;
        }

        public List<PacoteTuristico> Pacotes { get; set; }

        public async Task OnGetAsync()
        {
            Pacotes = await _context.PacotesTuristicos
                                    .OrderBy(p => p.DataPartida)
                                    .ToListAsync();
        }
    }
}