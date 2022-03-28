using painel_tcc_senaiSCS.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace painel_tcc_senaiSCS.Interfaces
{
    interface IUsuariosRepository
    {
        Usuario Login(string email, string senha);
        List<Usuario> Listar();
        Usuario BuscarPorId(int id);
        void Cadastrar(Usuario NovoUsuario);
        void Deletar(int id);
        void Atualizar(int id,Usuario NovoUsuario);

    }
}
