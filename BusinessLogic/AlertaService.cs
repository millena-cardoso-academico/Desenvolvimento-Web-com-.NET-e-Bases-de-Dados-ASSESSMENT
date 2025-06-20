using AgenciaTurismo.Models;
using System.Diagnostics;

namespace AgenciaTurismo.BusinessLogic
{
    public class AlertaService
    {
        // Este é o EVENT HANDLER.
        public void LidarComCapacidadeAtingida(object sender, CapacityReachedEventArgs e)
        {
            string mensagem = $"!!! ALERTA DE CAPACIDADE !!! Pacote '{e.TituloPacote}' (ID: {e.PacoteId}) atingiu sua capacidade máxima de {e.CapacidadeMaxima} pessoas em {e.HorarioOcorrencia}. Novas reservas serão bloqueadas.";

            // Registra o alerta no console de depuração do Visual Studio.
            Debug.WriteLine(mensagem);
        }
    }
}