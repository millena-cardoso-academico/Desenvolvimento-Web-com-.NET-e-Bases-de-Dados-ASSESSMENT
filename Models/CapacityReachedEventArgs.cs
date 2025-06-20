namespace AgenciaTurismo.Models
{
    public class CapacityReachedEventArgs : EventArgs
    {
        public int PacoteId { get; set; }
        public string TituloPacote { get; set; }
        public int CapacidadeMaxima { get; set; }
        public DateTime HorarioOcorrencia { get; set; }
    }
}