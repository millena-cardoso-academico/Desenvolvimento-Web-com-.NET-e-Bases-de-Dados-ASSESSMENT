using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AgenciaTurismo.Models
{
    public delegate void CapacityReachedEventHandler(object sender, CapacityReachedEventArgs e);

    public class PacoteTuristico
    {
        public int Id { get; set; }

        [Display(Name = "Título do Pacote")]
        [Required(ErrorMessage = "O título é obrigatório.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "O título deve ter entre 5 and 100 caracteres.")]
        public string Titulo { get; set; }

        [Display(Name = "Data de Partida")]
        [Required(ErrorMessage = "A data de partida é obrigatória.")]
        [DataType(DataType.Date)] // Isso ajuda o navegador a mostrar um seletor de data.
        public DateTime DataPartida { get; set; }

        [Display(Name = "Duração em Dias")]
        [Required(ErrorMessage = "A duração é obrigatória.")]
        [Range(1, 30, ErrorMessage = "A duração deve ser entre 1 e 30 dias.")]
        public int DuracaoDias { get; set; }

        [Display(Name = "Nº Máximo de Pessoas")]
        [Required(ErrorMessage = "A capacidade máxima é obrigatória.")]
        [Range(1, 50, ErrorMessage = "A capacidade deve ser entre 1 e 50 pessoas.")]
        public int CapacidadeMaxima { get; set; }

        [Display(Name = "Preço por Pessoa")]
        [Required(ErrorMessage = "O preço é obrigatório.")]
        [DataType(DataType.Currency)]
        [Range(1.00, 20000.00, ErrorMessage = "O preço deve ser entre R$ 1,00 e R$ 20.000,00.")]
        public decimal Preco { get; set; }

        [ValidateNever]
        public List<CidadeDestino> Destinos { get; set; } = new List<CidadeDestino>();
        [ValidateNever]
        public List<Reserva> Reservas { get; set; } = new List<Reserva>();

        public event CapacityReachedEventHandler CapacityReached;


        public void AdicionarReserva(Reserva novaReserva)
        {
            if (Reservas.Count < CapacidadeMaxima)
            {
                Reservas.Add(novaReserva);
                Console.WriteLine($"Reserva adicionada para o pacote '{Titulo}'. Vagas atuais: {Reservas.Count}/{CapacidadeMaxima}");

                // VERIFICA SE A CAPACIDADE FOI ATINGIDA *AGORA*
                if (Reservas.Count == CapacidadeMaxima)
                {
                    // Se atingiu, dispara o evento!!!
                    OnCapacityReached();
                }
            }
            else
            {
                // Se já estava lotado, pode disparar de novo para alertar sobre a tentativa de overbooking.
                Console.WriteLine($"Tentativa de reserva para pacote lotado '{Titulo}'.");
                OnCapacityReached();
            }
        }

        protected virtual void OnCapacityReached()
        {
            if (CapacityReached != null)
            {
                var args = new CapacityReachedEventArgs
                {
                    PacoteId = this.Id,
                    TituloPacote = this.Titulo,
                    CapacidadeMaxima = this.CapacidadeMaxima,
                    HorarioOcorrencia = DateTime.Now
                };

                // Dispara o evento, notificando todos os assinantes.
                CapacityReached(this, args);
            }
        }
    }
}