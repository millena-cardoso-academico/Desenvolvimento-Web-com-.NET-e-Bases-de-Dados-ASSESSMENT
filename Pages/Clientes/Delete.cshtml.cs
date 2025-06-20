using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AgenciaTurismo.Data;
using AgenciaTurismo.Models;

namespace AgenciaTurismo.Pages.Clientes
{
    public class DeleteModel : PageModel
    {
        private readonly AgenciaTurismo.Data.AgenciaTurismoContext _context;

        public DeleteModel(AgenciaTurismo.Data.AgenciaTurismoContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Cliente Cliente { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FirstOrDefaultAsync(m => m.Id == id);

            if (cliente == null)
            {
                return NotFound();
            }
            else
            {
                Cliente = cliente;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente != null)
            {
                cliente.IsDeleted = true;
                _context.Attach(cliente).State = EntityState.Modified; // Informa ao EF que o objeto foi alterado
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = $"Cliente '{cliente.Nome}' foi excluído com sucesso.";
            }

            return RedirectToPage("./Index");
        }
    }
}
