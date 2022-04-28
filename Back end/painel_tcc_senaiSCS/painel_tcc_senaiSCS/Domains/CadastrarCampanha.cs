using System;
using System.Collections.Generic;

#nullable disable

namespace painel_tcc_senaiSCS.Domains
{
    public partial class CadastrarCampanha
    {
        public int IdCampanha { get; set; }
        public int? IdUsuario { get; set; }
        public string NomeCampanha { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public string Arquivo { get; set; }
        public string Descricao { get; set; }
        public bool? CampanhaAtiva { get; set; }

        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}
