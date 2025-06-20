using AgenciaTurismo.BusinessLogic;
using AgenciaTurismo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace AgenciaTurismo.Pages
{
    public class SimularReservasModel : PageModel
    {
        public string PacoteJson { get; set; }

        public PacoteTuristico PacoteTeste { get; set; }
        public AlertaService AlertaService { get; private set; }

        private void CarregarPacoteDoTempData()
        {
            AlertaService = new AlertaService();
            PacoteJson = TempData["PacoteJson"] as string;

            if (string.IsNullOrEmpty(PacoteJson))
            {
                PacoteTeste = new PacoteTuristico
                {
                    Id = 101,
                    Titulo = "Viagem para o Monte Roraima",
                    CapacidadeMaxima = 3
                };
            }
            else
            {
                PacoteTeste = JsonConvert.DeserializeObject<PacoteTuristico>(PacoteJson);
            }
        }

        private void SalvarPacoteNoTempData()
        {
            TempData["PacoteJson"] = JsonConvert.SerializeObject(PacoteTeste);
        }

        public void OnGet()
        {
            CarregarPacoteDoTempData();
            TempData.Keep("PacoteJson");
        }

        public IActionResult OnPost()
        {
            CarregarPacoteDoTempData();

            PacoteTeste.CapacityReached += AlertaService.LidarComCapacidadeAtingida;
            PacoteTeste.AdicionarReserva(new Reserva { Id = PacoteTeste.Reservas.Count + 1, DataReserva = DateTime.Now });

            SalvarPacoteNoTempData();

            return RedirectToPage();
        }
    }
}