using System;
using System.Collections.Generic;

#nullable disable

namespace painel_tcc_senaiSCS.Domains
{
    public partial class Usuario
    {
        public Usuario()
        {
            CadastrarCampanhas = new HashSet<CadastrarCampanha>();
        }

        public int IdUsuario { get; set; }
        public int? IdTipoUsuario { get; set; }
        public string NomeUsuario { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        public virtual TipoUsuario IdTipoUsuarioNavigation { get; set; }
        public virtual ICollection<CadastrarCampanha> CadastrarCampanhas { get; set; }
    }
}
