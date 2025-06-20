namespace AgenciaTurismo.BusinessLogic
{
    // 1. DECLARAÇÃO DO DELEGATE
    public delegate decimal CalculateDelegate(decimal precoOriginal);

    public class DescontoService
    {
        public decimal AplicarDescontoPadrao(decimal precoOriginal)
        {
            return precoOriginal * 0.90m;
        }
    }
}