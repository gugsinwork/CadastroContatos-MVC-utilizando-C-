using CadastroContatos.Models;
using CadastroContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CadastroContatos.Controllers
{
    public class ContatoController : Controller
    {
        private readonly IContatoRepositorio _contatoRepositorio;
        public ContatoController(IContatoRepositorio contatoRepositorio)
        {
            _contatoRepositorio = contatoRepositorio;
        }
        public IActionResult Index()
        {
            List<ContatoModel> contatos = _contatoRepositorio.BuscarTodos();
            return View(contatos);
        }
        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult Editar(int id)
        {
           ContatoModel contato = _contatoRepositorio.ListarPorId(id);
            return View(contato);
        }
        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _contatoRepositorio.Apagar(id);
                if (apagado)
                {
                    TempData["MensagemApagar"] = "Contato excluido com sucesso!";
                }
                else
                {
                    TempData["MensagemErro"] = $"Ops, não conseguimos excluir seu contato, tente novamente";
                }
               
                return RedirectToAction("Index");
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos excluir seu contato, tente novamente, detalhe do erro:{erro.Message}";
                return RedirectToAction("Index");
                throw;
            }
        }
        public IActionResult ApagarConfirmacao(int id)
        {
                ContatoModel contato = _contatoRepositorio.ListarPorId(id);
                return View(contato);
        }
        [HttpPost]
        public IActionResult Criar(ContatoModel contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _contatoRepositorio.Adicionar(contato);
                    TempData["MensagemSucesso"] = "Contato cadastrado com sucesso";
                    return RedirectToAction("Index");
                }

                return View(contato);
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar seu contato, tente novamente, detalhe do erro:{erro.Message}";
                return RedirectToAction("Index");
                throw;
            }
        }
        [HttpPost]
        public IActionResult Alterar(ContatoModel contato)
        {
            try
            {
                    if (ModelState.IsValid)
                    {
                        _contatoRepositorio.Atualizar(contato);
                        TempData["MensagemEditar"] = "Contato editado com sucesso";
                    return RedirectToAction("Index");
                    }
                    return View("Editar", contato);
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos editar seu contato, tente novamente, detalhe do erro:{erro.Message}";
                return RedirectToAction("Index");
                throw;
            }
        }
        
    }
}
