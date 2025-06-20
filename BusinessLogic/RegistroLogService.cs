using System.Diagnostics; // Debug.WriteLine
using System.IO; // manipular arquivos

namespace AgenciaTurismo.BusinessLogic
{
    public class RegistroLogService
    {
        private static readonly List<string> logsEmMemoria = new List<string>();
        private readonly string caminhoArquivoLog = "log_operacoes.txt";

        // MÉTODO PARA REGISTRAR NO CONSOLE DE DEBUG
        public void LogToConsole(string mensagem)
        {
            Debug.WriteLine($"[LOG CONSOLE]: {mensagem}");
        }

        // 2. MÉTODO PARA REGISTRAR EM UM ARQUIVO
        public void LogToFile(string mensagem)
        {
            // File.AppendAllText anexa o texto ao final do arquivo.
            File.AppendAllText(caminhoArquivoLog, $"[LOG ARQUIVO] ({DateTime.Now}): {mensagem}\n");
        }

        // 3. MÉTODO PARA REGISTRAR NA MEMÓRIA
        public void LogToMemory(string mensagem)
        {
            logsEmMemoria.Add($"[LOG MEMÓRIA]: {mensagem}");
        }
        public List<string> ObterLogsDaMemoria()
        {
            return logsEmMemoria;
        }
    }
}