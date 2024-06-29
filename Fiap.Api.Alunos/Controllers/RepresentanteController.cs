using AutoMapper;
using Fiap.Api.Alunos.Models;
using Fiap.Api.Alunos.Service;
using Fiap.Api.Alunos.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Api.Alunos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepresentanteController : ControllerBase
    {
        private readonly IRepresentanteService _representanteService;
        private readonly IMapper _mapper; // Realiza o mapeamento entre Model e viewModel


        public RepresentanteController(IRepresentanteService represetanteService, IMapper mapper1)
        {
            _representanteService = represetanteService;
            _mapper = mapper1;

        }

        [HttpGet]
        public ActionResult<IEnumerable<RepresentanteViewModel>> Get()
        {
            var lista = _representanteService.ListarRepresentantes();
            var viewModelList = _mapper.Map<IEnumerable<RepresentanteViewModel>>(lista);

            if (viewModelList == null)
            {
                return NoContent();
            }
            else
            {
                return Ok(viewModelList);
            }

        }

        [HttpGet("{id}")]
        public ActionResult<RepresentanteViewModel> Get([FromRoute] int id)
        {
            var representante = _representanteService.ObterRepresentantePorId(id);
            if (representante == null)
            {
                return NotFound();
            }
            else
            {
                var viewModel = _mapper.Map<RepresentanteViewModel>(representante);
                return Ok(viewModel);
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] RepresentanteViewModel viewModel)
        {
            var model = _mapper.Map<RepresentanteModel>(viewModel);
            _representanteService.CriarRepresentante(model);

            return CreatedAtAction(nameof(Get), new { id = model.RepresentanteId }, model);
        }


        [HttpPut("{id}")]
        public ActionResult Put([FromRoute] int id, [FromBody] RepresentanteViewModel viewModel)
        {
            if(viewModel.RepresentanteId == id)
            {
                var model = _mapper.Map<RepresentanteModel>(viewModel);
                _representanteService.AtualizarRepresentante(model);

                return Ok(viewModel);
            } else
            {
                return BadRequest();
            }
                        
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _representanteService.DeletarRepresentante(id);

            return NoContent();
        }

    }
}


