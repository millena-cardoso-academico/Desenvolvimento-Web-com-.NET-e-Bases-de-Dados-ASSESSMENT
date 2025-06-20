using System.ComponentModel.DataAnnotations;
namespace AgenciaTurismo.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Display(Name = "Nome Completo")]
        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Display(Name = "Endereço de E-mail")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        // Propriedade de navegação para as reservas do cliente
        public List<Reserva> Reservas { get; set; } = new List<Reserva>();
        public bool IsDeleted { get; set; } = false;
    }
}