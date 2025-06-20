using AgenciaTurismo.BusinessLogic; // Importando o serviço
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AgenciaTurismo.Pages
{
    public class TestarLogsModel : PageModel
    {
        public List<string> LogsNaMemoria { get; set; }

        private readonly RegistroLogService _logService = new RegistroLogService();

        public void OnGet()
        {
            LogsNaMemoria = _logService.ObterLogsDaMemoria();
        }

        public IActionResult OnPost()
        {
            Action<string> registrarLogAction;

            registrarLogAction = _logService.LogToConsole;
            registrarLogAction += _logService.LogToFile;
            registrarLogAction += _logService.LogToMemory;

            string mensagem = $"Nova reserva de pacote simulada em {DateTime.Now.ToLongTimeString()}";

            registrarLogAction(mensagem);

            return RedirectToPage();
        }
    }
}