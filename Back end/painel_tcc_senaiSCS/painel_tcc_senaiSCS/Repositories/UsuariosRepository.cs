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

            if (UsuarioBuscado.IdTipoUsuario > 0 && UsuarioBuscado.NomeUsuario != null && UsuarioBuscado.Email != null && UsuarioBuscado.Senha != null)
            {
                UsuarioBuscado.IdTipoUsuario = UsuarioBuscado.IdTipoUsuario;
                UsuarioBuscado.NomeUsuario = UsuarioBuscado.NomeUsuario;
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
            var user = ctx.Usuarios.FirstOrDefault(u => u.Email == email);


            if (user != null)
            {
                if (Criptografia.Validate(user.Senha) == true)
                {
                    bool IsEncrypted = Criptografia.Comparar(senha, user.Senha);
                    if (IsEncrypted)    
                        return user;
                }
                else
                {
                    EncryptPassword(user);
                    bool IsEncrypted = Criptografia.Comparar(senha, user.Senha);
                    if (IsEncrypted)
                        return user;
                }
            }

            return null;
        }
        public async void EncryptPassword(Usuario _user)
        {
            _user.Senha = Criptografia.GerarHash(_user.Senha);
            ctx.Usuarios.Update(_user);
            await ctx.SaveChangesAsync();
        }

    }
}
