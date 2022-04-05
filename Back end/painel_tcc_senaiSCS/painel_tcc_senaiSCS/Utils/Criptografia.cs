using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace painel_tcc_senaiSCS.Utils
{
    public class Criptografia
    {
        public static string GerarHash(string senha)
        {
            return BCrypt.Net.BCrypt.HashPassword(senha);
        }
        public static bool Comparar(string senha, string senhaBanco)
        {
            bool A = BCrypt.Net.BCrypt.Verify(senha, senhaBanco); return A;
        }
        public static bool Validate(string senhaBanco)
        {
            if (senhaBanco.Length >= 32 && senhaBanco.Substring(0, 1) == "$")
            {
                return true;
            }
            else return false;
        }
    }
}
