using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
namespace AgenciaTurismo.Models
{
    public class Reserva
    {
        public int Id { get; set; }
        public DateTime DataReserva { get; set; }


        [Range(1, int.MaxValue, ErrorMessage = "É obrigatório selecionar um cliente.")]
        [Display(Name = "Cliente")]
        public int ClienteId { get; set; }
       
        [ValidateNever] 
        public Cliente Cliente { get; set; }

        // Chave Estrangeira para PacoteTuristico
        public int PacoteTuristicoId { get; set; }
        [ValidateNever] 
        public PacoteTuristico PacoteTuristico { get; set; }
    }
}