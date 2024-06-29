using Fiap.Web.Alunos.Models;

namespace Fiap.Web.Alunos.Data.Repository

{
    public class RepresentanteRepository : IRepresentanteRepository
    {

        private readonly DatabaseContext _context;

        public RepresentanteRepository(DatabaseContext context)
        {
            _context = context;
        }
        public void Add(RepresentanteModel representante)
        {
            _context.Representantes.Add(representante);
            _context.SaveChanges();
        }

        public void Delete(RepresentanteModel representante)
        {
            _context.Representantes.Remove(representante);

        }

        public IEnumerable<RepresentanteModel> GetAll()
        {
            return _context.Representantes.ToList();
        }

        public RepresentanteModel GetById(int id)
        {
            return _context.Representantes.Find(id);
        }

        public void Update(RepresentanteModel representante)
        {
            _context.Representantes.Update(representante);
            _context.SaveChanges();
        }
    }
}
