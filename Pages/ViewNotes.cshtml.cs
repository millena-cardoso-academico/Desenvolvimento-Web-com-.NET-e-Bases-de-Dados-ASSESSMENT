using System.ComponentModel.DataAnnotations;
using System.IO;
using Microsoft.AspNetCore.Hosting; // Necess�rio para IWebHostEnvironment
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

        // Propriedades para o formul�rio de cria��o de nota
        [BindProperty]
        [Required(ErrorMessage = "O t�tulo � obrigat�rio.")]
        [Display(Name = "T�tulo da Nota (ser� o nome do arquivo)")]
        public string TituloNota { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "O conte�do n�o pode estar vazio.")]
        [Display(Name = "Conte�do da Nota")]
        public string ConteudoNota { get; set; }

        // Propriedades para exibir dados na p�gina
        public List<string> NomesDasNotas { get; set; } = new List<string>();
        public string ConteudoNotaSelecionada { get; set; }
        public string TituloNotaSelecionada { get; set; }

        public void OnGet(string view)
        {
            // Garante que o diret�rio de notas exista.
            if (!Directory.Exists(_notesDirectory))
            {
                Directory.CreateDirectory(_notesDirectory);
            }

            // 1. L�GICA DE LISTAGEM DE ARQUIVOS
            // Pega todos os arquivos .txt do diret�rio e extrai apenas o nome deles.
            NomesDasNotas = Directory.GetFiles(_notesDirectory, "*.txt")
                                     .Select(Path.GetFileName)
                                     .ToList();

            // 2. L�GICA DE VISUALIZA��O DE ARQUIVO
            // Se a URL cont�m um par�metro 'view' (ex: /ViewNotes?view=minhanota.txt)
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

        // OnPostAsync � chamado para criar uma nova nota.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Se o formul�rio for inv�lido, recarrega a p�gina (e a lista de notas).
                OnGet(null);
                return Page();
            }

            // Sanitiza o nome do arquivo e adiciona a extens�o .txt
            var nomeArquivo = $"{Path.GetInvalidFileNameChars().Aggregate(TituloNota, (current, c) => current.Replace(c, '_'))}.txt";
            var caminhoCompleto = Path.Combine(_notesDirectory, nomeArquivo);

            if (System.IO.File.Exists(caminhoCompleto))
            {
                ModelState.AddModelError("TituloNota", "J� existe uma nota com este t�tulo. Por favor, escolha outro.");
                OnGet(null);
                return Page();
            }

            // Escreve o conte�do no arquivo de forma ass�ncrona.
            await System.IO.File.WriteAllTextAsync(caminhoCompleto, ConteudoNota);

            // Redireciona para a mesma p�gina para limpar o formul�rio e recarregar a lista.
            return RedirectToPage();
        }
    }
}