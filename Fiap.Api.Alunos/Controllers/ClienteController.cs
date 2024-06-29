using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Fiap.Api.Alunos.Models;
using Fiap.Api.Alunos.Service;
using Fiap.Api.Alunos.ViewModel;
using Microsoft.AspNetCore.Authorization;

namespace Fiap.Web.Alunos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _service;
        private readonly IMapper _mapper;

        public ClienteController(IClienteService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles ="operador,analista,gerente")]
        public ActionResult<IEnumerable<ClienteViewModel>> Get()
        {
            var clientes = _service.ListarClientes();
            var viewModelList = _mapper.Map<IEnumerable<ClienteViewModel>>(clientes);
            return Ok(viewModelList);
        }

        [HttpGet("{id}")]
        [Authorize(Roles ="analista,gerente")]
        public ActionResult<ClienteViewModel> Get(int id)
        {
            var cliente = _service.ObterClientePorId(id);
            if (cliente == null)
                return NotFound();

            var viewModel = _mapper.Map<ClienteViewModel>(cliente);
            return Ok(viewModel);
        }

        [HttpPost]
        public ActionResult Post([FromBody] ClienteViewModel viewModel)
        {
            var cliente = _mapper.Map<ClienteModel>(viewModel);
            _service.CriarCliente(cliente);
            return CreatedAtAction(nameof(Get), new { id = cliente.ClienteId }, cliente);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] ClienteViewModel viewModel)
        {
            var clienteExistente = _service.ObterClientePorId(id);
            if (clienteExistente == null)
                return NotFound();

            _mapper.Map(viewModel, clienteExistente);
            _service.AtualizarCliente(clienteExistente);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _service.DeletarCliente(id);
            return NoContent();
        }
    }
}