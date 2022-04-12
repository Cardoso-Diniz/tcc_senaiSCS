﻿using Campanha.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using painel_tcc_senaiSCS.Contexts;
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
        /// Lista Todas as Campanhas!!
        /// </summary>
        /// <returns></returns>
        //[Authorize(Roles = "2")]
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
        //[Authorize(Roles = "2")]
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
         /// Deleta uma Campanha existente!!
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
        /// Cadastra uma Campanha com Upload de imagem
        /// </summary>
        /// <param name="campanha"></param>
        /// <param name="arquivo"></param>
        /// <returns></returns>
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
