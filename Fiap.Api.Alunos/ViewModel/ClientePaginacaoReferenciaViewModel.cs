namespace Fiap.Api.Alunos.ViewModel
{
    public class ClientePaginacaoReferenciaViewModel
    {
        public IEnumerable<ClienteViewModel> Clientes { get; set; }
        public int PageSize { get; set; }
        public int Ref { get; set; }
        public int NextRef { get; set; }
        public string PreviousPageUrl => $"/Cliente?referencia={Ref}&tamanho={PageSize}";
        public string NextPageUrl => (Ref < NextRef) ? $"/Cliente?referencia={NextRef}&tamanho={PageSize}" : "";

    }
}
