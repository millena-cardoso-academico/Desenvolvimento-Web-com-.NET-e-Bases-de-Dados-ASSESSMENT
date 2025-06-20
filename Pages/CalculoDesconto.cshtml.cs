using AgenciaTurismo.BusinessLogic; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AgenciaTurismo.Pages
{
    public class CalculoDescontoModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public decimal PrecoOriginal { get; set; }

        public decimal? PrecoComDesconto { get; set; }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            // 1. Crio uma instância do meu serviço de desconto.
            var descontoService = new DescontoService();

            CalculateDelegate meuDelegateDeCalculo = descontoService.AplicarDescontoPadrao;

            PrecoComDesconto = meuDelegateDeCalculo(PrecoOriginal);
        }
    }
}