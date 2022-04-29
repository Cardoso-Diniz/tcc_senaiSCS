using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using painel_tcc_senaiSCS.Context;
using painel_tcc_senaiSCS.Domains;
using painel_tcc_senaiSCS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace painel_tcc_senaiSCS.Repositories
{
    public class CampanhasRepository : ICampanhasRepository
    {
        PainelSenaiContext ctx = new PainelSenaiContext();
  

        public void Atualizar(int id, CadastrarCampanha CampanhaAtualizada)
        {
            CadastrarCampanha CadastrarCampanhaBuscada = BuscarPorId(id);

            if (CadastrarCampanhaBuscada.IdUsuario != null && CadastrarCampanhaBuscada.NomeCampanha != null && CadastrarCampanhaBuscada.DataInicio != null && CadastrarCampanhaBuscada.DataFim != null && CadastrarCampanhaBuscada.Arquivo != null && CadastrarCampanhaBuscada.Descricao != null && CadastrarCampanhaBuscada.CampanhaAtiva != null)
            {
                CadastrarCampanhaBuscada.IdUsuario = CadastrarCampanhaBuscada.IdUsuario;
                CadastrarCampanhaBuscada.NomeCampanha = CadastrarCampanhaBuscada.NomeCampanha;
                CadastrarCampanhaBuscada.DataInicio = CadastrarCampanhaBuscada.DataInicio;
                CadastrarCampanhaBuscada.DataFim = CadastrarCampanhaBuscada.DataFim;
                CadastrarCampanhaBuscada.Arquivo = CadastrarCampanhaBuscada.Arquivo;
                CadastrarCampanhaBuscada.Descricao = CadastrarCampanhaBuscada.Descricao;
                CadastrarCampanhaBuscada.CampanhaAtiva = CadastrarCampanhaBuscada.CampanhaAtiva;
            }

            ctx.CadastrarCampanhas.Update(CadastrarCampanhaBuscada);

            ctx.SaveChanges();
        }

        public CadastrarCampanha BuscarPorId(int idCadastrarCampanha)
        {
            return ctx.CadastrarCampanhas.FirstOrDefault(c => c.IdCampanha == idCadastrarCampanha);
        }

        public void Cadastrar(CadastrarCampanha CadastrarNovaCampanha)
        {
            ctx.CadastrarCampanhas.Add(CadastrarNovaCampanha);

            ctx.SaveChanges();
        }


        public void Deletar(int idCadastrarCampanha)
        {
            CadastrarCampanha CadastrarCampanhaBuscada = BuscarPorId(idCadastrarCampanha);

            ctx.CadastrarCampanhas.Remove(CadastrarCampanhaBuscada);

            ctx.SaveChanges();
        }

        public List<CadastrarCampanha> ListarTodos()
        {
            return ctx.CadastrarCampanhas
                .Select(c => new CadastrarCampanha
                {
                    IdCampanha = c.IdCampanha,
                    IdUsuarioNavigation = new Usuario  
                    {
                        IdUsuario = c.IdUsuarioNavigation.IdUsuario,
                        NomeUsuario = c.IdUsuarioNavigation.NomeUsuario
                    },
                    NomeCampanha = c.NomeCampanha,
                    DataInicio = c.DataInicio,
                    DataFim = c.DataFim,
                    Arquivo = c.Arquivo,
                    Descricao = c.Descricao,
                    CampanhaAtiva = c.CampanhaAtiva,
                }).ToList();
        }
    }
}
