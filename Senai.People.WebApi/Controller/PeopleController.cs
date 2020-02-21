using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.People.WebApi.Domain;
using Senai.People.WebApi.Repository;

namespace Senai.People.WebApi.Controller
{
    [Produces("application/json")]

    [Route("api/[controller]")]

    [ApiController]

    public class PeopleController : ControllerBase
    {
        private PeopleRepository _peopleRepository; //{ get; set; }

        public PeopleController()
        {
            _peopleRepository = new PeopleRepository(); // representante do repository (objeto)
        }

        [HttpGet]
        public IEnumerable<PeopleDomain> Get()
        {
            return _peopleRepository.Listar();
        }

        [HttpPost]
        public IActionResult Post(PeopleDomain novoPeople)
        {
            // Faz a chamada para o método .Cadastrar();
            _peopleRepository.Cadastrar(novoPeople);

            // Retorna um status code 201 - Created
            return StatusCode(201);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // Cria um objeto generoBuscado que irá receber o gênero buscado no banco de dados
            PeopleDomain peopleBuscado = _peopleRepository.BuscarPorId(id);

            // Verifica se nenhum gênero foi encontrado
            if (peopleBuscado == null)
            {
                // Caso não seja encontrado, retorna um status code 404 com a mensagem personalizada
                return NotFound("Nenhum gênero encontrado");
            }

            // Caso seja encontrado, retorna o gênero buscado
            return Ok(peopleBuscado);
        }

        [HttpPut]
        public IActionResult AtualizarIdCorpo(PeopleDomain peopleAtualizado)
        {
            // Cria um objeto generoBuscado que irá receber o gênero buscado no banco de dados
            PeopleDomain peopleAtualizada = _peopleRepository.BuscarPorId(peopleAtualizado.IdFuncionario);

            // Verifica se algum gênero foi encontrado
            if (peopleAtualizada != null)
            {
                // Tenta atualizar o registro
                try
                {
                    // Faz a chamada para o método .AtualizarIdCorpo();
                    _peopleRepository.AtualizarIdCorpo(peopleAtualizado);

                    // Retorna um status code 204 - No Content
                    return NoContent();
                }
                // Caso ocorra algum erro
                catch (Exception erro)
                {
                    // Retorna BadRequest e o erro
                    return BadRequest(erro);
                }

            }

            // Caso não seja encontrado, retorna NotFound com uma mensagem personalizada
            // e um bool para representar que houve erro
            return NotFound
                (
                    new
                    {
                        mensagem = "Pessoa(s) não encontrado",
                        erro = true
                    }
                );
        }

        [HttpPut("{id}")]
        public IActionResult PutIdUrl(int id, PeopleDomain peopleAtualizado)
        {
            // Cria um objeto generoBuscado que irá receber o gênero buscado no banco de dados
            PeopleDomain peopleBuscado = _peopleRepository.BuscarPorId(id);

            // Verifica se nenhum gênero foi encontrado
            if (peopleBuscado == null)
            {
                // Caso não seja encontrado, retorna NotFound com uma mensagem personalizada
                // e um bool para representar que houve erro
                return NotFound
                    (
                        new
                        {
                            mensagem = "Gênero não encontrado",
                            erro = true
                        }
                    );
            }
            // Tenta atualizar o registro
            try
            {
                // Faz a chamada para o método .AtualizarIdUrl();
                _peopleRepository.AtualizarIdUrl(id, peopleAtualizado);

                // Retorna um status code 204 - No Content
                return NoContent();
            }
            // Caso ocorra algum erro
            catch (Exception erro)
            {
                // Retorna BadRequest e o erro
                return BadRequest(erro);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Faz a chamada para o método .Deletar();
            _peopleRepository.Deletar(id);

            // Retorna um status code com uma mensagem personalizada
            return Ok("People deletado");
        }
    }
}