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
    public class CadastrarCampanhasController : ControllerBase
    {
        private ICadastrarCampanhasRepository _cadastrarCampanhaRepository { get; set; }

        public CadastrarCampanhasController()
        {
            _cadastrarCampanhaRepository = new CadastrarCampanhasRepository();
        }


        /// <summary>
        /// Método responsável por cadastrar uma nova Campanha
        /// </summary>
        /// <param name="NovaCampanha">Objeto Campanha com os atributos a serem cadastrados</param>
        /// <returns>Status code 201 created</returns>
        ///[Authorize(Roles = "1")]
        [HttpPost]
        public IActionResult Cadastrar(CadastrarCampanha CadastrarNovaCampanha)
        {
            try
            {
                _cadastrarCampanhaRepository.Cadastrar(CadastrarNovaCampanha);

                return StatusCode(201);

            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }

        /// <summary>
        /// Método responsável por listar todas as Campanhas
        /// </summary>
        /// <returns>uma lista de Campanhas</returns>
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
    }
}
