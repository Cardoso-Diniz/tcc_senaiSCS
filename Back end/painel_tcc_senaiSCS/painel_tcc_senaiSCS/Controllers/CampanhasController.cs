using Campanha.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using painel_tcc_senaiSCS.Domains;
using painel_tcc_senaiSCS.Interfaces;
using painel_tcc_senaiSCS.Repositories;
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

                return StatusCode(204);
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
        [HttpDelete("{idCadastrarCampanha}")]
        public IActionResult Deletar(int idCadastrarCampanha)
        {
            try
            {
                // Faz a chamada para o método
                _campanhasRepository.Deletar(idCadastrarCampanha);

                // Retorna um status code
                return StatusCode(204);
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

            #region Upload da Imagem com extensões permitidas apenas
            string[] extensoesPermitidas = { "jpg", "png", "jpeg", "gif", "mp4" };
            string uploadResultado = Upload.UploadFile(arquivo, extensoesPermitidas);

            if (uploadResultado == "")
            {
                return BadRequest("Arquivo não encontrado");
            }

            if (uploadResultado == "Extensão não permitida")
            {
                return BadRequest("Extensão de arquivo não permitida");
            }

            campanha.Arquivo = uploadResultado;
            #endregion

            _campanhasRepository.Cadastrar(campanha);

            return Created("Campanha", campanha);
        }
    }
}
