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
                ModelState.AddModelError(string.Empty, "Este cliente j� possui uma reserva para este pacote.");
                await RecarregarDadosDaPagina(NovaReserva.PacoteTuristicoId);
                return Page();
            }

            var pacote = await _context.PacotesTuristicos
                               .Include(p => p.Reservas)
                               .FirstOrDefaultAsync(p => p.Id == NovaReserva.PacoteTuristicoId);

            if (pacote == null)
            {
                ModelState.AddModelError(string.Empty, "O pacote selecionado n�o foi encontrado.");
                await RecarregarDadosDaPagina(NovaReserva.PacoteTuristicoId);
                return Page();
            }

            if (pacote.DataPartida <= DateTime.Today)
            {
                ModelState.AddModelError(string.Empty, "Este pacote n�o pode ser reservado pois sua data de partida j� passou.");
                await RecarregarDadosDaPagina(NovaReserva.PacoteTuristicoId);
                return Page();
            }

            if (pacote.Reservas.Count >= pacote.CapacidadeMaxima)
            {
                ModelState.AddModelError(string.Empty, $"N�o h� mais vagas para este pacote. A capacidade m�xima � de {pacote.CapacidadeMaxima} participantes.");
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