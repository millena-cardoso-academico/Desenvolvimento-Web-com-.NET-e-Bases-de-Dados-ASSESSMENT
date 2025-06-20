using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AgenciaTurismo.Models
{
    public class CidadeDestino
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo 'Nome da Cidade' é de preenchimento obrigatório.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome da cidade deve ter no mínimo 3 caracteres.")]
        public string Nome { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Você precisa selecionar um país.")]
        public int PaisDestinoId { get; set; }
        [ValidateNever]
        public PaisDestino PaisDestino { get; set; }
    }
}