using AgenciaTurismo.Data;
using AgenciaTurismo.Models;
using AgenciaTurismo.ViewModels; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AgenciaTurismo.Pages
{
    public class CreatePacoteModel : PageModel
    {
        private readonly AgenciaTurismoContext _context;

        public CreatePacoteModel(AgenciaTurismoContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PacoteTuristico Pacote { get; set; }

        [BindProperty]
        public List<DestinoSelectionViewModel> DestinosDisponiveis { get; set; }

        public async Task OnGetAsync()
        {
            // Carrega todas as cidades do banco para popular os checkboxes
            var todasAsCidades = await _context.CidadesDestino.ToListAsync();

            DestinosDisponiveis = todasAsCidades.Select(cidade => new DestinoSelectionViewModel
            {
                Id = cidade.Id,
                Nome = cidade.Nome,
                IsSelected = false
            }).ToList();

            Pacote = new PacoteTuristico
            {
                DataPartida = DateTime.Today.AddDays(7)
            };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Pacote.DataPartida < DateTime.Today)
            {
                ModelState.AddModelError("Pacote.DataPartida", "A data de partida não pode ser uma data passada.");
            }

            // Precisamos recarregar a lista de destinos se o modelo for inválido
            if (!ModelState.IsValid)
            {
                // Repopula a lista de destinos para reexibir o formulário corretamente
                var todasAsCidades = await _context.CidadesDestino.ToListAsync();
                DestinosDisponiveis = todasAsCidades.Select(c => new DestinoSelectionViewModel { Id = c.Id, Nome = c.Nome }).ToList();
                return Page();
            }

            // Se o modelo for válido, adicionamos os destinos selecionados ao novo pacote.
            // Limpa a lista para garantir que só os selecionados entrem.
            Pacote.Destinos = new List<CidadeDestino>();
            foreach (var destinoVM in DestinosDisponiveis.Where(d => d.IsSelected))
            {
                var destinoDoBanco = await _context.CidadesDestino.FindAsync(destinoVM.Id);
                if (destinoDoBanco != null)
                {
                    Pacote.Destinos.Add(destinoDoBanco);
                }
            }

            _context.PacotesTuristicos.Add(Pacote);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"O pacote '{Pacote.Titulo}' foi cadastrado com sucesso!";
            return RedirectToPage("./PacoteList"); // Redirecionando para a lista de pacotes
        }
    }
}