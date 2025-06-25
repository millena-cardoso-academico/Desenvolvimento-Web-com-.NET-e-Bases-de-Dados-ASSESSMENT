using AgenciaTurismo.Data; 
using AgenciaTurismo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore; 
namespace AgenciaTurismo.Pages
{
    public class PacoteDetailsModel : PageModel
    {
        private readonly AgenciaTurismoContext _context;

        public PacoteDetailsModel(AgenciaTurismoContext context)
        {
            _context = context;
        }

        public PacoteTuristico Pacote { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id) 
        {
            if (id == null)
            {
                return NotFound();
            }

            Pacote = await _context.PacotesTuristicos
                                 .Include(p => p.Destinos) 
                                 .FirstOrDefaultAsync(p => p.Id == id);

            if (Pacote == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}