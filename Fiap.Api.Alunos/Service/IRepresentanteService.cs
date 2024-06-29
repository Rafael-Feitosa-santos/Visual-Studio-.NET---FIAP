using Fiap.Api.Alunos.Models;

namespace Fiap.Api.Alunos.Service
{
    public interface IRepresentanteService
    {
        IEnumerable<RepresentanteModel> ListarRepresentantes();
        RepresentanteModel ObterRepresentantePorId(int id);
        void CriarRepresentante(RepresentanteModel representante);
        void AtualizarRepresentante(RepresentanteModel representante);
        void DeletarRepresentante(int id);
    }
}
