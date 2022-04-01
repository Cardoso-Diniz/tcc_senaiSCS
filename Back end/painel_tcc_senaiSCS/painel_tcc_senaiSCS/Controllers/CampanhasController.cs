using Campanha.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using painel_tcc_senaiSCS.Context;
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

        /// <summary>
        /// Instancia o objeto para que haja referência às implementações no repositório
        /// </summary>
        public CampanhasController(ICampanhasRepository context)
        {
            _campanhasRepository = context;
        }
        /// <summary>
        /// Lista todos as Campanhas existentes
        /// </summary>
        /// <returns>Uma lista de campanha</returns>
        //[Authorize(Roles = "2")]
        [HttpGet("ListarTodos")]
        public IActionResult ListarTodos()
        {
            return Ok(_campanhasRepository.ListarTodos());
        }

        /// <summary>
        /// Busca uma campanha pelo id
        /// </summary>
        /// <param name="idCadastrarCampanha">id da campanha a ser buscado</param>
        /// <returns>Uma campanha encontrado com status code - 200</returns>
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
        /// Cadastra uma Campanha
        /// </summary>
        /// <param name="CadastrarNovaCampanha">Campanha a ser cadastrado</param>
        /// <returns>Um status code 201 - Created</returns>
        //[HttpPost]
        //public IActionResult Cadastrar(CadastrarCampanha CadastrarNovaCampanha)
        //{
        //    _campanhasRepository.Cadastrar(CadastrarNovaCampanha);

        //    return StatusCode(201);
        //}

        /// <summary>
        /// Atualiza uma Campanha existente
        /// </summary>
        /// <param name="CampanhaAtualizada">Objeto com as novas informações da Campanha e o id da Campanha a ser atualizada</param>
        /// <returns>Um status code 204 - No content</returns>
        //[Authorize(Roles = "2")]
        [HttpPut("{idClinica}")]
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
        /// Deleta uma Campanha
        /// </summary>
        /// <param name="idCadastrarCampanha">id da Campanha a ser deletada/param>
        /// <returns>Um status code 204 - No content</returns>
        [HttpDelete("{idCadastrarCampanha}")]
        public IActionResult Deletar(int idCadastrarCampanha)
        {
            _campanhasRepository.Deletar(idCadastrarCampanha);

            return StatusCode(204);
        }

        [HttpPost]
        public IActionResult Cadastrar([FromForm] CadastrarCampanha campanha, IFormFile arquivo)
        {

            #region Upload da Imagem com extensões permitidas apenas
            string[] extensoesPermitidas = { "jpg", "png", "jpeg", "gif" };
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
