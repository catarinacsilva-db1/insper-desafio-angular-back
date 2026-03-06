using CadastroUsuarios.Controllers.Utils;
using CadastroUsuarios.DTO;
using CadastroUsuarios.Models;
using CadastroUsuarios.Service;
using CadastroUsuarios.Service.Utils.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace CadastroUsuarios.Controllers
{
    [RoutePrefix("api/Usuario")]
    public class UsuarioController : ApiController
    {
        private readonly IUsuarioService _service;

        public UsuarioController(IUsuarioService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Index(string filtro = "todos", string termoPesquisa = "")
        {
            var query = await _service.PesquisaUsuarioAsync(filtro, termoPesquisa);
            List<UsuarioDTO> usuarios = query.Select(u => UsuarioMapper.ToDto(u)).ToList();
            return Ok(usuarios);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Obter(int id)
        {
            UsuarioModel usuarioModel = await _service.BuscarPorIdAsync(id);

            if (usuarioModel == null)
            {
                return NotFound();
            }

            UsuarioDTO usuarioDto = UsuarioMapper.ToDto(usuarioModel);
            return Ok(usuarioDto);
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Cadastrar(UsuarioDTO usuarioDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UsuarioModel usuarioModel = UsuarioMapper.ToModel(usuarioDto);

            try
            {
                await _service.AdicionarUsuarioAsync(usuarioModel);
            }
            catch (ValidacaoException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok("Usuário Cadastrado");
        }

        [HttpPut]
        [Route("")]
        public async Task<IHttpActionResult> Editar(UsuarioDTO usuarioDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                UsuarioModel usuarioModel = UsuarioMapper.ToModel(usuarioDto);
                await _service.EditarUsuarioAsync(usuarioModel);
            }
            catch (ValidacaoException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok("Usuário Atualizado");
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Deletar(int id)
        {
            try
            {
                await _service.DeletarUsuarioAsync(id);
            }
            catch (ValidacaoException ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Usuário Excluído");
        }

        [HttpPut]
        [Route("status/{id:int}")]
        public async Task<IHttpActionResult> AtualizarStatus(int id)
        {
            try
            {
                await _service.EditarStatusUsuarioAsync(id);
            }
            catch (ValidacaoException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok("Status do usuário atualizado");
        }
    }
}
