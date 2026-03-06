using CadastroUsuarios.Controllers.Utils;
using CadastroUsuarios.Models;
using CadastroUsuarios.Service;
using CadastroUsuarios.Service.Utils.Exceptions;
using CadastroUsuarios.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;

namespace CadastroUsuarios.Controllers
{
    public class UsuarioController : ApiController
    {
        private readonly IUsuarioService _service;

        public UsuarioController(IUsuarioService service)
        {
            _service = service;
        }

        public async Task<ActionResult> Index(string filtro = "todos", string termoPesquisa = "")
        {
            var query = await _service.PesquisaUsuarioAsync(filtro, termoPesquisa);
            List<UsuarioViewModel> usuarios = query.Select(u => UsuarioMapper.ToViewModel(u)).ToList();

            ViewBag.FiltroAtual = filtro;
            ViewBag.TermoPesquisa = termoPesquisa;
            return View(usuarios);
        }

        public ActionResult Cadastrar()
        {
            return View();
        }


        [HttpPost, ActionName("Cadastrar")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Cadastrar(UsuarioViewModel usuarioViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.mensagemErro = "Usuário não cadastrado";
                return View(usuarioViewModel);
            }

            UsuarioModel usuarioModel = UsuarioMapper.ToModel(usuarioViewModel);

            try
            {
                await _service.AdicionarUsuarioAsync(usuarioModel);
            }
            catch (ValidacaoException ex)
            {
                ViewBag.mensagemErro = ex.Message;
                return View(usuarioViewModel);
            }

            TempData["mensagemSucesso"] = "Usuário Cadastrado";
            return RedirectToAction("Index");

        }

        public async Task<ActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UsuarioModel usuarioModel = await _service.BuscarPorIdAsync(id.Value);

            if (usuarioModel == null)
            {
                return HttpNotFound();
            }

            UsuarioViewModel usuarioViewModel = UsuarioMapper.ToViewModel(usuarioModel);
            return View(usuarioViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(UsuarioViewModel usuarioViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.mensagemErro = "Usuário não atualizado";
                return View(usuarioViewModel);
            }

            try
            {
                UsuarioModel usuarioModel = UsuarioMapper.ToModel(usuarioViewModel);
                await _service.EditarUsuarioAsync(usuarioModel);

            }
            catch (ValidacaoException ex)
            {
                ViewBag.mensagemErro = ex.Message;
                return View(usuarioViewModel);
            }

            TempData["mensagemSucesso"] = "Usuário Atualizado";
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _service.DeletarUsuarioAsync(id);

            }
            catch (ValidacaoException ex)
            {
                ViewBag.mensagemErro = ex.Message;
                return RedirectToAction("Index");
            }
            TempData["mensagemSucesso"] = "Usuário Excluído";
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("AtualizarStatus")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AtualizarStatus(int id)
        {
            try
            {
                await _service.EditarStatusUsuarioAsync(id);

            }
            catch (ValidacaoException ex)
            {
                ViewBag.mensagemErro = ex.Message;
                return RedirectToAction("Index");
            }

            TempData["mensagemSucesso"] = "Status do usuário atualizado";
            return RedirectToAction("Index");
        }
    }
}
