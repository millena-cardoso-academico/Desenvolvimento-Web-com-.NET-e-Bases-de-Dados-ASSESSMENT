using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AgenciaTurismo.Pages
{
    public class CalcularEstadiaModel : PageModel
    {
        // Propriedade pra receber o n�mero de di�rias do formul�rio
        [BindProperty]
        public int NumeroDiarias { get; set; }

        // Propriedade pra receber o valor da di�ria do formul�rio
        [BindProperty]
        public int ValorDiaria { get; set; }

        // Propriedade pra guardar o resultado e exibir na tela
        public decimal? ValorTotal { get; set; }

        public void OnGet()
        {
        }

        public void OnPost()
        {

            Func<int, int, decimal> calcularTotalFunc = (diarias, valor) => diarias * valor;

            ValorTotal = calcularTotalFunc(NumeroDiarias, ValorDiaria);
        }
    }
}