using Fiap.Web.Alunos.Models;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Web.Alunos.Data.Repository
{
    public class ClienteRepository : IClienteRepository

    {
        private readonly DatabaseContext _context;

        public ClienteRepository(DatabaseContext context)
        {
            _context = context;
        }

        public void Add(ClienteModel cliente)
        {
            _context.Clientes.Add(cliente);
            _context.SaveChanges();
        }

        public void Delete(ClienteModel cliente)
        {
            _context.Clientes.Remove(cliente);
            _context.SaveChanges();
        }

        public IEnumerable<ClienteModel> GetAll() =>
            _context.Clientes.Include(c => c.Representante).ToList();


        public ClienteModel GetById(int id) => _context.Clientes.Find(id);

        public void Update(ClienteModel cliente)
        {
            _context.Update(cliente);
            _context.SaveChanges();
        }
    }
}
