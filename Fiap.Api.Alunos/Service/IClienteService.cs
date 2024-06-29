using Fiap.Api.Alunos.Models;

namespace Fiap.Api.Alunos.Service
{
    public interface IClienteService
    {
        IEnumerable<ClienteModel> ListarClientes();
        ClienteModel ObterClientePorId(int id);
        void CriarCliente(ClienteModel cliente);
        void AtualizarCliente(ClienteModel cliente);
        void DeletarCliente(int id);
    }
}
