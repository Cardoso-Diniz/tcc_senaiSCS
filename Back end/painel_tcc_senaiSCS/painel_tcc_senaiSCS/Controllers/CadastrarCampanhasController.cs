using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using painel_tcc_senaiSCS.Domains;
using painel_tcc_senaiSCS.Interfaces;
using painel_tcc_senaiSCS.Repositories;
using Campanha.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using painel_tcc_senaiSCS.Contexts;

namespace painel_tcc_senaiSCS.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CadastrarCampanhasController : ControllerBase
    {
        private ICadastrarCampanhasRepository _cadastrarCampanhaRepository { get; set; }

        private readonly PainelSenaiContext _context;
        public CadastrarCampanhasController(PainelSenaiContext context)
        {
            _context = context;
        }
        public CadastrarCampanhasController()
        {
            _cadastrarCampanhaRepository = new CadastrarCampanhasRepository();
        }


        [Authorize]
        [HttpGet]
        public IActionResult ListarTodos()
        {
            try
            {
                return Ok(_cadastrarCampanhaRepository.ListarTodos());
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }

        /// <summary>
        /// Método responsável por buscar uma Campanha pelo id
        /// </summary>
        /// <param name="idCadastrarCampanha">id da Campanha a ser procurada</param>
        /// <returns>Uma Campanha</returns>
        [Authorize]
        [HttpGet("{idCadastrarCampanha}")]
        public IActionResult BuscarPorId(int idCadastrarCampanha)
        {
            try
            {
                CadastrarCampanha CadastrarCampanhaBuscada = _cadastrarCampanhaRepository.BuscarPorId(idCadastrarCampanha);

                if (CadastrarCampanhaBuscada != null)
                {
                    return Ok(CadastrarCampanhaBuscada);
                }

                return BadRequest("A Campanha requisitada não existe");

            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }

        }

        /// <summary>
        /// Método responsável por atualizar uma Campanha
        /// </summary>
        /// <param name="idCadastrarCampanha">Id da Campanha a ser buscada</param>
        /// <param name="CampanhaAtualizada">Objeto com atributos a serem inseridos</param>
        /// <returns>Status code 204 no content</returns>
        //[Authorize(Roles = "1")]
        [HttpPut("{idCadastrarCampanha}")]
        public IActionResult Atualizar(int idCadastrarCampanha, CadastrarCampanha CampanhaAtualizada)
        {
            try
            {
                _cadastrarCampanhaRepository.Atualizar(idCadastrarCampanha, CampanhaAtualizada);

                return StatusCode(204);
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }

        }

        /// <summary>
        /// Método para excluir uma Campanha
        /// </summary>
        /// <param name="idCadastrarCampanha">Id da Campanha a ser buscada</param>
        /// <returns>Status code 204 no content</returns>
       ///[Authorize(Roles = "1")]
        [HttpDelete("{idCadastrarCampanha}")]
        public IActionResult Deletar(int idCadastrarCampanha)
        {
            try
            {
                _cadastrarCampanhaRepository.Deletar(idCadastrarCampanha);

                return StatusCode(204);
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }
        [HttpPost]
        public async Task<ActionResult<CadastrarCampanha>> PostCadastrarCampanha([FromForm] CadastrarCampanha cadastrarCampanha, IFormFile arquivo)
        {

            #region Upload da Imagem com extensões permitidas apenas
            string[] extensoesPermitidas = { "jpg", "png", "jpeg", "gif" };
            string uploadResultado = Upload.UploadFile(arquivo, extensoesPermitidas);

            if (uploadResultado == "")
            {
                return BadRequest("Arquivo não encontrado");
            }

            if (uploadResultado == "Extensão não permitxida")
            {
                return BadRequest("Extensão de arquivo não permitida");
            }

            cadastrarCampanha.Arquivo = uploadResultado;
            #endregion


            _context.CadastrarCampanhas.Add(cadastrarCampanha);
            await _context.SaveChangesAsync();

            return Created("Campanha", cadastrarCampanha);
        }
    }
}
