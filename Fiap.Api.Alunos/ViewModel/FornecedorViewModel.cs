namespace Fiap.Api.Alunos.ViewModel
{
    public class FornecedorViewModel
    {
        public int FornecedorId { get; set; }
        public string Nome { get; set; }
        public List<ProdutoViewModel> Produtos { get; set; }
    }
}
