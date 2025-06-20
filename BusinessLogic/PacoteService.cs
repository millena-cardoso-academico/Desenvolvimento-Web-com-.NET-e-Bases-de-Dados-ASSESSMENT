using AgenciaTurismo.Models;

namespace AgenciaTurismo.BusinessLogic
{
    public class PacoteService
    {
        // Lista estática para simular uma tabela de banco de dados.
        private static readonly List<PacoteTuristico> _pacotes;

        // Construtor estático é executado uma vez para inicializar a lista com dados.
        static PacoteService()
        {
            _pacotes = new List<PacoteTuristico>
            {
                new PacoteTuristico
                {
                    Id = 1,
                    Titulo = "Expedição Amazônia",
                    DataPartida = new DateTime(2025, 9, 10),
                    DuracaoDias = 7,
                    CapacidadeMaxima = 12,
                    Preco = 4500.00m
                },
                new PacoteTuristico
                {
                    Id = 2,
                    Titulo = "Praias de Fernando de Noronha",
                    DataPartida = new DateTime(2025, 11, 20),
                    DuracaoDias = 5,
                    CapacidadeMaxima = 8,
                    Preco = 7800.00m
                },
                new PacoteTuristico
                {
                    Id = 3,
                    Titulo = "Cânions do Sul",
                    DataPartida = new DateTime(2025, 7, 5),
                    DuracaoDias = 4,
                    CapacidadeMaxima = 15,
                    Preco = 2200.00m
                }
            };
        }

        public List<PacoteTuristico> GetAll()
        {
            return _pacotes;
        }

        public PacoteTuristico GetById(int id)
        {
            // Usa LINQ pra encontrar o primeiro pacote que bate com ID.
            // Retorna null se não encontrar.
            return _pacotes.FirstOrDefault(p => p.Id == id);
        }
    }
}