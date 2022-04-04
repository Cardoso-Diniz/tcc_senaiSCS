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
    public class TiposUsuariosRepository : ITiposUsuariosRepository
    {
        PainelSenaiContext ctx = new PainelSenaiContext();
        public void Atualizar(int id, TipoUsuario NovoTipoUsuario)
        {
            TipoUsuario UTipoBuscado = ctx.TipoUsuarios.Find(id);

            if (NovoTipoUsuario.NomeTipoUsuario != null)
            {
                UTipoBuscado.NomeTipoUsuario = NovoTipoUsuario.NomeTipoUsuario;
            }

            ctx.TipoUsuarios.Update(UTipoBuscado);
            ctx.SaveChanges();
        }

        public void Cadastrar(TipoUsuario NovoTipoUsuario)
        {
            ctx.TipoUsuarios.Add(NovoTipoUsuario);
            ctx.SaveChanges();  
        }

        public void Deletar(int id)
        {
            TipoUsuario UTipoBuscado = ctx.TipoUsuarios.Find(id);
            ctx.TipoUsuarios.Remove(UTipoBuscado);
            ctx.SaveChanges();
        }

        public List<TipoUsuario> ListarTodos()
        {
            return ctx.TipoUsuarios.ToList();
        }

        public List<TipoUsuario> ListarUser()
        {
            return ctx.TipoUsuarios.
               Include(e => e.Usuarios).ToList();
        }
    }
}
