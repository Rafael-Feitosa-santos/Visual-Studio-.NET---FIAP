using Fiap.Api.Alunos.Models;
using Fiap.Web.Alunos.Data.Contexts;
using Fiap.Web.Alunos.Data.Repository;
using Microsoft.EntityFrameworkCore;

public class ClienteRepository : IClienteRepository
{
    private readonly DatabaseContext _context;

    public ClienteRepository(DatabaseContext context)
    {
        _context = context;
    }

    public IEnumerable<ClienteModel> GetAll() => _context.Clientes.Include(c => c.Representante).ToList();

    public ClienteModel GetById(int id) => _context.Clientes.Find(id);

    public void Add(ClienteModel cliente)
    {
        _context.Clientes.Add(cliente);
        _context.SaveChanges();
    }

    public void Update(ClienteModel cliente)
    {
        _context.Update(cliente);
        _context.SaveChanges();
    }

    public void Delete(ClienteModel cliente)
    {
        _context.Clientes.Remove(cliente);
        _context.SaveChanges();
    }


    public IEnumerable<ClienteModel> GetAll(int page, int size)
    {
        return _context.Clientes.Include(c => c.Representante)
                        .Skip((page - 1) * page)
                        .Take(size)
                        .AsNoTracking()
                        .ToList();
    }
    public IEnumerable<ClienteModel> GetAllReference(int lastReference, int size)
    {
        var clientes = _context.Clientes.Include(_ => _.Representante)
                            .Where(c => c.ClienteId > lastReference)
                            .OrderBy(c => c.ClienteId)
                            .Take(size)
                            .AsNoTracking()
                            .ToList();
        return clientes;
    }

    public IEnumerable<ClienteModel> GetAll(int pagina)
    {
        throw new NotImplementedException();
    }
}