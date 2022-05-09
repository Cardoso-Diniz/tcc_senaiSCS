using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using painel_tcc_senaiSCS.Domains;
using painel_tcc_senaiSCS.Interfaces;
using painel_tcc_senaiSCS.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace painel_tcc_senaiSCS.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private IUsuariosRepository Usuario { get; set; }

        public UsuariosController()
        {
            Usuario = new UsuariosRepository();
        }

        /// <summary>
        /// Lista todos os usuarios
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "1")]
        [HttpGet("Listar")]
        public IActionResult ListarTodos()
        {
            try
            {
                return Ok(Usuario.Listar());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Busca os usuarios pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            try
            {
                return Ok(Usuario.BuscarPorId(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// Cadastra um novo Usuario
        /// </summary>
        /// <param name="NovoUser"></param>
        /// <returns></returns>
        [Authorize(Roles = "1")]
        [HttpPost]  
        public IActionResult Cadastro(Usuario NovoUser)
        {
            try
            {
                Usuario.Cadastrar(NovoUser);

                return Ok(Usuario.Listar().Last());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// Deleta um usuario
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "1")]
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                Usuario.Deletar(id);
                return Ok("Usuario Deletado");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// Atualiza informações do usuario
        /// </summary>
        /// <param name="id"></param>
        /// <param name="NovoUser"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Usuario NovoUser)
        {
            try
            {
                Usuario.Atualizar(id, NovoUser);
                return Ok("Usuario Atualizado");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
