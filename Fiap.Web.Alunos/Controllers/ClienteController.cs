using Fiap.Web.Alunos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Storage;
using Fiap.Web.Alunos.Data;
using Microsoft.EntityFrameworkCore;
using Fiap.Web.Alunos.ViewModels;
using AutoMapper;
using Fiap.Web.Alunos.Services;

namespace Fiap.Web.Alunos.Controllers
{

    public class ClienteController : Controller
    {

        //private readonly DatabaseContext _databaseContext;

        private readonly IMapper _mapper;

        private readonly IRepresentanteService _representanteService;
        private readonly IClienteService _clienteService;

        public ClienteController(IMapper mapper, IRepresentanteService representanteService, IClienteService clienteService)
        {

            //_databaseContext = databaseContext;
            _mapper = mapper;
            _representanteService = representanteService;
            _clienteService = clienteService;

        }

        public IActionResult Index()
        {

            //var clientes = _databaseContext.Clientes.Include(c => c.Representante).ToList();
            var clientes = _clienteService.ListarClientes();

            return View(clientes);
        }

        // Antes de usar o MVVM
        /* [HttpGet]
         public IActionResult Create()
         {
             Console.WriteLine("Executou a Action Cadastrar()");

             // Cria a variável par armazenar o SelectList
             var selectListRepresentantes =
                 new SelectList(_databaseContext.Representantes.ToList(),
                      nameof(RepresentanteModel.RepresentanteId),
                      nameof(RepresentanteModel.NomeRepresentante));

             // Adiciona o SelectList a ViewBag para se enviado para a View
             // A propriedade Representantes é criada de forma dinâmica na ViewBag
             ViewBag.Representantes = selectListRepresentantes;

             // Retorna para a View Create um 
             // Objeto modelo com as propriedades em branco

             return View(new ClienteModel());
         }*/

        [HttpGet]
        public IActionResult Create()
        {

            var viewModel = new ClienteCreateViewModel
            {
                Representantes = new SelectList(_representanteService.ListarRepresentantes(),
                                             nameof(RepresentanteModel.RepresentanteId),
                                             nameof(RepresentanteModel.NomeRepresentante))
            };

            return View(viewModel);
        }

        // Antes de usar o MVVM
        /*[HttpPost]
        public IActionResult Create(ClienteModel clienteModel)
        {
            _databaseContext.Clientes.Add(clienteModel);
            _databaseContext.SaveChanges();

            // Criando a mensagem de sucesso que será exibida para o cliente
            TempData["mensagemSucesso"] = $"O cliente {clienteModel.Nome} foi cadastrado com sucesso!";

            // Substituímos o return View()
            // Pelo método de redirecionamento
            return RedirectToAction(nameof(Index));
        }*/

        [HttpPost]
        public IActionResult Create(ClienteCreateViewModel viewModel)
        {

            // Verifica se todos os dados enviados estão válidos conforme as regras definidas no ViewModel
            if (ModelState.IsValid)
            {
                var cliente = _mapper.Map<ClienteModel>(viewModel);
                _clienteService.CriarCliente(cliente);
                TempData["mensagemSucesso"] = $"O cliente {viewModel.Nome} foi cadastrado com sucesso";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                viewModel.Representantes = new SelectList(
                    _representanteService.ListarRepresentantes(),
                    "RepresentanteId", "NomeRepresentante", viewModel.RepresentanteId);

                return View(viewModel);
            }


        }

        [HttpGet]
        public IActionResult Edit(int id)
        {

            var cliente = _clienteService.ObterClientePorId(id);
            if (cliente == null)
            {
                return NotFound();
            }
            else
            {

                ViewBag.Representantes =
                    new SelectList(_representanteService.ListarRepresentantes(),
                                    "RepresentanteId",
                                    "NomeRepresentante",
                                    cliente.RepresentanteId);
                return View(cliente);
            }
        }

        [HttpPost]
        public IActionResult Edit(ClienteModel clienteModel)
        {
            _clienteService.AtualizarCliente(clienteModel);

            TempData["mensagemSucesso"] = $"Os dados do cliente {clienteModel.Nome} foram alterados com sucesso!";
            return RedirectToAction(nameof(Index));

        }




        // Anotação de uso do Verb HTTP Get
        [HttpGet]
        public IActionResult Detail(int id)
        {
            var cliente = _clienteService.ObterClientePorId(id);
            if (cliente == null)
            {
                return NotFound();
            }
            else
            {

                ViewBag.Representantes = new SelectList(_representanteService.ListarRepresentantes(),
                                             nameof(RepresentanteModel.RepresentanteId),
                                             nameof(RepresentanteModel.NomeRepresentante));
                return View(cliente);
            }
        }

        // Anotação de uso do Verb HTTP Get
        [HttpGet]
        public IActionResult Delete(int id)
        {
            _clienteService.DeletarCliente(id);
            TempData["mensagemSucesso"] = $"Os dados do cliente foram removidos com sucesso";
            
            return RedirectToAction(nameof(Index));
        }


        public static List<ClienteModel> GerarClientesMocados()
        {
            var clientes = new List<ClienteModel>();

            for (int i = 1; i <= 5; i++)
            {

                var cliente = new ClienteModel
                {
                    ClienteId = i,
                    Nome = "Cliente" + i,
                    Sobrenome = "Sobrenome" + i,
                    Email = "cliente" + i + "@example.com",
                    DataNascimento = DateTime.Now.AddYears(-30),
                    Observacao = "Observação do cliente" + i,
                    RepresentanteId = i,
                    Representante = new RepresentanteModel
                    {
                        RepresentanteId = i,
                        NomeRepresentante = "Representante" + i,
                        Cpf = "000000000191"
                    }

                };
                clientes.Add(cliente);
            }
            return clientes;
        }

    }
}
