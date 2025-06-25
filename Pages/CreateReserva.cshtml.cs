using AgenciaTurismo.Data;
using AgenciaTurismo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AgenciaTurismo.Pages
{
    public class CreateReservaModel : PageModel
    {
        private readonly AgenciaTurismoContext _context;

        public CreateReservaModel(AgenciaTurismoContext context)
        {
            _context = context;
        }

        public PacoteTuristico PacoteParaReservar { get; set; }

        public SelectList ClientesSelectList { get; set; }

        [BindProperty]
        public Reserva NovaReserva { get; set; }

        public async Task<IActionResult> OnGetAsync(int pacoteId)
        {
            PacoteParaReservar = await _context.PacotesTuristicos.FindAsync(pacoteId);
            if (PacoteParaReservar == null)
            {
                return NotFound();
            }

            var clientes = await _context.Clientes.Where(c => !c.IsDeleted).OrderBy(c => c.Nome).ToListAsync();
            ClientesSelectList = new SelectList(clientes, "Id", "Nome");

            NovaReserva = new Reserva
            {
                PacoteTuristicoId = pacoteId
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await RecarregarDadosDaPagina(NovaReserva.PacoteTuristicoId);
                return Page();
            }

            bool reservaExistente = await _context.Reservas.AnyAsync(r =>
                r.ClienteId == NovaReserva.ClienteId &&
                r.PacoteTuristicoId == NovaReserva.PacoteTuristicoId);

            if (reservaExistente)
            {
                ModelState.AddModelError(string.Empty, "Este cliente já possui uma reserva para este pacote.");
                await RecarregarDadosDaPagina(NovaReserva.PacoteTuristicoId);
                return Page();
            }


            NovaReserva.DataReserva = DateTime.Now;

            _context.Reservas.Add(NovaReserva);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Reserva realizada com sucesso!";
            return RedirectToPage("/Pacotes/Details", new { id = NovaReserva.PacoteTuristicoId });
        }

        private async Task RecarregarDadosDaPagina(int pacoteId)
        {
            PacoteParaReservar = await _context.PacotesTuristicos.FindAsync(pacoteId);
            var clientes = await _context.Clientes.Where(c => !c.IsDeleted).OrderBy(c => c.Nome).ToListAsync();
            ClientesSelectList = new SelectList(clientes, "Id", "Nome");
        }
    }
}