using painel_tcc_senaiSCS.Context;
using painel_tcc_senaiSCS.Domains;
using painel_tcc_senaiSCS.Interfaces;
using painel_tcc_senaiSCS.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace painel_tcc_senaiSCS.Repositories
{
    public class UsuariosRepository : IUsuariosRepository
    {
        PainelSenaiContext ctx = new PainelSenaiContext();
        public void Atualizar(int id, Usuario NovoUsuario)
        {
            Usuario UsuarioBuscado = BuscarPorId(id);

            if (NovoUsuario.IdTipoUsuario > 0 && NovoUsuario.NomeUsuario != null && NovoUsuario.Email != null && NovoUsuario.Senha != null)
            {
                UsuarioBuscado.IdTipoUsuario = NovoUsuario.IdTipoUsuario;
                UsuarioBuscado.NomeUsuario = NovoUsuario.NomeUsuario;
                UsuarioBuscado.Email = NovoUsuario.Email;
                UsuarioBuscado.Senha = NovoUsuario.Senha;
                ctx.Usuarios.Update(UsuarioBuscado);
                ctx.SaveChanges();
            }

        }

        public Usuario BuscarPorId(int id)
        {
            return ctx.Usuarios.FirstOrDefault(e => e.IdUsuario == id);
        }

        public void Cadastrar(Usuario NovoUsuario)
        {
            ctx.Add(NovoUsuario);
            ctx.SaveChanges();
        }

        public void Deletar(int id)
        {
            Usuario UsuarioBuscado = ctx.Usuarios.Find(id);
            ctx.Usuarios.Remove(UsuarioBuscado);
            ctx.SaveChanges();
        }

        public List<Usuario> Listar()
        {
            return ctx.Usuarios.ToList();
        }

        public Usuario Login(string email, string senha)
        {
            return ctx.Usuarios.FirstOrDefault(e => e.Email == email && e.Senha==senha);
            //var usuario ctx.Usuarios.FirstOrDefault(e => e.Email == email);
            //if (usuario != null)
            //{ 
            //    if (usuario.Senha.Length < 32)
            //    {
            //        usuario.Senha = Criptografia.GerarHash(usuario.Senha);
            //        ctx.Update(usuario);
            //        ctx.SaveChanges();
            //    }

            //    bool comparado = Criptografia.Comparar(senha, usuario.Senha);
            //    if (comparado)
            //        return usuario;
            //}

            //return null;
        }
    }
}
