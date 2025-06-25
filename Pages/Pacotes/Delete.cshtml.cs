using AgenciaTurismo.Data;
using AgenciaTurismo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AgenciaTurismo.Pages.Pacotes
{
    public class DeleteModel : PageModel
    {
        private readonly AgenciaTurismoContext _context;

        public DeleteModel(AgenciaTurismoContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PacoteTuristico PacoteTuristico { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacoteturistico = await _context.PacotesTuristicos
                                        .Include(p => p.Destinos)
                                        .FirstOrDefaultAsync(m => m.Id == id);
            if (pacoteturistico == null)
            {
                return NotFound();
            }
            else
            {
                PacoteTuristico = pacoteturistico;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacoteParaDeletar = await _context.PacotesTuristicos
                                                .Include(p => p.Reservas)
                                                .FirstOrDefaultAsync(p => p.Id == id);

            if (pacoteParaDeletar == null)
            {
                return RedirectToPage("./Index");
            }

            if (pacoteParaDeletar.Reservas.Any())
            {
                ModelState.AddModelError(string.Empty, "Este pacote não pode ser excluído, pois já existem reservas associadas a ele.");

                PacoteTuristico = pacoteParaDeletar;
                return Page();
            }

            try
            {
                // Se não houver reservas, remove o pacote fisicamente.
                _context.PacotesTuristicos.Remove(pacoteParaDeletar);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Pacote excluído com sucesso!";
                return RedirectToPage("./Index");
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError(string.Empty, "Não foi possível remover o pacote. Tente novamente.");
                PacoteTuristico = pacoteParaDeletar;
                return Page();
            }
        }
    }
}