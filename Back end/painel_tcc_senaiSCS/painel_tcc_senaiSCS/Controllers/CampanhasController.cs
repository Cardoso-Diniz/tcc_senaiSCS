using Campanha.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using painel_tcc_senaiSCS.Domains;
using painel_tcc_senaiSCS.Interfaces;
using painel_tcc_senaiSCS.Repositories;
using painel_tcc_senaiSCS.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace painel_tcc_senaiSCS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampanhasController : ControllerBase
    {
        private readonly ICampanhasRepository _campanhasRepository;


        public CampanhasController(ICampanhasRepository context)
        {
            _campanhasRepository = context;
        }
        /// <summary>
        /// Lista todas as campanhas
        /// </summary>
        /// <returns></returns>
        [HttpGet("ListarTodos")]
        public IActionResult ListarTodos()
        {
            return Ok(_campanhasRepository.ListarTodos());
        }

        /// <summary>
        /// Busca uma campanha pelo id
        /// </summary>
        /// <param name="idCadastrarCampanha"></param>
        /// <returns></returns>
        [HttpGet("{idCadastrarCampanha}")]
        public IActionResult BuscarPorId(int idCadastrarCampanha)
        {
            CadastrarCampanha CadastrarCampanhaBuscada = _campanhasRepository.BuscarPorId(idCadastrarCampanha);

            if (CadastrarCampanhaBuscada == null)
            {
                return NotFound("A campanha informada não existe!");
            }
            return Ok(CadastrarCampanhaBuscada);
        }

        /// <summary>
        /// Atualiza uma campanha existente
        /// </summary>
        /// <param name="idCadastrarCampanha"></param>
        /// <param name="CampanhaAtualizada"></param>
        /// <returns></returns>
        [HttpPut("{idCadastrarCampanha}")]
        public IActionResult Atualizar(int idCadastrarCampanha, CadastrarCampanha CampanhaAtualizada)
        {
            try
            {
                _campanhasRepository.Atualizar(idCadastrarCampanha, CampanhaAtualizada);

                return Ok("A campanha informada foi atualizada!!");
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }

        }

        /// <summary>
        /// Deleta uma campanha existente
        /// </summary>
        /// <param name="idCadastrarCampanha"></param>
        /// <returns></returns>
        [Authorize(Roles = "1,2")]
        [HttpDelete("{idCadastrarCampanha}")]
        public IActionResult Deletar(int idCadastrarCampanha)
        {
            try
            {
                // Faz a chamada para o método
                _campanhasRepository.Deletar(idCadastrarCampanha);

                // Retorna um status code
                return Ok("A campanha informada, foi deleta com sucesso!!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// Lista todas as campanhas ativas
        /// </summary>
        /// <returns></returns>
        [HttpGet("AtivoList")]
        public IActionResult AtivoList()
        {
            try
            {
                return Ok(_campanhasRepository.AtivoList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// Cadastra a campanha junto com a imagem
        /// </summary>
        /// <param name="campanha"></param>
        /// <param name="arquivo"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Cadastrar([FromForm] CadastrarCampanha campanha, IFormFile arquivo)
        {

            _campanhasRepository.Cadastrar(campanha);

            return Created("Campanha", campanha);
        }
        //[Authorize(Roles = "1,2")]
        [HttpPatch("Ativo/{idCadastrarCampanha}")]
        public IActionResult AtualizarBool(int idCadastrarCampanha)
        {
            try
            {
                _campanhasRepository.AtualizarBool(idCadastrarCampanha);
                return Ok("A campanha foi atualizada com sucesso!!");
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }

        }
    }
}
