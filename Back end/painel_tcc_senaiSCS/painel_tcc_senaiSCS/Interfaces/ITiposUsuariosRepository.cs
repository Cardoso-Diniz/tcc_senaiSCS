using painel_tcc_senaiSCS.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace painel_tcc_senaiSCS.Interfaces
{
    interface ITiposUsuariosRepository
    {
        List<TipoUsuario> ListarTodos();
        List<TipoUsuario> ListarUser();
        void Cadastrar(TipoUsuario NovoTipoUsuario);
        void Deletar(int id);
        void Atualizar(int id, TipoUsuario NovoTipoUsuario);
    }
}
