using AlunosApi.Models;
using AlunosApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace AlunosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AlunosController : ControllerBase
    {
        private readonly AlunosService _alunoService;

        public AlunosController(AlunosService alunoService)
        {
            _alunoService = alunoService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Aluno>>> GetAlunos()
        {
            try
            {
                var alunos = await _alunoService.GetAlunos();
                return Ok(alunos);
            }
            catch
            {
                return BadRequest("Request inválido");
                //return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter alunos");
            }
        }

        [HttpGet("AlunoPorNome")]
        public async Task<ActionResult<IEnumerable<Aluno>>> GetAlunoPorNome([FromQuery] string nome)
        {
            var alunos = await _alunoService.GetAlunoByName(nome);

            if (alunos == null)
                return NotFound($"Não existem alunos com nome = {nome}");

            return Ok(alunos);
        }

        [HttpGet("{id:int}", Name = "GetAluno")]
        public async Task<ActionResult<Aluno>> GetAluno(int id)
        {
            try
            {
                var aluno = await _alunoService.GetAluno(id);

                if (aluno == null)
                    return NotFound($"Aluno com id= {id} não encontrado");

                return Ok(aluno);
            }
            catch
            {
                return BadRequest("Request inválido");
            }
        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Create(Aluno aluno)
        {
            try
            {
                bool senhaExiste = await _alunoService.SenhaExiste(aluno.Senha);
                if (senhaExiste)
                {
                    return BadRequest("Senha já existe!");
                }

                await _alunoService.CreateAluno(aluno);
                return CreatedAtRoute(nameof(GetAluno), new { id = aluno.Id }, aluno);
            }
            catch
            {
                //return BadRequest("Request inválido");
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao criar um novo aluno");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Edit(int id, [FromBody] Aluno aluno)
        {
            try
            {
                if (aluno.Id == id)
                {

                    
                    await _alunoService.UpdateAluno(aluno);
                    //return NoContent();
                    return Ok($"Aluno com id={id} atualizado com sucesso");
                }
                else
                {
                    return BadRequest("Dados inconsistentes");
                }
            }
            catch (Exception)
            {
                return BadRequest("Request inválido");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var aluno = await _alunoService.GetAluno(id);
                if (aluno != null)
                {
                    await _alunoService.DeleteAluno(aluno);
                    return Ok($"Aluno de id={id} excluído com sucesso");
                    //return Ok(id);
                }
                else
                {
                    return NotFound($"Aluno com id= {id} não encontrado");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{senha:int}/acesso", Name = "GetAcesso")]
        public async Task<ActionResult<Aluno>> GetAcesso(int senha)
        {
            try
            {
                string diretorioLogs = Path.Combine(AppContext.BaseDirectory, "Logs");

                // Verifica se o diretório de logs existe, senão, cria
                if (!Directory.Exists(diretorioLogs))
                {
                    Directory.CreateDirectory(diretorioLogs);
                }


                var caminhoLog = Path.Combine(diretorioLogs, $"Log_{DateTime.Now:ddMMyyyy}.txt");
                var aluno = await _alunoService.GetAcesso(senha);

                if (aluno == null)
                {
                    using (StreamWriter sw = System.IO.File.AppendText(caminhoLog))
                    {
                        sw.WriteLine($"{DateTime.Now:dd/MM/yyyy HH:mm:ss:fff} - Acesso negado para a senha {senha}");
                    }

                    return NotFound("Senha inválida!");
                }


                //var caminhoLog = Path.Combine(AppContext.BaseDirectory, "Logs", $"Log_{DateTime.Now:ddMMyyyy}.txt");
                using (StreamWriter sw = System.IO.File.AppendText(caminhoLog))
                {
                    sw.WriteLine($"{DateTime.Now:dd/MM/yyyy HH:mm:ss:fff} - Acesso liberado para o aluno {aluno.Nome} com a senha {senha}");
                }


                return Ok(aluno);

            }
            catch
            {
                return BadRequest("Request inválido");
            }
        }
        
    }
}
