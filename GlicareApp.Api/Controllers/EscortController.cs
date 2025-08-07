// -----------------------------------------------------------------------------
// 📄 Classe: EscortController
// 📦 Namespace: GlicareApp.Api.Controllers
//
// 🧠 O que é:
// Controlador de API responsável por lidar com as rotas relacionadas a acompanhantes.
//
// 🔧 Pra que serve:
// - Receber e responder às requisições HTTP relacionadas a Escort.
// - Delegar a lógica para os comandos e handlers (via MediatR).
// -----------------------------------------------------------------------------

using GlicareApp.Services.Commands;
using GlicareApp.Services.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GlicareApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EscortController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public EscortController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEscort([FromBody] CreateEscortCommand command)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var result = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetEscortById), new { id = result.Id }, result);
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEscortById(string id)
        {
            var escort = await _mediator.Send(new GetEscortByIdQuery(id));

            if (escort == null)
                return NotFound();

            return Ok(escort);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var escorts = await _mediator.Send(new GetAllEscortsQuery());
            return Ok(escorts);
        }
        }
    }
