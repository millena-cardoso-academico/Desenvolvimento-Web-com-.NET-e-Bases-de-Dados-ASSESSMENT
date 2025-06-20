namespace AgenciaTurismo.Models
{
    public class Reserva
    {
        public int Id { get; set; }
        public DateTime DataReserva { get; set; }

        // Chave Estrangeira para Cliente
        public int ClienteId { get; set; }
        // Prop. de Navegação: Cada reserva pertence a UM Cliente.
        public Cliente Cliente { get; set; }

        // Chave Estrangeira para PacoteTuristico
        public int PacoteTuristicoId { get; set; }
        // Prop. de Navegação: Cada reserva pertence a UM Pacote.
        public PacoteTuristico PacoteTuristico { get; set; }
    }
}