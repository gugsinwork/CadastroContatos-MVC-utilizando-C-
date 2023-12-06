using CadastroContatos.Models;
using System;
using System.Collections.Generic;


namespace CadastroContatos.Repositorio
{
    public interface IContatoRepositorio
    {
        List<ContatoModel> BuscarTodos();
        ContatoModel Adicionar(ContatoModel contato);
        ContatoModel ListarPorId(int Id);
        ContatoModel Atualizar(ContatoModel contato);
        bool Apagar(int id);
    }
}
