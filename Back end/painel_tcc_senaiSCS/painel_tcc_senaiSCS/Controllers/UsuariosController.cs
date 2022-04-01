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

        //[Authorize(Roles = "1,2")]
        [HttpGet]
        public IActionResult Listar()
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

        [Authorize(Roles = "1,2")]
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

        //[Authorize(Roles = "1")]
        [HttpPost]
        public IActionResult Cadastro(Usuario NovoUser)
        {
            try
            {
                Usuario.Cadastrar(NovoUser);

                return Ok(Usuario.Listar().Last());
                //return StatusCode(201); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

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

        [Authorize(Roles = "1")]
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
