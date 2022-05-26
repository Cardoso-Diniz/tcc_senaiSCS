using Microsoft.AspNetCore.Http;
using painel_tcc_senaiSCS.Domains;
using painel_tcc_senaiSCS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace painel_tcc_senaiSCS.Interfaces
{
    public interface ICampanhasRepository
    {
        List<CadastrarCampanha> ListarTodos();
        List<CadastrarCampanha> AtivoList();
        CadastrarCampanha BuscarPorId(int idCadastrarCampanha);
        void Cadastrar(CadastrarCampanha CadastrarNovaCampanha);
        void Atualizar(int id, CadastrarCampanha CampanhaAtualizada);
        void Deletar(int id);
        void AtualizarBool(int idCadastrarCampanha);
    }
}
