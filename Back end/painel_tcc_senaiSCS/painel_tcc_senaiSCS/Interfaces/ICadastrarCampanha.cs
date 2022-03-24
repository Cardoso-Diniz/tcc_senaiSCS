using painel_tcc_senaiSCS.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace painel_tcc_senaiSCS.Interfaces
{
    interface ICadastrarCampanha
    {
        List<CadastrarCampanha> ListarTodos();
        CadastrarCampanha BuscarPorId(int idCadastrarCampanha);
        void Cadastrar(int idCadastrarCampanha);
        void Atualizar(int idCadastrarCampanha);
        void Deletar(int idCadastrarCampanha);
    }
}
