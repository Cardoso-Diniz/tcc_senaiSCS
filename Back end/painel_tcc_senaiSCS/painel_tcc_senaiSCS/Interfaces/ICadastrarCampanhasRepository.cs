using painel_tcc_senaiSCS.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace painel_tcc_senaiSCS.Interfaces
{
    interface ICadastrarCampanhasRepository
    {
        List<CadastrarCampanha> ListarTodos();
        CadastrarCampanha BuscarPorId(int idCadastrarCampanha);
        void Cadastrar(CadastrarCampanha CadastrarNovaCampanha);
        void Atualizar(int id, CadastrarCampanha CampanhaAtualizada);
        void Deletar(int id);
    }
}
