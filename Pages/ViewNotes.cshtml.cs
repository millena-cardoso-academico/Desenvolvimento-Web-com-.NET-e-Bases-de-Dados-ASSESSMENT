using System.ComponentModel.DataAnnotations;
using System.IO;
using Microsoft.AspNetCore.Hosting; // Necessário para IWebHostEnvironment
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AgenciaTurismo.Pages
{
    public class ViewNotesModel : PageModel
    {
        private readonly IWebHostEnvironment _environment;
        private readonly string _notesDirectory;

        public ViewNotesModel(IWebHostEnvironment environment)
        {
            _environment = environment;
            // Define o caminho para a nossa pasta de notas.
            _notesDirectory = Path.Combine(_environment.WebRootPath, "files");
        }

        // Propriedades para o formulário de criação de nota
        [BindProperty]
        [Required(ErrorMessage = "O título é obrigatório.")]
        [Display(Name = "Título da Nota (será o nome do arquivo)")]
        public string TituloNota { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "O conteúdo não pode estar vazio.")]
        [Display(Name = "Conteúdo da Nota")]
        public string ConteudoNota { get; set; }

        // Propriedades para exibir dados na página
        public List<string> NomesDasNotas { get; set; } = new List<string>();
        public string ConteudoNotaSelecionada { get; set; }
        public string TituloNotaSelecionada { get; set; }

        public void OnGet(string view)
        {
            // Garante que o diretório de notas exista.
            if (!Directory.Exists(_notesDirectory))
            {
                Directory.CreateDirectory(_notesDirectory);
            }

            // 1. LÓGICA DE LISTAGEM DE ARQUIVOS
            // Pega todos os arquivos .txt do diretório e extrai apenas o nome deles.
            NomesDasNotas = Directory.GetFiles(_notesDirectory, "*.txt")
                                     .Select(Path.GetFileName)
                                     .ToList();

            // 2. LÓGICA DE VISUALIZAÇÃO DE ARQUIVO
            // Se a URL contém um parâmetro 'view' (ex: /ViewNotes?view=minhanota.txt)
            if (!string.IsNullOrEmpty(view))
            {
                var nomeArquivoSeguro = Path.GetFileName(view);
                var caminhoCompleto = Path.Combine(_notesDirectory, nomeArquivoSeguro);

                if (System.IO.File.Exists(caminhoCompleto))
                {
                    TituloNotaSelecionada = nomeArquivoSeguro;
                    ConteudoNotaSelecionada = System.IO.File.ReadAllText(caminhoCompleto);
                }
            }
        }

        // OnPostAsync é chamado para criar uma nova nota.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Se o formulário for inválido, recarrega a página (e a lista de notas).
                OnGet(null);
                return Page();
            }

            // Sanitiza o nome do arquivo e adiciona a extensão .txt
            var nomeArquivo = $"{Path.GetInvalidFileNameChars().Aggregate(TituloNota, (current, c) => current.Replace(c, '_'))}.txt";
            var caminhoCompleto = Path.Combine(_notesDirectory, nomeArquivo);

            if (System.IO.File.Exists(caminhoCompleto))
            {
                ModelState.AddModelError("TituloNota", "Já existe uma nota com este título. Por favor, escolha outro.");
                OnGet(null);
                return Page();
            }

            // Escreve o conteúdo no arquivo de forma assíncrona.
            await System.IO.File.WriteAllTextAsync(caminhoCompleto, ConteudoNota);

            // Redireciona para a mesma página para limpar o formulário e recarregar a lista.
            return RedirectToPage();
        }
    }
}